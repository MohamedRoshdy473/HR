using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System;
using HR.Domain.Services;
using HR.Domain.Repositories;
using HR.Domain;
using HR.Data.Models;
using HR.Core.Services;
using HR.Core.Repositories;
using HR.Core;
using HR.API.ConfirmationMail;

namespace HrAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public object JwtBearerDefaults { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddMvc();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );



            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // services.AddControllers();
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            // For Entity Framework  
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));

            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepositories>();
            services.AddTransient<IPositionsRepository, PositionsRepository>();
            services.AddTransient<IPositionsService, PositionsService>();
            services.AddTransient<IPositionLevelsRepository, PositionLevelsRepository>();
            services.AddTransient<IPositionsLevelService, PositionsLevelService>();
            services.AddTransient<IProfessionsRepository, ProfessionsRepository>();
            services.AddTransient<IProfessionsService, ProfessionsService>();
            services.AddTransient<IUniversityRepository, UniversityRepository>();
            services.AddTransient<IUniversityService, UniversityService>();
            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<IFacultyRepository, FacultyRepository>();
            services.AddTransient<IFacultyDepartmentService, FacultyDepartmentService>();
            services.AddTransient<IFacultyDepartmentRepository, FacultyDepartmentRepository>();
            services.AddTransient<IExcuseRepository, ExcuseRepository>();
            services.AddTransient<IExcuseService, ExcuseService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                   opt.TokenLifespan = TimeSpan.FromHours(2));
            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //for publishing
            //else
            //{
            //    app.Use(async (context, next) =>
            //    {
            //        await next();
            //        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
            //        {
            //            context.Request.Path = "/index.html";
            //            await next();
            //        }
            //    });

            //}
            app.UseCors(
             //options => options.WithOrigins("http://localhost:4200.com").AllowAnyMethod()
             options=>options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
           );
            app.UseStaticFiles(); // For the wwwroot folder.

           
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "wwwroot")),
                RequestPath = "/wwwroot",
                EnableDirectoryBrowsing = true
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
