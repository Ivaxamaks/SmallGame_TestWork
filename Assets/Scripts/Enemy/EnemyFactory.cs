using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Enemy
{
    public class EnemyFactory
    {
        private readonly IObjectResolver _container;
        private readonly Enemy _enemyPrefab;

        public EnemyFactory(
            IObjectResolver container,
            Enemy enemyPrefab)
        {
            _container = container;
            _enemyPrefab = enemyPrefab;
        }

        public Enemy Create(Transform transform)
        {
            var enemy = _container.Instantiate(_enemyPrefab);
            return enemy;
        }
    }
}