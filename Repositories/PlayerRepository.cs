using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greentube.Wallet.Model;

namespace Greentube.Wallet.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players;

        public PlayerRepository()
        {
            _players = new List<Player>();
        }

        public Task<IEnumerable<Player>> GetPlayers()
        {
            return Task.FromResult(_players.AsEnumerable());
        }

        public Task<Player> GetPlayer(Guid playerId)
        {
            return Task.FromResult(_players.FirstOrDefault(player => player.Id == playerId));
        }

        public async Task<bool> ChangeBalance(Guid playerId, decimal newBalance)
        {
            var player = await GetPlayer(playerId);
            if (player == null)
            {
                return false;
            }

            player.Balance = newBalance;
            return true;
        }

        public Task<Player> CreatePlayer()
        {
            var newPlayer = new Player
            {
                Id = Guid.NewGuid()
            };
            _players.Add(newPlayer);
            return Task.FromResult(newPlayer);
        }
    }
}