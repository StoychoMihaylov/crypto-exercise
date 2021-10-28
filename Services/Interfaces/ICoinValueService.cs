using Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICoinValueService
    {
        Task<ICollection<CoinValue>> GetCounValues(int userId, string coin, DateTime? from, DateTime? to);
    }
}
