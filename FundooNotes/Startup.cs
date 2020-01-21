//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz" Author="Jayashree sawakare">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FundooBusinessLayer.Interfaces;
  using FundooBusinessLayer.Services;
  using FundooRepositoryLayer.Interfaces;
  using FundooRepositoryLayer.ModelDB;
  using FundooRepositoryLayer.Services;
  using Microsoft.AspNetCore.Authentication.JwtBearer;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.HttpsPolicy;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using Microsoft.IdentityModel.Tokens;
  using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// This is the startup class.
    /// </summary>
    public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container
    /// </summary>
    /// <param name="services"></param>
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
       options.TokenValidationParameters = new TokenValidationParameters
       {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = Configuration["Jwt:Issuer"],
         ValidAudience = Configuration["Jwt:Issuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
       };
     });
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddDbContext<UserContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:UserDB"]));
      services.AddScoped<IUserBusiness, UserBusiness>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<INotesBusiness, NotesBusiness>();
      services.AddScoped<INotesRepository, NotesRepository>();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new ApiKeyScheme
        {
          Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
          In = "header",
          Name = "Authorization",
          Type = "apiKey"
        });
        c.DocumentFilter<SecurityRequirementDocumentFilter>();
      });

    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      app.UseAuthentication();
      app.UseHttpsRedirection();
      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(
        c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", " My API v1");
        });

    }
  }
  public class SecurityRequirementDocumentFilter:IDocumentFilter
  {
    public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
    {
      swaggerDoc.Security = new List<IDictionary<string, IEnumerable<string>>>()
      {
        new Dictionary<string,IEnumerable<string>>()
        {
          {"Bearer",new string[]{ } },
          {"Basic",new string[]{ } },
        }
      };
    }
  }
}
