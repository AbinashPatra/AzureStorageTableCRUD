using AzureStorageTableOperations.Models;
using AzureStorageTableOperations.Models.DomainModels;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureStorageTableOperations.Services
{
	public interface IEmployeeService
	{
		List<EmployeeEntity> GetEmployees();
		ServiceResponse<bool> CreateEmployee(EmployeeEntity request);
		ServiceResponse<EmployeeEntity> GetEmployeeByEmail(string partitionKey, string rowKey);
		ServiceResponse<bool> DeleteEmployeeByEmail(EmployeeEntity entity);
		ServiceResponse<bool> UpdateEmployee(EmployeeEntity entity);
	}
}
