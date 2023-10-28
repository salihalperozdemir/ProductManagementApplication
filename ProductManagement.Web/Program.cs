
using ProductManagement.DAL.Interfaces;
using ProductManagement.DAL.Repositories;
using ProductManagement_DAL.Data;
using System.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProductManagement.Entities.Models;
using ProductManagement.Dto.Interfaces;
using ProductManagement.Dto.Repositories;
using ProductManagement.Business.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
    options.AddPolicy("SellerPolicy", policy => policy.RequireRole("Seller"));
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
