using System.Collections.Generic;

namespace Source.Scripts.Gameplay.Model
{
    public class RuntimeData
    {
        public List<WordSlot> WordSlots { get; } = new List<WordSlot>();
        public List<string> Clusters { get; } = new List<string>();

        public RuntimeData(List<string> words, List<string> clusters)
        {
            Clusters.AddRange(clusters);

            for (var i = 0; i < words.Count; i++)
            {
                WordSlots.Add(new WordSlot(words[i].Length));
            }
        }
    }
}