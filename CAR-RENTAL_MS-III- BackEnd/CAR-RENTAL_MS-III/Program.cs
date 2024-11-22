
using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Mapping;
using CAR_RENTAL_MS_III.Repositories;
using CAR_RENTAL_MS_III.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CAR_RENTAL_MS_III
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString
                ("CarRentalConnection")));




            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICarService,CarService>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService,CustomerService>();

            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            builder.Services.AddScoped<IManagerService,ManagerService>();

            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();



            builder.Services.AddAutoMapper(typeof(MappingProfile));



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });







            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
