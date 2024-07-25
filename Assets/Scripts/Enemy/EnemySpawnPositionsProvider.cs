using UnityEngine;

namespace Enemy
{
    public class EnemySpawnPositionsProvider : MonoBehaviour
    {
        public Transform[] EnemySpawnPositions => _enemySpawnPositions;
        
        [SerializeField] private Transform[] _enemySpawnPositions;
    }
}