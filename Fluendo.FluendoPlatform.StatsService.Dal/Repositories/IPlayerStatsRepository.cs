using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.StatsService.Persistence.Repositories
{
    public interface ILeaderboardRepository
    {
        int Update(string leaderboardTopList);
    }
}
