using UnityEngine;

public class Health : MonoBehaviour
{
    [Tooltip("The maximum health value.")]
    public int maxHealth = 100;

    [Tooltip("The current health value.")]
    public int currentHealth;

    // This function is called when the script first loads.
    void Start()
    {
        // Set the character's health to full when it spawns.
        currentHealth = maxHealth;
    }

    // A public function that can be called from other scripts (like a weapon or enemy)
    public void TakeDamage(int damageAmount)
    {
        // Subtract the damage from our current health.
        currentHealth -= damageAmount;

        // Make sure health doesn't go below zero.
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log(transform.name + " took " + damageAmount + " damage. Current health: " + currentHealth);

        // If health has reached zero, call the Die function.
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // This is the temporary test function.
void Update()
{
    // If we press the Spacebar key...
    if (Input.GetKeyDown(KeyCode.Space))
    {
        // ...call the TakeDamage function on this object.
        TakeDamage(20);
    }
}
    private void Die()
    {
        Debug.Log(transform.name + " has died.");

        // This is where you could play a death animation or sound effect.

        // For now, we'll just destroy the GameObject.
        Destroy(gameObject);
    }
}