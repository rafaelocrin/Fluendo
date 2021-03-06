﻿using Fluendo.FluendoPlatform.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.StatsService.Services
{
    public interface ILeaderboardService
    {
        Enums.ResultStatus ProcessLeaderboard(string leaderBoardData);
    }
}
