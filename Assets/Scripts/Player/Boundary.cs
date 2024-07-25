using System;
using UnityEngine;

namespace Player
{
    public class Boundary : MonoBehaviour
    {
        public Action OnEnemyReachBoundary;
        
        public BoundaryPointsProvider BoundaryPointsProvider => _boundaryPoints;
        
        [SerializeField] private BoundaryPointsProvider _boundaryPoints;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;
            OnEnemyReachBoundary?.Invoke();
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy == null) return;
            enemy.Die();
        }
    }
}