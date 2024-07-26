using System;
using UI.HealthBar;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealthController : MonoBehaviour, IHealth
    {
        public event Action<float, float> HealthChanged;
        public event Action OnDied;
        
        public bool IsInitialized => MaxHealth > 0;
        public bool IsDead => Health <= 0;
        
        public float MaxHealth { get; private set; }
        public float Health { get; private set; }
        
        [field: SerializeField] 
        public Transform HealthBarAnchor { get; private set; }
        
        public void Initialize(float maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;

            HealthChanged?.Invoke(Health, MaxHealth);
        }
        
        public void TakeDamage(float damage)
        {
            if (IsDead) return;
            
            if (damage >= Health)
            {
                Health = 0;
            }
            else
            {
                Health -= damage;
            }

            HealthChanged?.Invoke(Health, MaxHealth);
            if (IsDead)
                OnDied?.Invoke();
        }
    }
}