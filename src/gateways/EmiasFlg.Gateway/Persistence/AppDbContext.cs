using System.Reflection;
using EmiasFlgGateway.Domain;
using Example.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmiasFlgGateway.Persistence;

public class AppDbContext : BaseAuditableDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public AppDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }
    
    public DbSet<Study> Studies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}