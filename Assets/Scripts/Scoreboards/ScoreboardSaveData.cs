using System;
using System.Collections.Generic;

namespace Scoreboard
{
    [Serializable]
    public class ScoreboardSaveData
    {
        public List<ScoreboardEntryData> victoryPoints = new List<ScoreboardEntryData>();
    }
}
