// initialize the web application builder.

using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

//Add services to the container
//builder.Services.AddControllers(); //Add MVC Controllers
//builder.Services.AddCarter(configurator: config =>
//{
//    //Get all implementations of ICarterModule and register them to expose the HTTP methods.
//    var catalogModules = typeof(CatalogModule).Assembly.GetTypes()
//                         .Where(t=>t.IsAssignableTo(typeof(ICarterModule))).ToArray();
//    config.WithModules(catalogModules);
//});

//common services: carter, mediatr, fluentvalidation
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

builder.Services
    .AddCarterWithAssemblies(catalogAssembly,basketAssembly);

builder.Services
    .AddMediatRWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

//Register Mass Transit Extension method to register Mass Transit with Assemblies
//Providing the assemblies allows mass transit to scan these assemblies for consumers and other configurations
//Only include those assemblies which need asynchronous communications
//builder.Services.AddMassTransitWithAssemblies(catalogAssembly, basketAssembly);
builder.Services.AddMassTransitRabbitMqWithAssemblies(builder.Configuration, new[] {basketAssembly,catalogAssembly});


//Add Keycloak Authentication
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//build the webapplication
var app = builder.Build();

//Configure the HTTP request pipeline

//Expose HTTP Request endpoints
app.MapCarter();


//Log every HTTP Request
app.UseSerilogRequestLogging();

//Use exception handler
app.UseExceptionHandler(options =>
{

});

app.UseAuthentication();
app.UseAuthorization();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();


//Handle Exceptions 
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if(exception == null)
//        {
//            return;
//        }
//        var problemDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };
//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";
//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});

////Use Static files
//app.UseStaticFiles();

////Use Routing
//app.UseRouting();

////add authentication
//app.UseAuthentication();

////add authorization
//app.UseAuthorization();

////define endpoints
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//run the application
app.Run();
