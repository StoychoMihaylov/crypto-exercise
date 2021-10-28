using Database.Entities;
using Database.interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CoinValueService : Service, ICoinValueService
    {
        public CoinValueService(IApplicationDBContext context)
            : base(context)
        {

        }

        public async Task<ICollection<CoinValue>> GetCounValues(int userId, string coin, DateTime? from, DateTime? to)
        {
            var coinValues = await this.Context.CoinValues
                .AsNoTracking()
                .Where(x => x.Time >= from && x.Time <= to.Value)
                .ToListAsync();

            return coinValues;
        }
    }
}
