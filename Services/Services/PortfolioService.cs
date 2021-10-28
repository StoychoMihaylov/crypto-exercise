using Database.Entities;
using Database.interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PortfolioService : Service, IPortfolioService
    {
        public PortfolioService(IApplicationDBContext context)
            : base(context)
        {}

        public async Task<ICollection<PortfolioEntry>> GetPortfolioEntities(int userId)
        {
            var portfolioEntries = await this.Context.PortfolioEntries
                .Where(pv => pv.User.Id == userId)
                .ToListAsync();

            return portfolioEntries;
        }
    }
}
