using CodeBase.Data.Static.Player;
using CodeBase.Logic.Player;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Defeat;
using CodeBase.Services.Pause;
using CodeBase.Services.Replay;
using CodeBase.Services.StaticData;
using CodeBase.Services.Victory;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private const string PlayerName = "Player";

        private readonly DiContainer _diContainer;

        public GameObject Player { get; private set; }
        public GameObject PreviewPlayer { get; private set; }

        public PlayerFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at)
        {
            PlayerStaticData playerStaticData = _diContainer.Resolve<IStaticDataService>().ForPlayer(typeId);
            return Player = InstantiateRegister(at, playerStaticData);
        }

        public void CreatePreviewPlayer(PlayerTypeId typeId, Transform parent)
        {
            PreviewPlayer = _diContainer.InstantiatePrefab(_diContainer.Resolve<IStaticDataService>().ForPlayer(typeId).Preview.gameObject, parent);
            PreviewPlayer.name = PlayerName;
        }

        public void RebuildBasePreviewPlayerObject(PlayerTypeId playerTypeId)
        {
            PlayerPreview asset = _diContainer.Resolve<IStaticDataService>().ForPlayer(playerTypeId).Preview;
            PlayerPreview preview = PreviewPlayer.GetComponent<PlayerPreview>();

            preview.Body.MeshFilter.mesh = asset.Body.MeshFilter.sharedMesh;
            preview.Body.MeshRenderer.material = asset.Body.MeshRenderer.sharedMaterial;
            
            preview.FrontLeftWheel.MeshFilter.mesh = asset.FrontLeftWheel.MeshFilter.sharedMesh;
            preview.FrontLeftWheel.MeshRenderer.material = asset.FrontLeftWheel.MeshRenderer.sharedMaterial;
            
            preview.FrontRightWheel.MeshFilter.mesh = asset.FrontRightWheel.MeshFilter.sharedMesh;
            preview.FrontRightWheel.MeshRenderer.material = asset.FrontRightWheel.MeshRenderer.sharedMaterial;
            
            preview.RearLeftWheel.MeshFilter.mesh = asset.RearLeftWheel.MeshFilter.sharedMesh;
            preview.RearLeftWheel.MeshRenderer.material = asset.RearLeftWheel.MeshRenderer.sharedMaterial;
            
            preview.RearRightWheel.MeshFilter.mesh = asset.RearRightWheel.MeshFilter.sharedMesh;
            preview.RearRightWheel.MeshRenderer.material = asset.RearRightWheel.MeshRenderer.sharedMaterial;
        }

        private GameObject InstantiateRegister(Vector3 at, PlayerStaticData playerStaticData)
        {
            GameObject gameObject = _diContainer.InstantiatePrefab(playerStaticData.Prefab.gameObject, at, Quaternion.identity, null);

            _diContainer.Resolve<IPauseService>().Register(gameObject);
            _diContainer.Resolve<IReadWriteDataService>().Register(gameObject);
            _diContainer.Resolve<IReplayService>().Register(gameObject);
            _diContainer.Resolve<IDefeatService>().Register(gameObject);
            _diContainer.Resolve<IVictoryService>().Register(gameObject);
            
            return gameObject;
        }
    }
}