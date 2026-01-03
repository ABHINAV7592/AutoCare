using service_booking.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register services FIRST
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<UserDB>();

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserReg}/{action=Register}/{id?}");

app.Run();
