using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class PortfolioEntry
    {
        [Key]
        public int Id { get; set; }

        public DateTime LastChanged { get; set; }

        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string Coin { get; set; }

        [Required]
        public decimal Quantitiy { get; set; }
    }
}