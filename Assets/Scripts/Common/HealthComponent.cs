using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth = 100;

    public void GetDamage(int damage)
    {
        if (_currentHealth < damage)
        {
            Destroy(gameObject);
        }
        else
        {
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
        }
    }
}
