using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
private void Awake() 
{ 
    // If there is an instance, and it's not me, delete myself.
    
    if (Instance != null && Instance != this) 
    { 
        Destroy(Instance); 

        Instance = this; 
    } 
    else 
    { 
        Instance = this; 
    } 
}
    // Start is called before the first frame update
    Vector3 position;
    public float shakeAmount=1;
    Vector3 originalPos;
    Quaternion originalRot;
    void Start()
    {
        position=transform.position;

        originalRot = transform.rotation;
    }
    public IEnumerator ScreenShake(float duration, float magnitude)
    {

        Vector3 originalPos = position;
        //Quaternion originalRot = transform.rotation;

        float elapsed = 0f;

        while (elapsed < duration)
        {
                    if(Time.timeScale==0){
                        transform.position = originalPos;
                        transform.rotation = originalRot;
                        yield break;
                    }
            // Generate a random offset
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            // Apply the offset to the camera position
            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);

            // Generate a random angle
            float angle = Random.Range(-1f, 1f) * magnitude;

            // Apply the angle to the camera rotation
            transform.rotation = Quaternion.Euler(originalRot.eulerAngles + new Vector3(0f, 0f, angle));

            // Update the elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Restore the original position and rotation of the camera
        transform.position = originalPos;
        transform.rotation = originalRot;
    }
    // Update is called once per frame
    void Update()
    {
     if(Earthquake.shaking){
        this.transform.position=position+new Vector3(UnityEngine.Random.Range(-100*shakeAmount,100*shakeAmount),Random.Range(-100*shakeAmount,100*shakeAmount));
     }else{
        transform.position=position;
     }
    }
}
