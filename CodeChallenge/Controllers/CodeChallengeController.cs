using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Core.Library.Models;
using Core.Library.Data;
using Core.Library.Services;

namespace CodeChallenge.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class codeChallengeController : ControllerBase
	{
		private readonly IService _service;

		public codeChallengeController(IService service)
		{
			_service = service;
		}

		[HttpGet]
		public ActionResult<IEnumerable<ServiceRequestData>> ServiceRequest()
		{
			List<ServiceRequest> allServices = _service.GetAllServices();

			if (allServices == null || allServices.Count == 0)
			{
				return NoContent();
			}

			return Ok(allServices);
		}

		[HttpGet("{id}")]
		public ActionResult<string> ServiceRequest(Guid id)
		{
			ServiceRequest service = _service.GetServiceById(id);

			if (service == null)
			{
				return NotFound();
			}

			return Ok(service);
		}

		[HttpPost]
		public IActionResult ServiceRequest([FromBody] ServiceRequestDto request)
		{
			if (string.IsNullOrEmpty(request.BuildingCode) || string.IsNullOrEmpty(request.Description))
			{
				return BadRequest();
			}

			ServiceRequest newService = _service.CreateNewService(request);

			return CreatedAtAction(nameof(ServiceRequest), newService);		
		}

		[HttpPut("{id}")]
		public IActionResult ServiceRequest(Guid id, [FromForm] Guid value)
		{
			if (id == null)
			{
				return BadRequest();
			}

			ServiceRequest service = _service.UpdateService(id);

			if (service == null)
			{
				return NotFound();
			}

			return Ok(service);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteServiceRequest(Guid id)
		{
			bool service = _service.DeleteService(id);

			if (!service)
			{
				return NotFound();
			}

			return Ok();
		}
	}
}
