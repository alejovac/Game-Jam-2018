using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeccionTubo : MonoBehaviour
{
    public bool conectado;
    public bool personajeAlLado;

    public MovimientoPersonaje personajeAdjacente = null;

    void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.tag == "Tubo")
            conectado = true;

        if (theCollision.gameObject.tag == "Player")
        {
            personajeAlLado = true;
            personajeAdjacente = theCollision.GetComponent<MovimientoPersonaje>();
        }
    }
    
    void OnTriggerExit2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.tag == "Tubo")
            conectado = false;

        if (theCollision.gameObject.tag == "Player")
        {
            personajeAlLado = false;
            personajeAdjacente = null;
        }
    }
}