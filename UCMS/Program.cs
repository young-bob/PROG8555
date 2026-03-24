using UCMS.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFileLogger("log.txt");

builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<UCMS.Filters.FooterFilter>();
    opt.Filters.Add<UCMS.Filters.GlobalExceptionFilter>();
});
builder.Services.AddScoped<UCMS.Filters.ActivityLogFilter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
