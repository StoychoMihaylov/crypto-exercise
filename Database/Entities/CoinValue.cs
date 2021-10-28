using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class CoinValue
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime Time { get; set; }

        public decimal Value { get; set; }

        public string Coin { get; set; }

        public virtual User User { get; set; }
    }
}
