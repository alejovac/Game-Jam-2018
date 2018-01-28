using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sonidos : NetworkBehaviour
{
	public AudioClip perdida1;
	public AudioClip perdida2;
	public AudioClip perdida3;
	public AudioClip alto;
	public AudioClip cuidado;
	public AudioClip retrocede;
	public AudioClip aqui;
	public AudioClip unodostres;
	public AudioClip genial;
	public AudioClip ven;
	public AudioClip ahora;
	public AudioClip porPoco;
	public AudioClip tututu;
	public AudioClip vamos;

	public bool libre;
	public float contador = 0;
	public float tiempoEntreAudios = 5;

	public Queue<AudioClip> cola = new Queue<AudioClip>();

	void Start()
	{
		libre = true;
	}

	public void deadsound()
	{
		switch (Random.Range (1, 4)) 
		{
		case 1:
			cola.Enqueue(perdida1);
			break;
		case 2:
			cola.Enqueue(perdida2);
			break;
		case 3:
			cola.Enqueue(perdida3);
			break;
		}
	}

	void Update ()
	{
		if (cola.Count > 0) {
			AudioSource.PlayClipAtPoint ((AudioClip)cola.Dequeue(), transform.position);
		}

		if (cola.Count <= 0) {
			if (Input.GetKeyDown ("q")) {
				cola.Enqueue(alto);
			} else if (Input.GetKeyDown ("w")) {
				cola.Enqueue(cuidado);
			} else if (Input.GetKeyDown ("e")) {
				cola.Enqueue (retrocede);
			} else if (Input.GetKeyDown ("r")) {
				cola.Enqueue(aqui);
			} else if (Input.GetKeyDown ("t")) {
				cola.Enqueue(unodostres);
			} else if (Input.GetKeyDown ("a")) {
				cola.Enqueue(genial);
			} else if (Input.GetKeyDown ("s")) {
				cola.Enqueue(ven);
			} else if (Input.GetKeyDown ("d")) {
				cola.Enqueue(porPoco);
			} else if (Input.GetKeyDown ("f")) {
				cola.Enqueue(vamos);
			} else if (Input.GetKeyDown ("p")) {
				cola.Enqueue(tututu);
			}
		}
	}
}