using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyNetWorkButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
public void Host(){
    NetworkManager.Singleton.StartHost();
}
public void Server(){
        NetworkManager.Singleton.StartServer();
}
public void Client(){
        NetworkManager.Singleton.StartClient();
}
}
