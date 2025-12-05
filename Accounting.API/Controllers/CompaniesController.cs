using Accounting.Application.Interfaces;
using Accounting.Application.Services;
using Accounting.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Microsoft.Identity.Client;
using System.Text.Json;

namespace Accounting.API.Controllers
{
    [Route("Accounting/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesService companiesService;
        private readonly ILogger<CompaniesController> logger;

        public CompaniesController(ILogger<CompaniesController> logger, ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CompanyDTO>>> GetAllAsync()
        {
            try
            {
                List<CompanyDTO> retValue = await companiesService.GetAllAsync();
                if (!retValue.Any())
                    return NotFound();

                return Ok(retValue);
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetAll failed: {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetAll failed: {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CompanyDTO>>> GetByFilterAsync(string name)
        {
            try
            {
                List<CompanyDTO> retValue = await companiesService.GetByFilterAsync(name);
                if (!retValue.Any())
                    return NotFound();

                return Ok(retValue);
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetByFilterAsync failed: name {name}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetByFilterAsync failed: name {name}, {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{companyID}")]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompanyDTO>> GetByIdAsync(int companyID)
        {
            try
            {
                CompanyDTO? retValue = await companiesService.GetByIdAsync(companyID);
                if (retValue is null)
                    return NotFound();

                return Ok(retValue);
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetAll failed with given IDs: CompanyID {companyID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetAll failed with given IDs: CompanyID {companyID}, {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromBody] CompanyDTO companyDTO)
        {
            try
            {
                int rowsAffected = await companiesService.UpdateAsync(companyDTO);
                return (rowsAffected == 0 ? NotFound() : Ok());
            }
            catch (FormatException exception)
            {
                logger.LogError($"Update failed: {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"Update failed: {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAsync([FromBody] CompanyDTO companyDTO)
        {
            try
            {
                await companiesService.AddAsync(companyDTO);
                return Ok();
            }
            catch (FormatException exception)
            {
                logger.LogError($"Add failed: {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"Add failed: {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Delete/{companyID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAsync(int companyID)
        {
            try
            {
                int rowsAffected = await companiesService.DeleteAsync(companyID);
                return (rowsAffected == 0 ? NotFound() : Ok());
            }
            catch (FormatException exception)
            {
                logger.LogError($"Delete failed: companyID {companyID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"Delete failed: companyID {companyID}, {exception}");
                return BadRequest(exception.Message);
            }
        }

    }
}
