using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SetParameters : MonoBehaviour {
    public Text ipText;
    public Text portText;
    
	// Update is called once per frame
	void Update () {
        if (ipText != null)
        NetworkManager.singleton.networkAddress = string.IsNullOrEmpty(ipText.text) ? "localhost" : ipText.text;

        int port = 7777;
        if (int.TryParse(portText.text,out port))
            NetworkManager.singleton.networkPort = port;
    }
}
