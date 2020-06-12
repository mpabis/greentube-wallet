using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;
using Greentube.Wallet.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greentube.Wallet.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<ActionResult> Post([FromQuery]Guid transactionId, [FromQuery]Guid playerId, [FromQuery]TransactionType transactionType, [FromQuery]decimal amount)
        {
            return await _transactionService.Add(transactionId, playerId, transactionType, amount)
                ? (ActionResult) Ok()
                : BadRequest();
        }
    }
}
