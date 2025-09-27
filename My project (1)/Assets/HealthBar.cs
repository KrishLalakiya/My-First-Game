using UnityEngine;
using UnityEngine.UI; // IMPORTANT: This is required for working with UI elements.

public class HealthBar : MonoBehaviour
{
    [Tooltip("The UI Slider element that represents the health bar.")]
    public Slider slider;
    
    [Tooltip("The Health script of the character this bar should follow (e.g., the Player).")]
    public Health targetHealth;

    void Start()
    {
        // Set the slider's maximum value to the character's max health.
        slider.maxValue = targetHealth.maxHealth;
        // Set the slider's current value to the character's current health.
        slider.value = targetHealth.currentHealth;
    }

    void Update()
    {
        // Continuously update the slider's value to match the character's current health.
        slider.value = targetHealth.currentHealth;
    }
}