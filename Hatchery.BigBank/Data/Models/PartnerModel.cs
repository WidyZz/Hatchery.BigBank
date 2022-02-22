using System.ComponentModel.DataAnnotations;
using Hatchery.BigBank.Data.Entities;

namespace Hatchery.BigBank.Data.Models
{
    public class PartnerModel
    {
        [Key] public int PartnerId { get; set; }
        [Required] public string PartnerName { get; set; }
        [Required] public string IBAN { get; set; }
        public DateTime PartneredSince { get; set; }
        public DateTime? ClosedAt { get; set; }
        public ICollection<CalculatorModel>? Calculators { get; set; }
    }
}
