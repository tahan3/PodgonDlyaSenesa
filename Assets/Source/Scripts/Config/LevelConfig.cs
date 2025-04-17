using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Source.Scripts.Config.Core;

namespace Source.Scripts.Config
{
    [Serializable]
    public class LevelConfig
    {
        [JsonProperty("level_index")]
        private int _levelIndex;

        [JsonProperty("segments")]
        private List<string> _segments;
        
        [JsonProperty("words")]
        private List<string> _words;
        
        [JsonIgnore]
        public int LevelIndex => _levelIndex;
        [JsonIgnore]
        public IReadOnlyList<string> Segments => _segments;
        [JsonIgnore]
        public IReadOnlyList<string> Words => _words;
    }
}