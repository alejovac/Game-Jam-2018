using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SeleccionadorDeSonido : NetworkBehaviour {
	public Sonidos serverSound;
	public Sonidos clienteSound;

	void Start () {
		Sonidos asd = isServer?serverSound:clienteSound;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
