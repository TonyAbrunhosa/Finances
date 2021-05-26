using FinancasAPI.Domain.Contracts;
using FinancasAPI.Domain.Contracts.IService;
using FinancasAPI.Domain.Entities;
using FinancasAPI.Domain.EntitiesOutput;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OperationController : Controller
    {
        private readonly IOperationsService _service;

        public OperationController(IOperationsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Operations>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperations()
        {
            try
            {
                ICommandResult result = await _service.GetOperations();

                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }

        [HttpGet("GetByAccount")]
        [ProducesResponseType(typeof(IEnumerable<OperationsOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperationsByAccount()
        {
            try
            {
                ICommandResult result = await _service.GetOperationsByAccount();

                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }

        [HttpGet("GetByAtivo")]
        [ProducesResponseType(typeof(IEnumerable<OperationsOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperationsByAtivo()
        {
            try
            {
                ICommandResult result = await _service.GetOperationsByAtivo();

                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }

        [HttpGet("GetByTypeOperations")]
        [ProducesResponseType(typeof(IEnumerable<OperationsOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperationsByTypeOperations()
        {
            try
            {
                ICommandResult result = await _service.GetOperationsByTypeOperations();

                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }

        [HttpGet("GetTotalByAtivos")]
        [ProducesResponseType(typeof(IEnumerable<OperationsOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalByAtivos()
        {
            try
            {
                ICommandResult result = await _service.GetTotalByAtivos();

                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }
    }
}
