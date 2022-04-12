using System;
using CodeBase.Services.Random;
using JetBrains.Annotations;
using Sirenix.OdinInspector;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class SpawnConfig
    {
        [Toggle("UsingGenerator")]
        public GeneratorSpawnConfig Generator;

        [Toggle("UsingCapsule"), OnCollectionChanged("OnUsingCapsuleChanged")]
        public CapsuleSpawnConfig Capsule;

        [Toggle("UsingEnemy")]
        public EnemiesSpawnConfig Enemy;

#if UNITY_EDITOR

        [UsedImplicitly]
        private void OnUsingCapsuleChanged()
        {
            if (Capsule.UsingCapsule == false)
            {
                Capsule.Quantity = 0;
                Capsule.CapsuleSpawnPoints = Array.Empty<PointData>();
            }
        }

#endif
    }
}