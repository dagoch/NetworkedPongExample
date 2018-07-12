using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;

    public Text healthText;

    public void Start()
    {
        healthText.text = currentHealth.ToString();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (healthText != null) {
            healthText.text = currentHealth.ToString();
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }
    }
}