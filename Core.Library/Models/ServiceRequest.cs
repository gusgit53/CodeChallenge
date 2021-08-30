using System;

namespace Core.Library.Models
{
	public class ServiceRequest
	{
		public Guid Id { get; set; }
		public string BuildingCode { get; set; }
		public string Description { get; set; }
		public string CurrentStatus { get; set; }
		public string CreatedBy { get; set; }
		public string CreatedDate { get; set; }
		public string LastModifiedBy { get; set; }
		public string LastModifiedDate { get; set; }
	}
}
