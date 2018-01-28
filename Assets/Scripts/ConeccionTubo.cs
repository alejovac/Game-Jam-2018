using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeccionTubo : MonoBehaviour
{
    public bool conectado;
    public bool personajeAlLado;

    void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.tag == "Tubo")
            conectado = true;

        if (theCollision.gameObject.tag == "Player")
            personajeAlLado = true;
    }
    
    void OnTriggerExit2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.tag == "Tubo")
            conectado = false;

        if (theCollision.gameObject.tag == "Player")
            personajeAlLado = false;
    }
}