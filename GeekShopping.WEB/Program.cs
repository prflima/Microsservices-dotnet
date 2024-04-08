using GeekShopping.WEB.Services.IServices;
using GeekShopping.WEB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add httpclient services
builder.Services.AddHttpClient<IProductService, ProductService>(
        c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:ProductAPI"])
    );


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
