
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IDbTransaction BegingTransactionAsync()
    {
        var transaction = _context.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }

    public Task CommitTransactionAsync()
    {
        var transaction = _context.Database.CommitTransactionAsync();

        return transaction;
    }

    public Task RollbackTransactionAsync()
    {
        var transaction = _context.Database.RollbackTransactionAsync();
        return transaction;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }


    //protected override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken.None)
    //{
    //    var domainEvents = ChangeTracker.Entries<Entity>()
    //        .Select(e => e.Entity)
    //        .Where(e => e.GetDomainEvents().Any())
    //        .SelectMany(e => e.DomainEvents());

    //    var result = await base.SaveChangesAsync(cancellationToken);

    //    foreach (var domainEvent in domainEvents)
    //    {
    //       //wait _publisher.Publish(domainEvent, cancellationToken);
    //    }

    //    return result;
    //}
}
