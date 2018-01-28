using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DisconnectOnEscape : MonoBehaviour {

        void Update()
        {
        if (Input.GetKeyDown(KeyCode.Escape))
            NetworkManager.singleton.StopHost();
    }
}
