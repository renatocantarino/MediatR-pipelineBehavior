using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using pipelineBehavior.Infra.Behavior;
using pipelineBehavior.Infra.Exceptions;
using pipelineBehavior.Validations;

namespace pipelineBehavior
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var presentationAssembly = typeof(Infra.AssemblyReference).Assembly;

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(presentationAssembly));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "pipelineBehavior", Version = "v1" });
            });

            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddMediatR(presentationAssembly);
            services.AddTransient(typeof(IPipeline<,>), typeof(PipelineValidator<,>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "pipelineBehavior v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}