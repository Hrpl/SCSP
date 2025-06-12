
using SCSP.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace SCSP.API.Extensions;

public static class AddDbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            $"Host=85.208.87.10;Port=5432;Database=SCSP;Username=postgres;Password=2208;"
        ));
        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
