using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.StatsService.Persistence;
using Fluendo.FluendoPlatform.StatsService.Persistence.Repositories;
using System;

namespace Fluendo.FluendoPlatform.StatsService.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        protected readonly ILeaderboardRepository _leaderboardRepository;

        public LeaderboardService(ILeaderboardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }

        public Enums.ResultStatus ProcessLeaderboard(string leaderboardData)
        {
            return (_leaderboardRepository.Update(leaderboardData) != -1) ? Enums.ResultStatus.Success : Enums.ResultStatus.Failed;
        }
    }
}
