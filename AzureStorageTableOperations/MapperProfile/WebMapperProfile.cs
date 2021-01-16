using AutoMapper;
using AzureStorageTableOperations.Models.DomainModels;
using AzureStorageTableOperations.Models.ViewModels;

namespace AzureStorageTableOperations.MapperProfile
{
	public class WebMapperProfile : Profile
	{
		public WebMapperProfile()
		{
			CreateMap<EmployeeEntity, EmployeeViewModel>();
			CreateMap<EmployeeViewModel, EmployeeEntity>();
		}
	}
}
