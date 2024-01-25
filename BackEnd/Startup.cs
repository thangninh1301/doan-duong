using BackEnd.Data;
using BackEnd.IService;
using BackEnd.IServices.Base;
using BackEnd.IServices.IUserServices;
using BackEnd.Models;
using BackEnd.Service;
using BackEnd.Service.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Services.UserServices;
using BackEnd.IServices.IDoctorTestService;
using BackEnd.Services.DoctorTestService;
using BackEnd.Services.Base;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                 .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddScoped<IApointmentTicket, ApointmentTicketService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddScoped<ITimeSlot, TimeSlotService>();
            services.AddScoped<IUserApointmentTicket, UserApointmentTicketService>();
            services.AddScoped<IUserRegisterTicket, UserRegisterTicketService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAdminthongke, Adminthongke>();
            services.AddScoped<IRegisterTicketAdmin, RegisterTicketServiceAdmin>();
            services.AddScoped<IResult, SqlResult>();
            services.AddScoped<IDoctorTestService, DoctorTestService>();
            services.AddScoped<IApplicationUser, ApplicationUserService>();
            services.AddScoped<IRole, RoleService>();
            services.AddScoped<IUserRole, UserRoleService>();
            services.AddScoped<IAdminService, Adminservice>();
            services.AddScoped<ITest, TestService>();
            services.AddScoped<IResultDetail, ResultDetailService>();

            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                );
            services.AddTransient<AppDBContext>();
            services.AddIdentityCore<ApplicationUser>(options => { })
                   .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<AppDBContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEnd", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
