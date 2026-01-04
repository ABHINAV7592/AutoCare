using service_booking.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddScoped<UserDB>();
builder.Services.AddScoped<MechanicDB>();
builder.Services.AddScoped<VehicleRegDB>();
builder.Services.AddScoped<ServiceDB>();
builder.Services.AddScoped<SlotDB>();
builder.Services.AddScoped<BookingDB>();


var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
