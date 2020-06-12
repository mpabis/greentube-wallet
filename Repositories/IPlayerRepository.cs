using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greentube.Wallet.Model;

namespace Greentube.Wallet.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayers();
        Task<Player> CreatePlayer();
        Task<Player> GetPlayer(Guid playerId);
        Task<bool> ChangeBalance(Guid playerId, decimal amountDelta);
    }
}