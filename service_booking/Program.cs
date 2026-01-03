using service_booking.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register services FIRST
builder.Services.AddControllersWithViews();

// ✅ DB classes
builder.Services.AddScoped<UserDB>();
builder.Services.AddScoped<MechanicDB>();

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MechanicReg}/{action=Register}/{id?}");

app.Run();
