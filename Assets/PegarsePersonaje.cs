using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarsePersonaje : MonoBehaviour {
    public ConeccionTubo conectionUp;
    public ConeccionTubo conectionDown;
    public ConeccionTubo conectionLeft;
    public ConeccionTubo conectionRight;

    void Update()
    {
        int playerCant = 0;
        int playerMoviendo = 0;
        
        Vector2 movimiento = new Vector2(0,0);
        ConeccionTubo[] conecciones = { conectionUp, conectionDown, conectionLeft, conectionRight };
        foreach (ConeccionTubo coneccion in conecciones)
        {
            if (coneccion.personajeAlLado && !coneccion.personajeAdjacente.quieto)
            {
                playerCant++;
                playerMoviendo += coneccion.personajeAdjacente.quieto ? 0 : 1;
                Vector2 pjDestino = coneccion.personajeAdjacente.posicionDestino;
                Vector2 pjPosicion = coneccion.personajeAdjacente.transform.position;

                movimiento += (pjDestino - pjPosicion) * coneccion.personajeAdjacente.speed;
            }
        }

        if (playerCant == 2)
        {
            if (playerMoviendo > 1)
            {
                transform.Translate(movimiento / 2);
            }
            else
            {
                Vector3 pos = transform.position;
                pos.x = Mathf.Round(pos.x);
                pos.y = Mathf.Round(pos.y + .5f) - 0.5f;
                pos.z = Mathf.Round(pos.z);

                transform.position = pos;
            }
        }
    } 
}
