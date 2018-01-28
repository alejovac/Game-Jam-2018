using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DisconnectOnClick : MonoBehaviour
{

    public void Disconnect()
    {
        NetworkManager.singleton.StopClient();
   
    }
}
