using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;
using Greentube.Wallet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greentube.Wallet.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IPlayerService _playerService;

        public TransactionController(ITransactionService transactionService, IPlayerService playerService)
        {
            _transactionService = transactionService;
            _playerService = playerService;
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<ActionResult> Post(
            [FromQuery] Guid transactionId,
            [FromQuery] Guid playerId,
            [FromQuery] TransactionType transactionType,
            [FromQuery] decimal amount)
        {
            var balance = await _playerService.GetBalance(playerId);

            if (!balance.HasValue)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Rejected.");
            }

            return await _transactionService.Add(transactionId, playerId, transactionType, amount, balance.Value)
                ? Ok("Accepted")
                : StatusCode(StatusCodes.Status400BadRequest, "Rejected.");
        }
    }
}
