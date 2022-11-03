using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIflipper : MonoBehaviour
{
    public CharacterControl charcontrol;

    public void FlipUIElement()
    {
        if (charcontrol.isSpriteFlipped() == true)
        {
            transform.rotation = Quaternion.Euler(new Vector3(-180f, -180f, -180f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
}
