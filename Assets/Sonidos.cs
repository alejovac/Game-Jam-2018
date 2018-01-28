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

	public Queue<AudioClip> cola;

	void Start()
	{
		libre = true;
	}

	public void deadsound()
	{
		switch (Random.Range (1, 4)) 
		{
		case 1:
			AudioSource.PlayClipAtPoint (perdida1, transform.position);
			libre = false;
			break;
		case 2:
			AudioSource.PlayClipAtPoint (perdida2, transform.position);
			libre = false;
			break;
		case 3:
			AudioSource.PlayClipAtPoint (perdida3, transform.position);
			libre = false;
			break;
		}
	}

	void Update ()
	{
		if (cola.Count == 0) {
			if (Input.GetKeyDown ("q")) {
				AudioSource.PlayClipAtPoint (alto, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("w")) {
				AudioSource.PlayClipAtPoint (cuidado, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("e")) {
				AudioSource.PlayClipAtPoint (retrocede, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("r")) {
				AudioSource.PlayClipAtPoint (aqui, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("t")) {
				AudioSource.PlayClipAtPoint (unodostres, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("a")) {
				AudioSource.PlayClipAtPoint (genial, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("s")) {
				AudioSource.PlayClipAtPoint (ven, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("d")) {
				AudioSource.PlayClipAtPoint (porPoco, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("f")) {
				AudioSource.PlayClipAtPoint (vamos, transform.position);
				libre = false;
			} else if (Input.GetKeyDown ("p")) {
				AudioSource.PlayClipAtPoint (tututu, transform.position);
				libre = false;
			}
		} 
		else 
		{
			if (contador <= tiempoEntreAudios) {
				contador += Time.deltaTime;
			} 
			else {
				contador = 0;
				libre = true;
			}
		}

		if () {
			deadsound();
			libre = false;
		}
	}
}