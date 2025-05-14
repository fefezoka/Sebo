using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SEBO.API.Data;
using SEBO.API.Domain.Entities.IdentityAggregate;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SEBO.API.IoC.Modules
{
    public static class IdentityModule
    {
        public static void AddAuthentication(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<SEBOContext>()
                .AddDefaultTokenProviders();

            var jwtOptionsSettings = configuration.GetSection(nameof(ApplicationJwtOptions));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("ApplicationJwtOptions:SecurityKey").Value));

            builder.Configure<ApplicationJwtOptions>(options =>
            {
                options.Issuer = jwtOptionsSettings[nameof(ApplicationJwtOptions.Issuer)] ?? "";
                options.Audience = jwtOptionsSettings[nameof(ApplicationJwtOptions.Audience)] ?? "";
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                options.AccessTokenExpiration = int.Parse(jwtOptionsSettings[nameof(ApplicationJwtOptions.AccessTokenExpiration)] ?? "0");
                options.RefreshTokenExpiration = int.Parse(jwtOptionsSettings[nameof(ApplicationJwtOptions.RefreshTokenExpiration)] ?? "0");
            });

            builder.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
            });

            builder.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(30);
            });

            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            builder.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameter;
            });

        }
    }
}
