using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStun : MonoBehaviour
{
    public HPSystem hpSystem;
    // The child game object that has the character's sprite or model
    public GameObject characterSprite;

    // The animation curve asset that controls how the shake magnitude decreases over time
    public AnimationCurve shakeCurve;

    // A flag to indicate if the shake effect is active
    private bool shakeActive = false;

    // A variable to store the original position of the character sprite
    private Vector3 originalPosition;
    // The duration of the hitstop effect in seconds
    public float hitStopDuration = 0.1f;

    // A flag to indicate if the hitstop effect is active
    private bool hitStopActive = false;
    private Vector2 originalVelocity;

    // A variable to store the original time scale
    private float originalTimeScale;

    public GameObject blood;
    public CharacterControl characterController;
    public AudioClip splashSound;

    // A method to trigger the hitstop effect
    private void TriggerHitStop(float time)
    {
        // If the hitstop effect is not already active
        if (!hitStopActive)
        {
            // Set the flag to true
            hitStopActive = true;

            // Store the original velocity of the character
            originalVelocity = characterController.physicsBody.velocity;

            // Set the velocity of the character to zero
            characterController.physicsBody.velocity = Vector3.zero;
            characterController.InputDisabled = true;
            // Start a coroutine to resume the character's movement after the hitstop duration
            StartCoroutine(ResumeMovement(time));
        }
    }

    // A coroutine to resume the character's movement after the hitstop duration
    private IEnumerator ResumeMovement(float time)
    {
        // Wait for the hitstop duration in real time
        yield return new WaitForSecondsRealtime(time);

        // Set the velocity of the character back to its original value
        characterController.physicsBody.velocity = originalVelocity;
        characterController.InputDisabled = false;
        // Set the flag to false
        hitStopActive = false;
    }


// A coroutine to resume the game after the hitstop duration
private IEnumerator ResumeGame()
    {
        // Wait for the hitstop duration in real time
        yield return new WaitForSecondsRealtime(hitStopDuration);

        // Set the time scale back to the original value
        Time.timeScale = originalTimeScale;

        // Set the flag to false
        hitStopActive = false;
    }
    private void Start()
    {
        hpSystem.damageTaken.AddListener(TriggerShake);
    }
    // A method to trigger the shake effect based on a damage input value
    public void TriggerShake(float damage)
    {
        // If the shake effect is not already active
        if (!shakeActive)
        {
            AudioManager.instance.playSoundAtPoint(splashSound, transform.position);
            var bloodInstance=Instantiate(blood);
            bloodInstance.transform.position = this.transform.position;
            // Set the flag to true
            shakeActive = true;

            // Store the original position of the character sprite
            originalPosition = characterSprite.transform.localPosition;

            // Calculate the duration of the shake effect based on the damage input value
            // For example, you can use 0.1 seconds per unit of damage
            float shakeDuration = Mathf.Min(0.3f,0.1f * damage);

            // Calculate the maximum magnitude of the shake effect based on the damage input value
            // For example, you can use 0.01 units per unit of damage
            float maxShakeMagnitude = Mathf.Min(0.5f,3*0.01f * damage);

            // Start a coroutine that performs the actual shaking logic
            StartCoroutine(ShakeCharacterSprite(shakeDuration, maxShakeMagnitude));
            TriggerHitStop(shakeDuration);
        }
    }

    // A coroutine that performs the actual shaking logic
    private IEnumerator ShakeCharacterSprite(float shakeDuration, float maxShakeMagnitude)
    {
        // Initialize a variable to store the elapsed time
        float elapsedTime = 0f;

        // Loop until the elapsed time reaches or exceeds the shake duration
        while (elapsedTime < shakeDuration)
        {
            // Increase the elapsed time by delta time
            elapsedTime += Time.deltaTime;

            // Evaluate the animation curve at the normalized elapsed time
            float curveValue = 1-shakeCurve.Evaluate(elapsedTime / shakeDuration);

            // Generate a random vector inside a unit circle
            Vector2 randomVector = Random.insideUnitCircle;

            // Scale it by the maximum shake magnitude and the curve value
            randomVector *= maxShakeMagnitude * curveValue;

            // Add it to the original position of the character sprite
            characterSprite.transform.localPosition = originalPosition + (Vector3)randomVector;

            // Wait until next frame before continuing the loop
            yield return null;
        }

        // Set the character sprite's position back to its original value
        characterSprite.transform.localPosition = originalPosition;

        // Set the flag to false
        shakeActive = false;
    }
    bool first = false;
    private void Update()
    {
        if (hpSystem.dead) {
            first = false;
            characterController.InputDisabled = true;
        }
        else
        {
            if (!first)
            {
                first = true;
                characterController.InputDisabled = false;
            }
        }
    
    }
}
