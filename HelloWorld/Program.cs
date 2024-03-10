var builder = WebApplication.CreateBuilder(args);
//register all of the controllers and views 
builder.Services.AddControllersWithViews();
var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
app.MapGet("/profile", () => "hi i am mr kaying.");
app.MapGet("/friends", () => "Su Su,Mya Mya,Aye Aye,Aung Aung");
//enable the map the default urls pattern 
//app.MapDefaultControllerRoute();
app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
app.Run();
