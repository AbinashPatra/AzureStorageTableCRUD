using AzureStorageTableOperations.Models;
using AzureStorageTableOperations.Models.DomainModels;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AzureStorageTableOperations.Services
{
	public class EmployeeService : IEmployeeService
	{
		private CloudTable _empTable;
		//private IConfiguration _configuration;
		//	private CloudTableClient _empTable;
		public EmployeeService(IConfiguration configuration)
		{
			//Can be read from Env variables as well
			string storageAccountConnectionStr = configuration["StorageConnectonString"];
			var storageAccountConnection = Microsoft.Azure.Cosmos.Table.CloudStorageAccount.Parse(storageAccountConnectionStr);
			//tables client
			var tableClient = storageAccountConnection.CreateCloudTableClient();
			//employee table connection
			_empTable = tableClient.GetTableReference("Employee");
			_empTable.CreateIfNotExists(); //TODO: Remove it, this really is not necessary always certainly not in Prod.
		}

		public ServiceResponse<bool> CreateEmployeeTable(EmployeeEntity request)
		{
			var response = new ServiceResponse<bool>();
			request.RowKey = request.PartitionKey = request.Email;
			try
			{
				TableOperation insertOperation = TableOperation.Insert(request);
				var result = _empTable.Execute(insertOperation);

				response.CreateSuccessResponse(true);
			}
			catch (StorageException ex)
			{
				response.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

			return response;
		}

		public ServiceResponse<bool> CreateEmployee(EmployeeEntity request)
		{
			var response = new ServiceResponse<bool>();
			request.RowKey = request.PartitionKey = request.Email;
			try
			{
				TableOperation insertOperation = TableOperation.Insert(request);
				var result = _empTable.Execute(insertOperation);

				response.CreateSuccessResponse(true);
			}
			catch (StorageException ex)
			{
				response.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

			return response;
		}

		public List<EmployeeEntity> GetEmployees()
		{
			return _empTable.ExecuteQuery(new TableQuery<EmployeeEntity>()).ToList();
		}

		public ServiceResponse<EmployeeEntity> GetEmployeeByEmail(string partitionKey, string rowKey)
		{

			var response = new ServiceResponse<EmployeeEntity>();
			try
			{
				//Using Linq
				//IQueryable<EmployeeEntity> linqQuery = _empTable.CreateQuery<EmployeeEntity>()
				//.Where(x => x.PartitionKey == partitionKey && x.RowKey == rowKey)
				//.Select(x => new EmployeeEntity() { PartitionKey = x.PartitionKey, RowKey = x.RowKey, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, PhoneNumber = x.PhoneNumber });
				//return linqQuery.FirstOrDefault();
				TableOperation retrieveOperation = TableOperation.Retrieve<EmployeeEntity>(partitionKey, rowKey);
				TableResult result = _empTable.Execute(retrieveOperation);

				response.CreateSuccessResponse(result.Result as EmployeeEntity);
			}
			catch (StorageException ex)
			{
				response.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

			//Log it
			return response;
		}

		public ServiceResponse<bool> DeleteEmployeeByEmail(EmployeeEntity entity)
		{

			var response = new ServiceResponse<bool>();
			try
			{
				//Using Linq
				//IQueryable<EmployeeEntity> linqQuery = _empTable.CreateQuery<EmployeeEntity>()
				//.Where(x => x.PartitionKey == partitionKey && x.RowKey == rowKey)
				//.Select(x => new EmployeeEntity() { PartitionKey = x.PartitionKey, RowKey = x.RowKey, Email = x.Email, FirstName = x.FirstName, LastName = x.LastName, PhoneNumber = x.PhoneNumber });
				//return linqQuery.FirstOrDefault();
				TableOperation retrieveOperation = TableOperation.Delete(entity);
				TableResult result = _empTable.Execute(retrieveOperation);

				response.CreateSuccessResponse(true);
			}
			catch (StorageException ex)
			{
				response.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

			//Log it
			return response;
		}

		public ServiceResponse<bool> UpdateEmployee(EmployeeEntity entity)
		{

			var response = new ServiceResponse<bool>();
			try
			{
				TableOperation retrieveOperation = TableOperation.InsertOrMerge(entity);
				TableResult result = _empTable.Execute(retrieveOperation);

				response.CreateSuccessResponse(true);
			}
			catch (StorageException ex)
			{
				response.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
			}

			//Log it
			return response;
		}
	}
}
