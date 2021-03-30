using System;
using Zoo.Areas.Identity.Data;
using Zoo.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Zoo.Areas.Identity.IdentityHostingStartup))]
namespace Zoo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ZooAuthContext>(options => options.UseSqlServer(
                context.Configuration["ConnectionStrings:DbConnection"]));

                services.AddDefaultIdentity<ZooUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                })
            .AddEntityFrameworkStores<ZooAuthContext>();
        });
        }
    }
}