using DataAccessObject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;

namespace PRN231PE_SP24_123890_SE160385;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddODataConfiguraion(this IServiceCollection services)
    {
        var modelBuilder = new ODataConventionModelBuilder();
        // Add entity set / entity type
        modelBuilder.EntitySet<WatercolorsPainting>("WatercolorsPainting");
        // Add OData
        services.AddControllers().AddOData(options 
            => options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100)
                      .AddRouteComponents("odata", modelBuilder.GetEdmModel()));
        return services;
    }
    public static IServiceCollection AddJWTConfiguration(this IServiceCollection services,
        string secretKey)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                                    Encoding.UTF8.GetBytes(secretKey))
                    };
                });
        return services;
    }
}
