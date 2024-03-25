using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Infrastructures.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRealEstateRepository, RealEstateRepository>();
            services.AddScoped<IRealEstateImageRepository, RealEstateImageRepository>();

            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsImageRepository, NewsImageRepository>();

            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IRealtimeAuctionRepository, RealtimeAuctionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRealEstateService, RealEstateService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IRealtimeAuctionService, RealtimeAuctionService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IFirebaseService, FirebaseService>();
            services.AddScoped<FirebaseService>();

            services.AddSingleton<ICurrentTime, CurrentTime>();

            // ATTENTION: if you do migration please check file README.md
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(databaseConnection));

            // this configuration just use in-memory for fast develop
            //services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("test"));

            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            return services;
        }
    }
}
