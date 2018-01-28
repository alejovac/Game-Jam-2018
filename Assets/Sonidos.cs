using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour
{
	public AudioClip hola;
	public AudioClip chau;
	public bool libre;

	void Start()
	{
		libre = true;
	}

	void Update ()
	{
		if (libre)
		{
			if (Input.GetKeyDown("z"))
			{
				AudioSource.PlayClipAtPoint(hola, transform.position);
				libre = false;
			}
		}
	}
}