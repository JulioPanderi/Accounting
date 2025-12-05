using Accounting.Application.Interfaces;
using Accounting.Application.Services;
using Accounting.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Microsoft.Identity.Client;
using System.Text.Json;

namespace Accounting.API.Controllers
{
    [ApiController]
    [Route("Accounting/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinsService coinsService;
        private readonly ILogger<CoinsController> logger;

        public CoinsController(ILogger<CoinsController> logger, ICoinsService coinService)
        {
            this.coinsService = coinService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CoinDTO>>> GetAllAsync()
        {
            try
            {
                List<CoinDTO> retValue = await coinsService.GetAllAsync();
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
        [Route("GetById/{coinID}")]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CoinDTO>> GetByIdAsync(short coinID)
        {
            try
            {
                CoinDTO? retValue = await coinsService.GetByIdAsync(coinID);
                if (retValue is null)
                    return NotFound();

                return Ok(retValue);
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetAll failed with given IDs: CoinID {coinID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetAll failed with given IDs: CoinID {coinID}, {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromBody] CoinDTO coinDTO)
        {
            try
            {
                int rowsAffected = await coinsService.UpdateAsync(coinDTO);
                return (rowsAffected == 0 ? NotFound() : Ok() );
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
        public async Task<ActionResult> AddAsync([FromBody] CoinDTO coinDTO)
        {
            try
            {
                short coinID = await coinsService.AddAsync(coinDTO);
                return Ok(coinID);
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
        [Route("Delete/{coinID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAsync(short coinID)
        {
            try
            {
                int rowsAffected = await coinsService.DeleteAsync(coinID);
                return (rowsAffected == 0 ? NotFound() : Ok() );
            }
            catch (FormatException exception)
            {
                logger.LogError($"Delete failed: coinID {coinID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"Delete failed: coinID {coinID}, {exception}");
                return BadRequest(exception.Message);
            }
        }
    }
}
