using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TARge21House.ApplicationServices.Services;
using TARge21House.Core.ServiceInterface;
using TARge21House.Data;
using TARge21House.HouseTest.Macros;
using TARge21House.HouseTest.Mock;

namespace TARge21House.HouseTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; }

        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
        }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        protected T Macro<T>() where T : IMacros
        {
            return serviceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IHouseServices, HouseServices>();
            services.AddScoped<IHostEnvironment, MockIHostEnvironment>();

            services.AddDbContext<TARge21HouseContext>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(x => macroBaseType.IsAssignableFrom(x) && !x.IsInterface
                && !x.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }
    }
}
