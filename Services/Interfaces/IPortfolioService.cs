using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<ICollection<PortfolioEntry>> GetPortfolioEntities(int userId);
    }
}
