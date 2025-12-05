using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Accounting.Domain.DTO
{
    public class CompanyDTO
    {
        public int CompanyID { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
}
