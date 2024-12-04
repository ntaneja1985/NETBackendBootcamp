// initialize the web application builder.

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
//builder.Services.AddControllers(); //Add MVC Controllers
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);


//build the webapplication
var app = builder.Build();

//Configure the HTTP request pipeline

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();


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
