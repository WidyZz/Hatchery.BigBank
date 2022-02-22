using System.ComponentModel.DataAnnotations;
using Hatchery.BigBank.Data.Entities;

namespace Hatchery.BigBank.Data.Models
{
    public class CalculatorModel
    {
        [Key] public int CalculatorId { get; set; }
        [Required] [Range(20000, 500000)] public decimal CreditValue { get; set; }
        [Required] [Range(6, 96)] public int DueDate { get; set; }
        [Required] public string PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
