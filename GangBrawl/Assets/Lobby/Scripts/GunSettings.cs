using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle GiveWeaponsOnSpawn;
    void Start()
    {
        GiveWeaponsOnSpawn.isOn = DirectorBehaviour.spawnWithGuns;
    }
    public void SetGunState()
    {
        DirectorBehaviour.spawnWithGuns = GiveWeaponsOnSpawn.isOn;

    }

}
