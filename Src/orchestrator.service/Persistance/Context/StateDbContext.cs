namespace orchestrator.service.Persistance.Context;

using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using orchestrator.service.Persistance.Configurations;
using orchestrator.service.Persistance.Entities;

public class StateDbContext : SagaDbContext
{
    public StateDbContext(DbContextOptions<StateDbContext> options) : base(options) { }

    public DbSet<OrderState> OrderStates { get; set; }

    public DbSet<PaymentState> PaymentStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StateDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new OrderStateEntityTypeConfiguration();
            yield return new PaymentStateEntityTypeConfiguration();
        }
    }
}

// Add-Migration CreateDatabase -OutputDir "Persistance/Migrations"
// Update-Database
