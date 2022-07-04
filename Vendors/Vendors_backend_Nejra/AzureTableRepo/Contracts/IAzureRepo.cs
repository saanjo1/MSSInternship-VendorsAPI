namespace Vendors.AzureTablerepo.Contracts

{
    public interface IAzureRepo<T>
    {
        Task <string> Create(T record);
        Task<T> GetById(string recordId);
        
 
        Task<IEnumerable<T>> Read();

        Task<bool> Update(T record);
        Task<bool> Delete(string recordId);



    }
}
