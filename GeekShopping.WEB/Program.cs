using GeekShopping.WEB.Services.IServices;
using GeekShopping.WEB.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add httpclient services
builder.Services.AddHttpClient<IProductService, ProductService>(
        c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:ProductAPI"])
    );


// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure bearer
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
  .AddOpenIdConnect("oidc", opt =>
  {
      opt.Authority = builder.Configuration["ServicesUrls:Identity"];
      opt.GetClaimsFromUserInfoEndpoint = true;
      opt.ClientId = builder.Configuration["SecretsIdentity:ClientId"];
      opt.ClientSecret = builder.Configuration["SecretsIdentity:SecretKey"];
      opt.ResponseType = "code";
      opt.ClaimActions.MapJsonKey("role", "role", "role");
      opt.ClaimActions.MapJsonKey("sub", "sub", "sub");
      opt.TokenValidationParameters.NameClaimType = "name";
      opt.TokenValidationParameters.RoleClaimType = "role";
      opt.Scope.Add(builder.Configuration["ServicesUrls:Scope"]);
      opt.SaveTokens = true;
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
