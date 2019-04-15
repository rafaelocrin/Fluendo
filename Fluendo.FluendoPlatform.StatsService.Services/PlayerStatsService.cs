using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.StatsService.Persistence;
using Fluendo.FluendoPlatform.StatsService.Persistence.Repositories;
using System;

namespace Fluendo.FluendoPlatform.StatsService.Services
{
    public class PlayerStatsService : IPlayerStatsService
    {
        protected readonly IPlayerStatsRepository _playerStatsRepository;

        public PlayerStatsService(IPlayerStatsRepository playerStatsRepository)
        {
            _playerStatsRepository = playerStatsRepository;
        }

        public Enums.ResultStatus ProcessPlayerStats(string playerStatsData)
        {
            return (_playerStatsRepository.Update(playerStatsData) != -1) ? Enums.ResultStatus.Success : Enums.ResultStatus.Failed;
        }
    }
}
