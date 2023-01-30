using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Business.Concrete;
using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories;

namespace TosunlarYapiMarket.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSqlAndCookie(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<TosunlarYapiMarketDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            serviceCollection.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/User/Login/GirisYap");
                opt.LogoutPath = new PathString("/User/Login/CikisYap");
                opt.Cookie = new CookieBuilder
                {
                    Name = "TosunlarYms",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(14);
                opt.AccessDeniedPath = new PathString("/User/AccessDenied");

            });

            return serviceCollection;
        }

        public static IServiceCollection AddMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICustomerRepository, EfCustomerRepository>();
            serviceCollection.AddScoped<IDebtRepository, EfDebtRepository>();
            serviceCollection.AddScoped<IStockRepository, EfStockRepository>();
            serviceCollection.AddScoped<INoteRepository, EfNoteRepository>();
            serviceCollection.AddScoped<IInvoiceRepository, EfInvoiceRepository>();
            serviceCollection.AddScoped<IPayOffDebtRepository,EfPayOfDebtRepository>();
            serviceCollection.AddScoped<IStockDetailRepository, EfStockDetailRepository>();
            serviceCollection.AddScoped<IStockBasketRepository, EfStockBasketRepository>();

            serviceCollection.AddScoped<ICustomerService, CustomerManager>();
            serviceCollection.AddScoped<IDebtService, DebtManager>();
            serviceCollection.AddScoped<IStockService, StockManager>();
            serviceCollection.AddScoped<INoteService, NoteManager>();
            serviceCollection.AddScoped<IInvoiceService, InvoiceManager>();
            serviceCollection.AddScoped<IPayOffDebtService, PayOffDebtManager>();
            serviceCollection.AddScoped<IStockDetailService, StockDetailManager>();
            serviceCollection.AddScoped<IStockBasketService, StockBasketManager>();

            return serviceCollection;
        }
    }
}
