using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Database.interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<PortfolioEntry> PortfolioEntries { get; set; }

        DbSet<CoinValue> CoinValues { get; set; }

        DbSet<PortfolioValue> PortfolioValues { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
