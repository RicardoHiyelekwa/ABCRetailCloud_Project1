
using ABCRetailCloud.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<TableStorageService>();
builder.Services.AddSingleton<BlobStorageService>();
builder.Services.AddSingleton<QueueStorageService>();
builder.Services.AddSingleton<FileStorageService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
name:"default",
pattern:"{controller=Home}/{action=Index}/{id?}"
);

app.Run();
