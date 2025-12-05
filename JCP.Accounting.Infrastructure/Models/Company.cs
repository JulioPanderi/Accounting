using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Models
{
    public class Company
    {
        [Key]
        public int CompanyID {get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LegalName { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false)]
        public string TradeName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public int CountryID { get; set; }
        [Required]
        public Country Country { get; set; }
    }
}
