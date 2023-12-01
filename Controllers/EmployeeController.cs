using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTWebApp.Application.Helpers;
using RESTWebApp.Application.Models;
using RESTWebApp.Application.Services.EmployeeService;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using System.Net.NetworkInformation;

namespace RESTWebApp.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeAppService _employeeAppService;

        public EmployeeController(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        [HttpGet("GetAllEmployee")]
        [Produces("application/json")]
        [Authorize] 
        public async Task<IActionResult> GetAllEmployee([FromQuery] PageInfo pageinfo)
        {
            try
            {
                var employeeList = await _employeeAppService.GetAllEmployees(pageinfo);
                if (employeeList.Data.Count() < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Not Found");
                }
                return Requests.Response(this, new ApiStatus(200), employeeList, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message); // not found
            }
        }

        [HttpGet("GetEmployeeByCode")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeByCode(string code)
        {
            try
            {
                var data = await _employeeAppService.GetEmployeeByCode(code);
                if (data == null)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Not Found");
                }

                return Requests.Response(this, new ApiStatus(200), data, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message); // not found
            }
        }

        [HttpGet("SearchEmployee")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchEmployee(string searchString, [FromQuery] PageInfo pageInfo)
        {
            try
            {
                var data = await _employeeAppService.SearchEmployee(searchString, pageInfo);
                if (data.Data.Count() < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Not Found");
                }

                return Requests.Response(this, new ApiStatus(200), data, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message); // not found
            }
        }

        [HttpDelete("DeleteEmployee")]
        [Authorize]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {
                var (isDeleted, isMessage) = await _employeeAppService.Delete(Id);
                if (!isDeleted)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }

                return Requests.Response(this, new ApiStatus(200), isMessage, "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPost("SaveEmployee")]
        [Authorize]
        public async Task<IActionResult> SaveEmployee([FromBody] CreateEmployeeDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _employeeAppService.Create(model);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }

                return Requests.Response(this, new ApiStatus(200), isMessage, "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPatch("UpdateEmployee")]
        [Authorize]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto model)
        {
            try
            {
                var (isUpdated, isMessage) = await _employeeAppService.Update(model);
                if (!isUpdated)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }

                return Requests.Response(this, new ApiStatus(200), isMessage, "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}
