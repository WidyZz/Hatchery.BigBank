﻿using System.ComponentModel.DataAnnotations;

namespace Hatchery.BigBank.Data.Entities
{
    public class LoanRequest
    {
        [Key] public int LoanRequestId { get; set; }
        [Required] public Partner Partner { get; set; }
        [Required] [Range(20000, 500000)] public decimal CreditValue { get; set; }
        [Required] [Range(6, 96)] public int DueDate { get; set; }
        [Required] public string PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
