using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Recyclable.Controllers;
using Recyclable.Data;
using Recyclable.Interface;
using Recyclable.Repository;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

 
builder.Services.AddControllersWithViews();
// For data context
builder.Services.AddDbContext<Data>(options =>
            options.UseSqlServer(connectionString));


// For connection string dependency injection
builder.Services.AddScoped<IRecyclableTypeRepository, RecyclableTypeRepository>(provider =>
{
    return new RecyclableTypeRepository(connectionString);
});
builder.Services.AddScoped<IRecyclableItemRepository, RecyclableItemRepository>(provider =>
{
    return new RecyclableItemRepository(connectionString);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RecyclableType}/{action=List}/{id?}");

app.Run();
