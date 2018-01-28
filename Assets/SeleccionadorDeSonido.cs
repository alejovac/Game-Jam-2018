using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SeleccionadorDeSonido : NetworkBehaviour {


	public Queue<AudioClip> cola = new Queue<AudioClip>();
	public Sonidos serverSound;
	public Sonidos clienteSound;
	private AudioClip nuevoSonido = null;
	Sonidos asd;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		asd = isServer?serverSound:clienteSound;

		if (Input.GetKeyDown ("q")) {
			nuevoSonido = asd.alto;
		} else if (Input.GetKeyDown ("w")) {
			nuevoSonido = asd.cuidado;
		} else if (Input.GetKeyDown ("e")) {
			nuevoSonido = asd.retrocede;
		} else if (Input.GetKeyDown ("r")) {
			nuevoSonido = asd.aqui;
		} else if (Input.GetKeyDown ("t")) {
			nuevoSonido = asd.unodostres;
		} else if (Input.GetKeyDown ("a")) {
			nuevoSonido = asd.genial;
		} else if (Input.GetKeyDown ("s")) {
			nuevoSonido = asd.ven;
		} else if (Input.GetKeyDown ("d")) {
			nuevoSonido = asd.porPoco;
		} else if (Input.GetKeyDown ("f")) {
			nuevoSonido = asd.vamos;
		} else if (Input.GetKeyDown ("p")) {
			nuevoSonido = asd.tututu;
		}
		//ordenar al server
		if (nuevoSonido != null){
			agregarSonidoEnCola(nuevoSonido);
			nuevoSonido = null;
		}
		if (cola.Count > 0) {
//			NetworkServer.Spawn (new ColaScript(cola.Dequeue()));
			//ordenar al cliente
		}
	}

	[Command]
	public void agregarSonidoEnCola(AudioClip asd) 
	{
		cola.Enqueue(asd);
	}
}
