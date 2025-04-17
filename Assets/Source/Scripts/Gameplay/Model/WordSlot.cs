using System.Collections.Generic;
using System.Text;

namespace Source.Scripts.Gameplay.Model
{
    public class WordSlot
    {
        private readonly Dictionary<LetterCluster, int> _clusterMap = new Dictionary<LetterCluster, int>();
        private readonly char[] _slots;
        private readonly int _size;

        private const char EmptyChar = ' ';
        
        private int _currentSize;

        public bool IsFull => _currentSize == _size;
        public int Size => _size;
        
        public WordSlot(int size)
        {
            _size = size;
            _currentSize = 0;
            _slots = new char[_size];
            FillSlots(0, _size, EmptyChar);
        }

        private bool EmptySlotsExist(int index, int size)
        {
            for (int i = index; i < index + size; i++)
            {
                if (_slots[i] != EmptyChar) return false;
            }

            return true;
        }

        private bool FillSlots(int index, string cluster)
        {
            if (index + cluster.Length <= _size)
            {
                for (int i = index, j = 0; i < index + cluster.Length; i++, j++)
                {
                    _slots[i] = cluster[j];
                }

                return true;
            }
            
            return false;
        }

        private bool FillSlots(int index, int length, char character)
        {
            if (index + length <= _size)
            {
                for (int i = index; i < index + length; i++)
                {
                    _slots[i] = character;
                }

                return true;
            }
            
            return false;
        }
        
        public bool RemoveCluster(LetterCluster cluster)
        {
            if (_clusterMap.ContainsKey(cluster))
            {
                FillSlots(_clusterMap[cluster], cluster.Letters.Length, EmptyChar);
                
                _clusterMap.Remove(cluster);
                _currentSize -= cluster.Letters.Length;
                
                return true;
            }
            
            return false;
        }
        
        public bool TryAddCluster(LetterCluster cluster, int index)
        {
            if (_currentSize + cluster.Letters.Length <= _size && EmptySlotsExist(index, cluster.Letters.Length))
            {
                _currentSize += cluster.Letters.Length;
                _clusterMap.Add(cluster, index);
                FillSlots(_clusterMap[cluster], cluster.Letters);
                return true;
            }

            return false;
        }

        public string GetCurrentWord()
        {
            var word = new StringBuilder();
            
            for (var i = 0; i < _slots.Length; i++)
            {
                word.Append(_slots[i]);
            }

            return word.ToString();
        }
    }
}