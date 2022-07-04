using Microsoft.AspNetCore.Mvc;
using Vendors.AzureTablerepo.Contracts;
using Vendors.AzureTablerepo.Models;

namespace Vendors.Controllers
{
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private IAzureRepo<Vendor> _azureRepo;

        public VendorsController( IAzureRepo<Vendor> azureRepo)
        {
            _azureRepo = azureRepo; 
        }

        [HttpGet]
        [Route("api/[action]")]

        public async Task<IActionResult> Get()
        {
            var createResponse = await _azureRepo.Read();
            return Ok(createResponse);
        }

        [HttpGet]
        [Route("api/[action]")]

        public async Task<IActionResult> GetById(string Id)
        {
            var createResponse = await _azureRepo.GetById(Id);
            return Ok(createResponse);
        }


        [HttpPost]
        [Route("api/[action]")]
        public async Task<IActionResult> Post(Vendor vendor)
        {
            Console.WriteLine("Entered post");
            var createResponse=await _azureRepo.Create(vendor);
            return Ok(createResponse);
            
        }
  
        [HttpPut]
        [Route("api/[action]")]

        public async Task<IActionResult> Put(Vendor vendor)
        {
            var createResponse = await _azureRepo.Update(vendor);
            return Ok(createResponse);
        }

        [HttpDelete]
        [Route("api/[action]")]

        public async Task<IActionResult> Delete(string VendorId)
        { 
            var result = await _azureRepo.Delete(VendorId);
            string createResponse;
            if (result == true)
            {
                createResponse = "Vendor is deleted successfully.";
            }
            else
            {
                createResponse = "An error occurred while deleting a vendor .";
            }
            return Ok(createResponse);

        }
    }
}
