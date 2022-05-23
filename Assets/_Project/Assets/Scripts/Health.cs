using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose
{
    [DisallowMultipleComponent]
    public class Health : MonoBehaviour
    {
        private int _maxHealth;
        private int _currentHealth;
        
        /// <summary>
        /// Allowed a user to deal damage to this health component, reducing its current health.
        /// </summary>
        /// <param name="damage"> The amount of damage to deal to the current health.</param>
        /// <returns> A bool for whether the damage dealt reduced the current health to less than or equal to zero.</returns>
        public bool TakeDamage(int damage)
        {
            _currentHealth -= damage;
            return _currentHealth <= 0;
        }
        
        public int GetCurrentHealth() => _currentHealth;
        
        /// Heal by the specified amount.
        public void Heal(int healAmount)
        {
            _currentHealth += healAmount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
        
        /// Set the current health to the specified percentage of the max health.
        /// <param name="percentage"> A value between 0 and 1 specifying the percentage of the max health to set the current health to.</param>
        public void SetHealthPercentage(float percentage)
        {
            _currentHealth = Mathf.RoundToInt(percentage * _maxHealth);
        }
        
        /// Set the maximum health to the specified value and set the current health to the same value.
        /// <param name="maxHealth"> The value to set the maximum health to.</param>
        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }
    }
}