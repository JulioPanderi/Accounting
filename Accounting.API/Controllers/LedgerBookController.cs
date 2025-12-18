using Accounting.Application.Interfaces;
using Accounting.Application.Services;
using Accounting.Domain.DTO;
using Accounting.Shared.Filters;
using Accounting.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Accounting.API.Controllers
{
    [Route("LedgerBook/[controller]")]
    [ApiController]
    public class LedgerBookController : ControllerBase
    {
        private readonly ILedgerBookService ledgerBookService;
        private readonly ILogger<LedgerBookController> logger;

        public LedgerBookController(ILogger<LedgerBookController> logger, ILedgerBookService ledgerBookService)
        {
            this.ledgerBookService = ledgerBookService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAll/{companyID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<TransactionDTO>>> GetByDateRangeAsync(int companyID, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                TransactionsFilter filter = new TransactionsFilter
                {
                    IssueDateFrom = dateFrom,
                    IssueDateTo = dateTo
                };

                List<TransactionDTO> retValue = await ledgerBookService.GetByFilterAsync(companyID, filter);
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
        public async Task<ActionResult<TransactionDTO>> GetByIdAsync(int companyID, long transactionID)
        {
            try
            {
                TransactionsFilter filter = new TransactionsFilter
                {
                    TransactionID = transactionID
                };
                List<TransactionDTO> transactionsDTO = await ledgerBookService.GetByFilterAsync(companyID, filter);
                TransactionDTO? retValue = transactionsDTO.FirstOrDefault();
                return (retValue is null ? NotFound() : Ok(retValue));
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetAll failed with given IDs: Company {companyID}, TransactionID {transactionID}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetAll failed with given IDs: Company {companyID}, TransactionID {transactionID}, {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [Route("GetByNumber/{companyID}/{accountID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionDTO>> GetByNumberAsync(int companyID, int internalNumber)
        {
            try
            {
                TransactionsFilter filter = new TransactionsFilter
                {
                    InternalNumberFrom = internalNumber,
                    InternalNumberTo = internalNumber
                };
                List<TransactionDTO> transactionsDTO = await ledgerBookService.GetByFilterAsync(companyID, filter);
                TransactionDTO? retValue = transactionsDTO.FirstOrDefault();
                return (retValue is null ? NotFound() : Ok(retValue));
            }
            catch (FormatException exception)
            {
                logger.LogError($"GetAll failed with given IDs: Company {companyID}, Internal Number {internalNumber}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"GetAll failed with given IDs: Company {companyID}, Internal Number {internalNumber}, {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAsync([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                await ledgerBookService.AddAsync(transactionDTO);
                return Ok();
            }
            catch (FormatException exception)
            {
                logger.LogError($"Insert failed: {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"Insert failed: {exception}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("Delete/{companyID}/{accountID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAsync(long transactionID, string deleteObs)
        {
            try
            {
                AuditTransactionDTO audit = new AuditTransactionDTO();
                audit.TransactionID = transactionID;
                audit.AuditTransactionObservations = deleteObs;
                await ledgerBookService.DeleteAsync(transactionID, audit);
                return Ok();
            }
            catch (FormatException exception)
            {
                logger.LogError($"Delete failed: TransactionID {transactionID} deleteObs {deleteObs}, {exception}");
                return BadRequest(exception.Message);
            }
            catch (JsonException exception)
            {
                logger.LogError($"Delete failed: TransactionID {transactionID} deleteObs {deleteObs}, {exception}");
                return BadRequest(exception.Message);
            }
        }
    }
}