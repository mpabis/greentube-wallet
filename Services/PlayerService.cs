using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;
using Greentube.Wallet.Repositories;

namespace Greentube.Wallet.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<Player> Register()
        {
            return _playerRepository.CreatePlayer();
        }

        public async Task<decimal?> GetBalance(Guid playerId)
        {
            var existingPlayer = await _playerRepository.GetPlayer(playerId);

            return existingPlayer?.Balance;
        }
    }
}