using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFetchService
    {
        Task<ICollection<CoinPriceDTO>> RequestGetCoinPrices(string[] coins);
    }
}
