using AutoMapper;
using Microsoft.Azure.Cosmos.Table;
using Vendors.AzureTablerepo.Contracts;
using Vendors.AzureTablerepo.Models;

namespace Vendors.AzureTablerepo.Services

{
    public class AzureTableRepoVendors : IAzureRepo<Vendor>
    {
        private string _connectionString;
        private string _tableName;
        private CloudTableClient _tableClient;
        private CloudTable _table;
        private readonly IMapper _mapper;

        public AzureTableRepoVendors(string connectionString, string tableName, IMapper mapper)
        {
            _connectionString = connectionString?? throw new ArgumentNullException("connectionString is null");
            _tableName = tableName ?? throw new ArgumentNullException("tableName is null");
            CloudStorageAccount storageAccount =
              CloudStorageAccount.Parse(connectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
            _table = _tableClient.GetTableReference(tableName);
            _table.CreateIfNotExists();
            _mapper = mapper;
        }

        public async Task<string> Create(Vendor vendor)
        {
            vendor.VendorId = Guid.NewGuid().ToString();
            VendorTableEntity vendorEntity = new VendorTableEntity(vendor.VendorId);
            _mapper.Map(vendor, vendorEntity);
            TableOperation insertOperation = TableOperation.InsertOrReplace(vendorEntity);
            var tableresult = _table.Execute(insertOperation);
            
            return await Task.FromResult(tableresult.HttpStatusCode==204? "vendors successfully created": "vendors can not create");
        }

        public async Task<bool> Delete(string VendorId)
        {
            try
            {
                VendorTableEntity vendorEntity = new VendorTableEntity
                {
                    PartitionKey = VendorId,
                    RowKey = VendorId,
                    ETag = "*"
                };

                TableOperation deleteOperation = TableOperation.Delete(vendorEntity);
                var tableResult = _table.Execute(deleteOperation);
                return tableResult.HttpStatusCode == 204 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Vendor> GetById(string recordId)
        {
            var filter = $"PartitionKey eq '{recordId}' ";
            var query = new TableQuery<VendorTableEntity>().Where(filter);
            
            var lst = _table.ExecuteQuery(query);
            var returnResult = lst.Select(x => _mapper.Map<Vendor>(x)).FirstOrDefault();
            return returnResult;
        }

        public async Task<IEnumerable<Vendor>> Read()
        {
            var query = new TableQuery<VendorTableEntity>();
            var lst = _table.ExecuteQuery(query);
            var result = lst.Select(x => _mapper.Map<Vendor>(x));

            return result;
        }

        public async Task<bool> Update(Vendor vendor)
        {
            VendorTableEntity vendorEntity = new VendorTableEntity(vendor.VendorId);
            _mapper.Map(vendor, vendorEntity);

            TableOperation insertOperation = TableOperation.InsertOrReplace(vendorEntity);

            var operationResult= _table.Execute(insertOperation);
            return operationResult.HttpStatusCode == 204 ? true : false;
        }
    }
}
