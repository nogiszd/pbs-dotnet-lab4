using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using WinLab4.Infrastructure.Persistence.Descriptions;
using WinLab4.Models;

namespace WinLab4.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.DescribeUser();
        builder.DescribeEvent();
        builder.DescribeReservation();

        DisableGeneratedKey(builder);
    }

    private static void DisableGeneratedKey(ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes().Where(x => x.ClrType.IsAssignableTo(typeof(IEntity))))
        {
            entity.FindProperty(nameof(IEntity.Id))!.ValueGenerated = ValueGenerated.Never;
        }
    }
}
