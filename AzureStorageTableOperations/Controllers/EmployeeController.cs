using Microsoft.AspNetCore.Mvc;
using AzureStorageTableOperations.Models.DomainModels;
using AutoMapper;
using AzureStorageTableOperations.Services;
using System.Collections.Generic;
using AzureStorageTableOperations.Models.ViewModels;
using Microsoft.Extensions.Logging;

namespace AzureStorageTableOperations.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IEmployeeService _employeeService;
		private readonly ILogger<EmployeeController> _logger;
		public EmployeeController(IMapper mapper, IEmployeeService employeeService, ILogger<EmployeeController> logger)
		{
			_mapper = mapper;
			_employeeService = employeeService;
			_logger = logger;
		}
		// GET: Employee
		public ActionResult Index()
		{
			var employees = _employeeService.GetEmployees();
			var employeesVm = _mapper.Map<List<EmployeeEntity>, List<EmployeeViewModel>>(employees);
			return View(employeesVm);
		}

		// GET: Employee/Details/5
		public ActionResult Details(string partitionKey, string rowKey)
		{
			var employeeSvcResponse = _employeeService.GetEmployeeByEmail(partitionKey, rowKey);
			if (employeeSvcResponse.Failed)
				_logger.LogWarning(employeeSvcResponse.ErrorData.ErrorMessage);

			var employeeVm = _mapper.Map<EmployeeEntity, EmployeeViewModel>(employeeSvcResponse.ResultData);
			return View(employeeVm);
		}

		// GET: Employee/Create
		public ActionResult Create()
		{
			return View(new EmployeeViewModel());
		}

		// POST: Employee/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(EmployeeViewModel model)
		{
			var employeeEntity = _mapper.Map<EmployeeViewModel, EmployeeEntity>(model);
			var employeeSvcResponse = _employeeService.CreateEmployee(employeeEntity);
			if (employeeSvcResponse.Failed)
			{
				_logger.LogWarning(employeeSvcResponse.ErrorData.ErrorMessage);
				return View();
			}
			else
				return RedirectToAction(nameof(Index));
		}

		// GET: Employee/Edit/5
		public ActionResult Edit(string partitionKey, string rowKey)
		{
			var employeeSvcResponse = _employeeService.GetEmployeeByEmail(partitionKey, rowKey);
			if (employeeSvcResponse.Failed)
				_logger.LogWarning(employeeSvcResponse.ErrorData.ErrorMessage);

			var employeeVm = _mapper.Map<EmployeeEntity, EmployeeViewModel>(employeeSvcResponse.ResultData);
			return View(employeeVm);
		}

		// POST: Employee/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EmployeeViewModel request)
		{
			var employeeEntity = _mapper.Map<EmployeeViewModel, EmployeeEntity>(request);
			var employeeSvcResponse = _employeeService.UpdateEmployee(employeeEntity);
			if (employeeSvcResponse.Failed)
			{
				_logger.LogWarning(employeeSvcResponse.ErrorData.ErrorMessage);
				return View();
			}
			else
				return RedirectToAction(nameof(Index));
		}

		// GET: Employee/Delete/5
		public ActionResult Delete(string partitionKey, string rowKey)
		{
			var employeeSvcResponse = _employeeService.GetEmployeeByEmail(partitionKey, rowKey);
			if (employeeSvcResponse.Failed)
				_logger.LogWarning(employeeSvcResponse.ErrorData.ErrorMessage);

			var employeeVm = _mapper.Map<EmployeeEntity, EmployeeViewModel>(employeeSvcResponse.ResultData);
			return View(employeeVm);
		}

		// POST: Employee/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string partitionKey, string rowKey)
		{
			var employeeSvcResponse = _employeeService.GetEmployeeByEmail(partitionKey, rowKey);
			if (employeeSvcResponse.Failed)
				_logger.LogWarning(employeeSvcResponse.ErrorData.ErrorMessage);

			var employeeSvcDelResponse = _employeeService.DeleteEmployeeByEmail(employeeSvcResponse.ResultData);
			if (employeeSvcDelResponse.Failed)
				_logger.LogWarning(employeeSvcDelResponse.ErrorData.ErrorMessage);
			return RedirectToAction("Index");
		}
	}
}
