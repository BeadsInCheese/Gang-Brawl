using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeSprite(3));
    }

    // The sprite renderer component
    public ParticleSystem particleSystem;

    // A method to trigger the fading effect with a fading duration
    public void TriggerFade(float fadeDuration)
    {
        // Start a coroutine that performs the actual fading logic
        StartCoroutine(FadeSprite(fadeDuration));
    }

    // A coroutine that performs the actual fading logic
    private IEnumerator FadeSprite(float fadeDuration)
    {
        // Store the original color of the sprite
        //Color originalColor = particleSystem.main.startColor.color;

        // Store a transparent color with zero alpha
        //Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Initialize a variable to store the elapsed time
        float elapsedTime = 0f;

        // Loop until the elapsed time reaches or exceeds the fade duration
        while (elapsedTime < fadeDuration)
        {
            // Increase the elapsed time by delta time
            elapsedTime += Time.deltaTime;

            // Calculate the normalized elapsed time
            float normalizedTime = elapsedTime / fadeDuration;

            // Interpolate between the original color and the transparent color based on the normalized elapsed time
            //Color currentColor = Color.Lerp(originalColor, transparentColor, normalizedTime);

            // Set the color of the sprite to the current color
            //var main = particleSystem.main;
           // main.startColor = new ParticleSystem.MinMaxGradient(currentColor);

            // Wait until next frame before continuing the loop
            yield return null;
        }

        // Destroy the game object
        Destroy(gameObject);
    }
}
