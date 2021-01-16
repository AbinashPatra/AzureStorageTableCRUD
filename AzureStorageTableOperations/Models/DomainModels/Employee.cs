using Microsoft.Azure.Cosmos.Table;

namespace AzureStorageTableOperations.Models.DomainModels
{
    public class EmployeeEntity : TableEntity
    {
        public EmployeeEntity(string email)
        {
            this.PartitionKey = email; this.RowKey = email;
            this.Email = email;
        }
        public EmployeeEntity() { }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
