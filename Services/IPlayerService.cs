using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;
using Microsoft.AspNetCore.Mvc;

namespace Greentube.Wallet.Services
{
    public interface IPlayerService
    {
        Task<Player> Register();
        Task<ActionResult<decimal?>> GetBalance(Guid playerId);
    }
}