using Demo_App;
using Demo_App.Services;
using Demo_App.Services.Blogging;
using Demo_App.Services.BlogPage;
using Demo_App.Services.Events;
using Demo_App.Services.Pages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5ccXVVRmlfWUZ+WEs=");
builder.Services.AddSyncfusionBlazor();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddSingleton<BlogService>();
builder.Services.AddHttpClient<IBloggingService, BloggingService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});
builder.Services.AddHttpClient<ICustomMenuService, CustomMenuService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});
builder.Services.AddHttpClient<IPageService, PageService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});

builder.Services.AddHttpClient<IRowService, RowService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});

builder.Services.AddHttpClient<IContainerService, ContainerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});

builder.Services.AddHttpClient<IComponentService, ComponentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});

builder.Services.AddHttpClient<IComponentPropertyService, ComponentPropertyService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});

builder.Services.AddHttpClient<IEventService, EventService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});


builder.Services.AddHttpClient<IBlogPageService, BlogPageService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
});

builder.Services.AddScoped<IJSService, JSService>();
builder.Services.AddHttpClient<IFileManager, FileManager>(client =>
{
    client.BaseAddress = new Uri("http://localhost:7114/");
});



builder.Services.AddScoped<IJSService, JSService>();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = null;
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
