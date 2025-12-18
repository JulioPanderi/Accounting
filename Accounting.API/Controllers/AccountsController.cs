using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Microsoft.Identity.Client;
using System.Text.Json;

namespace Accounting.API.Controllers
{
    [Route("Accounting/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountsService;
        private readonly ILogger<AccountsController> logger;

        public AccountsController(ILogger<AccountsController> logger, IAccountsService accountsService)
        {
            this.accountsService = accountsService;
            this.logger = logger;
        }

        [HttpGet]
        //[Route("GetAll/")]
        [Route("GetAll/{companyID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AccountDTO>>> GetAllAsync(int companyID)
        {
            try
            {
                List<AccountDTO> retValue = await accountsService.GetAllAsync(companyID);
                if (!retValue.Any())
                    return NotFound();

                return Ok(retValue);
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetAll failed with given ID: {companyID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetAll failed with given ID: {companyID}, {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{companyID}/{accountID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDTO>> GetByIdAsync(int companyID, string accountID)
        {
            try
            {
                AccountDTO? retValue = await accountsService.GetByIdAsync(companyID, accountID);
                if (retValue is null)
                    return NotFound();

                return Ok(retValue);
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetAll failed with given IDs: Company {companyID}, Account {accountID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetAll failed with given IDs: Company {companyID}, Account {accountID}, {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromBody] AccountDTO accountDTO)
        {
            try
            {
                await accountsService.UpdateAsync(accountDTO);
                return Ok();
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
        public async Task<ActionResult> AddAsync([FromBody] AccountDTO accountDTO)
        {
            try
            {
                await accountsService.AddAsync(accountDTO);
                return Ok();
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
        [Route("Delete/{companyID}/{accountID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAsync(int companyID, string accountID)
        {
            try
            {
                await accountsService.DeleteAsync(companyID, accountID);
                return Ok();
            }
            catch (FormatException exception)
            {
                logger.LogError($"Delete failed: companyID {companyID} accountID {accountID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"Delete failed: companyID {companyID} accountID {accountID}, {exception}");
                return BadRequest(exception.Message);
            }
        }
    }
}