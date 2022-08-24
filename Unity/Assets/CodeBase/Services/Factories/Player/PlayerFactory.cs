using CodeBase.Data.Static.Player;
using CodeBase.Infrastructure.Mediator.Level;
using CodeBase.Logic.Player;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
using CodeBase.Services.Victory;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories.Player
{
    [UsedImplicitly]
    public class PlayerFactory : IPlayerFactory
    {
        private const string PlayerName = "Player";

        private readonly DiContainer _diContainer;
        
        public PlayerFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData playerStaticData = _diContainer.Resolve<IStaticDataService>().ForPlayer(typeId);
            return InstantiateRegister(at, playerStaticData);
        }

        public void CreatePreviewPlayer(PlayerTypeId typeId, Transform parent)
        {
            GameObject player = _diContainer.InstantiatePrefab(_diContainer.Resolve<IStaticDataService>().ForPlayer(typeId).Preview.gameObject, parent);
            player.name = PlayerName;
        }

        private GameObject InstantiateRegister(Vector3 at, PlayerStaticData playerStaticData)
        {
            GameObject gameObject = _diContainer.InstantiatePrefab(playerStaticData.Prefab.gameObject, at, Quaternion.identity, null);

            _diContainer.Resolve<IPauseService>().Register(gameObject);
            _diContainer.Resolve<IReadWriteDataService>().Register(gameObject);
            _diContainer.Resolve<IReplayService>().Register(gameObject);
            _diContainer.Resolve<IDefeatService>().Register(gameObject);
            _diContainer.Resolve<IVictoryService>().Register(gameObject);
            
            _diContainer.Resolve<ILevelMediator>().Construct(gameObject.GetComponent<PlayerPrefab>());
            
            return gameObject;
        }
    }
}