using AutoMapper;
using Vendors.AzureTablerepo.Models;

namespace Vendors.MappingProfiles
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, VendorTableEntity>().ReverseMap();
        }
    }
}
