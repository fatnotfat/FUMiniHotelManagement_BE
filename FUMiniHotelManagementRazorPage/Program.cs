using Repo.Interface;
using Repo;
using Service.Interface;
using Service;
using Services;
using Services.Interface;

namespace FUMiniHotelManagementRazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
            builder.Services.AddTransient<ICustomerServices, CustomerService>();
            builder.Services.AddTransient<IRoomInformation, RoomInformationRepo>();
            builder.Services.AddTransient<IRoomInformationService, RoomInformationService>();
            builder.Services.AddTransient<IAdminRepo, AdminRepo>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IStaffRepo, StaffRepo>();
            builder.Services.AddTransient<IStaffService, StaffService>();
            builder.Services.AddTransient<IHelper, Helper>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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
            app.UseSession();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
