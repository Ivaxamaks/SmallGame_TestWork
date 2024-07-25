using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [Header("Player Settings")]
        public float PlayerMoveSpeed;
        public float PlayerShootingRange;
        public float PlayerShootingRate;
        public int PlayerDamage;
        public float BulletSpeed;

        [Header("Enemy Settings")]
        public float MinEnemySpawnTime;
        public float MaxEnemySpawnTime;
        public float MinEnemySpeed;
        public float MaxEnemySpeed;
        public int EnemyHealth;

        [Header("Game Settings")]
        public int MinEnemiesToKill;
        public int MaxEnemiesToKill;
        public int PlayerInitialHealth;
    }
}