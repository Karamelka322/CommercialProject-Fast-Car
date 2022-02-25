using CodeBase.Services.Random;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorPrefab : MonoBehaviour, IReplayHandler
    {
        private IRandomService _randomService;
        
        public void Construct(IRandomService randomService) => 
            _randomService = randomService;

        public void OnReplay() => 
            transform.position = _randomService.GeneratorSpawnPoint();
    }
}