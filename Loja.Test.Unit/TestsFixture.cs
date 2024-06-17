using Microsoft.Extensions.DependencyInjection;
using Loja.Infra.Common.Initializers;
using Microsoft.Extensions.Hosting;
using Loja.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Loja.Test.Unit;

public class TestsFixture
{
    public AppDbContext DbContext;

    public TestsFixture()
    {
        var builder = new HostApplicationBuilder();

        builder.ConfigureEnvironment();
        builder.Services.AddDatabaseConfiguration();
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        DbContext = app.Services.GetRequiredService<AppDbContext>();
        DbContext.Database.Migrate();
    }
}
