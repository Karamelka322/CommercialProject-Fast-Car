using System;
using CodeBase.Services.Random;
using JetBrains.Annotations;
using Sirenix.OdinInspector;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class SpawnConfig
    {
        [FoldoutGroup("Player"), HideLabel]
        public PlayerSpawnConfig Player;

        [Toggle("UsingGenerator")]
        public GeneratorSpawnConfig Generator;

        [Toggle("UsingCapsule"), OnCollectionChanged("OnUsingCapsuleChanged")]
        public EnergySpawnConfig energy;

        [Toggle("UsingEnemy")]
        public EnemiesSpawnConfig Enemy;

#if UNITY_EDITOR

        [UsedImplicitly]
        private void OnUsingCapsuleChanged()
        {
            if (energy.UsingCapsule == false)
            {
                energy.Quantity = 0;
                energy.SpawnPoints = Array.Empty<PointData>();
            }
        }

#endif
    }
}