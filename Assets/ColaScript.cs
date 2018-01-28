using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColaScript : NetworkBehaviour {
	public NetworkIdentity ID;

	// Use this for initialization
	void Start (AudioClip sound) {
		AudioSource.PlayClipAtPoint (sound, transform.position);
	}
}
