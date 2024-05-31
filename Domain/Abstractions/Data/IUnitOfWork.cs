using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BegingTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}