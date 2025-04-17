using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Source.Scripts.Config.Core;

namespace Source.Scripts.Config
{
    [Serializable]
    public class GameplayConfig : IConfig
    {
        [JsonProperty("levels")]
        private List<LevelConfig> _levels;
        
        [JsonIgnore]
        public IReadOnlyList<LevelConfig> Levels => _levels;
    }
}