using System.ComponentModel.DataAnnotations;

namespace Hatchery.BigBank.Data.Entities
{
    public class Partner
    {
        [Key] public int PartnerId { get; set; }
        [Required] public string PartnerName { get; set; }
        [Required] public string IBAN { get; set; }
        public DateTime PartneredSince { get; set; }
        public DateTime? ClosedAt { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<LoanRequest>? Calculators { get; set; }
    }
}
