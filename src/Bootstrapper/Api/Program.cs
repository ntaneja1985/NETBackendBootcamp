// initialize the web application builder.



var builder = WebApplication.CreateBuilder(args);

//Add services to the container
//builder.Services.AddControllers(); //Add MVC Controllers
//builder.Services.AddCarter(configurator: config =>
//{
//    //Get all implementations of ICarterModule and register them to expose the HTTP methods.
//    var catalogModules = typeof(CatalogModule).Assembly.GetTypes()
//                         .Where(t=>t.IsAssignableTo(typeof(ICarterModule))).ToArray();
//    config.WithModules(catalogModules);
//});

builder.Services
    .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

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

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.UseExceptionHandler(options =>
{

});

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
