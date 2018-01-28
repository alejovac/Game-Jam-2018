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
        Vector2 movimiento = new Vector2(0,0);

        if (conectionUp.personajeAlLado)
            movimiento.y -= 1;
        if (conectionDown.personajeAlLado)
            movimiento.y += 1;
        if (conectionLeft.personajeAlLado)
            movimiento.x += 1;
        if (conectionRight.personajeAlLado)
            movimiento.x -= 1;

        transform.Translate(movimiento);
    } 
}
