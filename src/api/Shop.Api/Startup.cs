using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Api.Api.Attributes;
using Shop.Api.Api.Middleware;
using Shop.Api.Api.Model.Mapping;
using Shop.Api.Business;
using Shop.Api.Data;
using Shop.Api.Data.Model;
using Shop.Api.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Shop.Api
{
    public class Startup
    {
        private const string SwaggerApiVersion = "v1";

        private readonly IConfiguration _configuration;
        private IServiceProvider _serviceProvider;

        public Startup(WebHostBuilderContext context)
        {
            _configuration = context.Configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite();
            services.AddSingleton(_configuration);
            services.AddMvc(o => o.Filters.Add(typeof(ErrorFormattingAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            AddSwagger(services);
            AddDatabase(services);
            AddAutomapperProfiles(services);

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketBusinessComponent, BasketBusinessComponent>();
            services.AddScoped<IProductBusinessComponent, ProductBusinessComponent>();

            services.AddSingleton<IProductSortExpressionMapper, ProductSortExpressionMapper>();

            _serviceProvider = services.BuildServiceProvider();
            return _serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ShopDbInitializer>()
                .Initialize().Wait();

            ValidateModelAttribute.Mapper = serviceProvider.GetService<AutoMapper.IMapper>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/api", apiApp =>
            {
                app.UseMiddleware<DummyAuthenticationMiddleware>();
                app.UseMvc();
            });

            UseSwagger(app);
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseWelcomePage();
        }

        private void AddAutomapperProfiles(IServiceCollection services)
        {
            services.AddSingleton<AutoMapper.IConfigurationProvider>(new AutoMapper.MapperConfiguration(cfg => cfg.AddProfiles(GetType().Assembly)));
            services.AddSingleton<AutoMapper.IMapper, AutoMapper.Mapper>();
        }

        private void AddDatabase(IServiceCollection services)
        {
            var connectionString = GetConnectionString(_configuration);

            services.AddDbContext<ShopDbContext>(options => { options.UseSqlite(connectionString); });
            services.AddSingleton<ShopDbInitializer>();
            services.AddScoped<IShopDbTransactionManager, ShopDbTransactionManager>();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc(SwaggerApiVersion, new Info { Title = "Shop API", Version = SwaggerApiVersion }); });
        }

        private void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/{SwaggerApiVersion}/swagger.json", $"Shop API {SwaggerApiVersion}"); });
        }

        private string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(ShopDbContext));
            var dataDirectory = new FileInfo(new Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath).Directory?.FullName;
            connectionString = connectionString.Replace("|DataDirectory|", dataDirectory);
            return connectionString;
        }
    }
}
