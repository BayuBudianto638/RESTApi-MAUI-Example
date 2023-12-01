using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RESTWebApp.Application.ConfigProfile;
using RESTWebApp.Application.Services.EmployeeService;
using RESTWebApp.Data.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.UnitTesting
{
    public class Startup
    {
        public Startup()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<HumanResourcesContext>(option =>
                    option.UseInMemoryDatabase("Server=FAIRUZ-PC\\SQLEXPRESS;Database=SalesDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ConfigurationProfile());
            });
            var mapper = config.CreateMapper();

            // Add services to the container.
            serviceCollection.AddSingleton(mapper);
            serviceCollection.AddTransient<IEmployeeAppService, EmployeeAppService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
