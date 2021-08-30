using System;
using System.Collections.Generic;
using System.Linq;
using Core.Library.Models;
using Core.Library.Data;
using Core.Library.Enums;

namespace Core.Library.Services
{
	public class Service : IService
	{
		public ServiceRequest CreateNewService(ServiceRequestDto request)
		{
			var user = "Admin";

			var newService = new ServiceRequest
			{
				Id = Guid.NewGuid(),
				BuildingCode = request.BuildingCode,
				Description = request.Description,
				CurrentStatus = CurrentStatus.Created.ToString(),
				CreatedBy = user,
				CreatedDate = DateTime.Now.Date.ToShortDateString(),
				LastModifiedBy = user,
				LastModifiedDate = DateTime.Now.Date.ToShortDateString()
			};

			ServiceRequestData.ServiceRequestDataList.Add(newService);

			return newService;
		}

		public ServiceRequest GetServiceById(Guid id) => ServiceRequestData.ServiceRequestDataList.Where(x => x.Id == id).FirstOrDefault();

		public List<ServiceRequest> GetAllServices() => ServiceRequestData.ServiceRequestDataList.Select(x => x).ToList();

		public ServiceRequest UpdateService(Guid id)
		{
			var user = "JhonDoe";
			var service = ServiceRequestData.ServiceRequestDataList.Where(x => x.Id == id).FirstOrDefault();

			if (service != null)
			{
				service.CurrentStatus = CurrentStatus.InProgress.ToString();
				service.LastModifiedBy = user;
				service.LastModifiedDate = DateTime.Now.Date.ToShortDateString();
			}

			return service;
		}

		public bool DeleteService(Guid id)
		{
			var service = ServiceRequestData.ServiceRequestDataList.SingleOrDefault(x => x.Id == id);

			if (service != null)
			{
				ServiceRequestData.ServiceRequestDataList.Remove(service);
				return true;
			}

			return false;
		}
	}
}
