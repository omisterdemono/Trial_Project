using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private Image _helthFill;

    private void Awake()
    {
        FillHelthBar();
    }

    private void FillHelthBar()
    {
        if (_helthFill != null)
        {
            _helthFill.fillAmount = _currentHealth /_maxHealth;
        }
        else
        {
            throw new System.Exception("No health fill image is not set");
        }
    }

    public void GetDamage(int damage)
    {
        if (_currentHealth <= damage)
        {
            Destroy(gameObject);
        }
        else
        {
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
        }
        FillHelthBar();
    }

    public void GetHealth(int health)
    {
        if (health + _currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += health;
        }
        FillHelthBar();
    }
}
