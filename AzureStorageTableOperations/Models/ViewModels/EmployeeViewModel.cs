namespace AzureStorageTableOperations.Models.ViewModels
{
	public class EmployeeViewModel
	{
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		//Identifiers
		public string PartitionKey { get; set; }
		public string RowKey { get; set; }
	}
}
