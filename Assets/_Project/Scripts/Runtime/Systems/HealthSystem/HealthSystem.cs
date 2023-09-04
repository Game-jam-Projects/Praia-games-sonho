using System;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.Health
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public float MaxHealth { get; set; }

        public float CurrentHealth { get; set; }
        public bool IsDie { get; set; }

        public event Action<HealthArgs> OnChangeHealth;
        public event Action<IDamageable> OnDie;
        public event Action<Vector3> OnTakeDamage;
        public event Action OnHeal;

        [SerializeField] private bool destroyOnDie;
        [SerializeField] private bool disableOnDie;

        private void Start()
        {
           ResetLife();
        }

        public void TakeDamage(Vector3 direction, float damage)
        {
            if (damage <= 0)
                return;

            CurrentHealth -= damage;

            if (CurrentHealth < 0)
            {
                Die();
                return;
            }

            OnChangeHealth?.Invoke(new() { current = CurrentHealth, max = MaxHealth });
            OnTakeDamage?.Invoke(direction);
        }

        public void Die()
        {
            if (IsDie)
                return;
            IsDie = true;
            OnDie?.Invoke(this);


            if (destroyOnDie)
                Destroy(gameObject);

            else if (disableOnDie)
                gameObject.SetActive(false);
        }

        public void Heal(float amount)
        {
            if (amount <= 0)
                return;

            CurrentHealth += amount;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;

            OnChangeHealth?.Invoke(new() { current = CurrentHealth, max = MaxHealth });
            OnHeal?.Invoke();
        }

        public void ResetLife()
        {
            IsDie = false;
            CurrentHealth = MaxHealth;
            OnChangeHealth?.Invoke(new() { current = CurrentHealth, max = MaxHealth });
        }
    }
}