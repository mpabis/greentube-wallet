using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;
using Greentube.Wallet.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greentube.Wallet.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        //[HttpPost("[controller]/[action]")]
        //[HttpPost("player/register")]
        [HttpPost]
        public async Task<ActionResult<Player>> Register()
        {
            var player = await _playerService.Register();
            return player;
        }

        [HttpGet("{playerId}")]
        //[HttpGet("[controller]/[action]/{playerId}")]
        public async Task<ActionResult<decimal>> Balance([FromRoute]Guid playerId)
        {
            var balance = await _playerService.GetBalance(playerId);
            return balance == null ? (ActionResult) NotFound() : Ok(balance.Value);
        }
    }
}
