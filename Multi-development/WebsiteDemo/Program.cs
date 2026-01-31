using WebsiteDemo;
using WebsiteDemo.Services.Blogging;
using Syncfusion.Blazor;
using WebsiteDemo.Services;
using WebsiteDemo.Services.Pages;
using WebsiteDemo.Services.Events;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSyncfusionBlazor();
// need to change this logic 
builder.Services.AddHttpClient<IBloggingService, BloggingService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});
builder.Services.AddHttpClient<ICustomMenuService, CustomMenuService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});
builder.Services.AddHttpClient<IRowService, RowService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});
builder.Services.AddHttpClient<IPageService, PageService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});
builder.Services.AddHttpClient<IEventService, EventService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});
builder.Services.AddScoped<SharedService>();
builder.Services.AddScoped<HonorSharedService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IBlogPageService, BlogPageService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});


builder.Services.AddScoped<IJSService, JSService>();


builder.Services.AddHttpClient<IFileManager, FileManager>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
