using System.Reflection;
using Example.EfCore;
using FlgMl.Microservice.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlgMl.Microservice.Persistence;

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