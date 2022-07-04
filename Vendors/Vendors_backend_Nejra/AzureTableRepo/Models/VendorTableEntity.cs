using Microsoft.Azure.Cosmos.Table;
namespace Vendors.AzureTablerepo.Models

{
    public class VendorTableEntity : TableEntity
    {

        public VendorTableEntity() { }

        public VendorTableEntity(string id)
        {
            this.PartitionKey = id;
            this.RowKey = id;
        }

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
