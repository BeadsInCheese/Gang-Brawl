using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AddToNetworkObjects : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        NetworkManager.Singleton.AddNetworkPrefab(this.gameObject);
    }
}
