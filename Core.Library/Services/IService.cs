using System;
using System.Collections.Generic;
using Core.Library.Models;

namespace Core.Library.Services
{
	public interface IService
	{
		ServiceRequest CreateNewService(ServiceRequestDto request);
		ServiceRequest GetServiceById(Guid id);
		List<ServiceRequest> GetAllServices();
		ServiceRequest UpdateService(Guid id);
		bool DeleteService(Guid id);
	}
}
