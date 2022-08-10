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

        public void SetConfig(GeneratorSpawnConfig config) => 
            _config = config;

        public void SpawnGenerator() => 
            _levelFactory.LoadGenerator(_config.PrefabReference, _randomService.GeneratorSpawnPoint());

        public void Clear() => 
            _config = null;
    }
}