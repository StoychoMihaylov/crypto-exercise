using System;
using System.Collections.Generic;

namespace Database.Entities
{
    public class User
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; }
        public List<PortfolioEntry> PortfolioEntries { get; set; }

        public virtual ICollection<CoinValue> CoinValue { get; set; }

        public virtual ICollection<PortfolioValue> PortfolioValue { get; set; }
    }
}