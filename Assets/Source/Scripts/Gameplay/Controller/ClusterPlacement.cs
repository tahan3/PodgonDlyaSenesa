using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Scripts.Config;
using Source.Scripts.Gameplay.Model;
using Source.Scripts.Gameplay.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Gameplay.Controller
{
    public class ClusterPlacement
    {
        private readonly LevelConfig _levelModel;
        private readonly ClusterView _clusterPrefab;
        private readonly WordSlotView _wordSlotPrefab;
        private readonly SlotsContainerView _slotsContainerView;
        
        private readonly Dictionary<WordSlotView, WordSlot> _slotMapping = new Dictionary<WordSlotView, WordSlot>();
        private readonly Dictionary<WordSlotView, int> _slotIndexMapping = new Dictionary<WordSlotView, int>();
        private readonly Dictionary<ClusterView, LetterCluster> _clusterMapping = new Dictionary<ClusterView, LetterCluster>();
        private readonly Dictionary<ClusterView, WordSlot> _clustersInSlots = new Dictionary<ClusterView, WordSlot>();

        public ClusterPlacement(LevelConfig levelModel, ClusterView clusterPrefab, WordSlotView wordSlotPrefab,
            SlotsContainerView slotsContainerView)
        {
            _levelModel = levelModel;
            _clusterPrefab = clusterPrefab;
            _wordSlotPrefab = wordSlotPrefab;
            _slotsContainerView = slotsContainerView;
        }

        public void Initialize(GameView gameView)
        {
            for (var i = 0; i < _levelModel.Segments.Count; i++)
            {
                var clusterModel = new LetterCluster(_levelModel.Segments[i]);
                var clusterView = Object.Instantiate(_clusterPrefab, gameView.ClustersParent);
                
                clusterView.Setup(clusterModel.Letters, OnClusterDrop, OnClusterMiss);
                _clusterMapping[clusterView] = clusterModel;
            }
            
            for (int i = 0; i < _levelModel.Words.Count; i++)
            {
                var slot = new WordSlot(_levelModel.Words[i].Length);
                var slotsContainer = Object.Instantiate(_slotsContainerView, gameView.SlotsParent);
                for (int j = 0; j < _levelModel.Words[i].Length; j++)
                {
                    var slotView = Object.Instantiate(_wordSlotPrefab, slotsContainer.SlotsParent);
                    _slotMapping[slotView] = slot;
                    _slotIndexMapping[slotView] = _levelModel.Words[i].Length - 1 - j;
                }
            }
        }

        private void OnClusterMiss(ClusterView clusterView)
        {
            Debug.Log("Cluster Miss");

            RemoveUsedCluster(clusterView);
        }

        private void RemoveUsedCluster(ClusterView clusterView)
        {
            if (_clustersInSlots.TryGetValue(clusterView, out var slot))
            {
                var cluster = _clusterMapping[clusterView];
                slot.RemoveCluster(cluster);
                _clustersInSlots.Remove(clusterView);

                Debug.Log($"Cluster {cluster.Letters} removed from : {slot.GetCurrentWord()}");
            }
        }

        private void OnClusterDrop(ClusterView clusterView, WordSlotView wordSlotView)
        {
            var cluster = _clusterMapping[clusterView];
            var slot = _slotMapping[wordSlotView];
            
            RemoveUsedCluster(clusterView);

            if (slot.TryAddCluster(cluster, _slotIndexMapping[wordSlotView]))
            {
                clusterView.transform.DOMove(wordSlotView.transform.position, 0.2f);

                _clustersInSlots[clusterView] = slot;
                
                Debug.Log($"Cluster {cluster.Letters} added to : {slot.GetCurrentWord()}");
            }
        }
    }
}