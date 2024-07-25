using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerEnemyDetector : MonoBehaviour
    {
        private GameSettings _gameSettings;
        private Collider2D[] _results;

        public void Initialize(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _results = new Collider2D[gameSettings.MaxEnemiesToKill];
        }

        public Enemy.Enemy FindClosestEnemy()
        {
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, _gameSettings.PlayerShootingRange, _results);
            Enemy.Enemy closestEnemy = null;
            var closestDistance = Mathf.Infinity;

            for (var i = 0; i < count; i++)
            {
                Enemy.Enemy enemy = _results[i].GetComponent<Enemy.Enemy>();
                if (enemy == null) continue;
                var distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
    }
}