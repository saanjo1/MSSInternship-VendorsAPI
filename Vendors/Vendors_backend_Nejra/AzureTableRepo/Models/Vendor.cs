
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Vendors.AzureTablerepo.Models

{

    public class Vendor
    {
        public string? VendorId { get; set; }
        public string? VendorName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? AdditionalComment { get; set; }
    }
}
