using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Models;
using Zupa.Test.Booking.Services;

namespace Zupa.Test.Booking
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
            services.AddSingleton<IBasketsRepository, InMemoryBasketsRepository>();
            services.AddSingleton<IBasketsService, BasketsService>();
            services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
            services.AddSingleton<IRedeemCodesRepository, InMemoryRedeemCodesRepository>();
            services.AddSingleton<ICalcEngineService<Basket>, CalcEnginePromoCodeService>();
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Booking API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API V1");
            });

            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
