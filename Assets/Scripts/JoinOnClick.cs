using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class JoinOnClick : MonoBehaviour
{

    public void Join()
    {
        NetworkManager.singleton.StartClient();
    }
}
