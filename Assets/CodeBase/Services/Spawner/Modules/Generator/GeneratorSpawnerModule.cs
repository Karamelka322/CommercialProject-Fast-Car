using CodeBase.Data.Static.Level;
using CodeBase.Services.Factories.Level;
using CodeBase.Services.Random;

namespace CodeBase.Services.Spawner
{
    public class GeneratorSpawnerModule : IGeneratorSpawnerModule
    {
        private readonly ILevelFactory _levelFactory;
        private readonly IRandomService _randomService;

        private GeneratorSpawnConfig _config;

        public GeneratorSpawnerModule(ILevelFactory levelFactory, IRandomService randomService)
        {
            _levelFactory = levelFactory;
            _randomService = randomService;
        }

        public void SetConfig(LevelStaticData levelConfig)
        {
            _config = levelConfig.Spawn.Generator;
        }

        public void TrySpawnGenerator()
        {
            if (IsSpawnGenerator()) 
                SpawnGenerator();
        }

        public void Clear() => 
            _config = null;

        private bool IsSpawnGenerator() => 
            _config != null && _config.UsingGenerator;

        private void SpawnGenerator() => 
            _levelFactory.LoadGenerator(_randomService.GeneratorSpawnPoint());
    }
}