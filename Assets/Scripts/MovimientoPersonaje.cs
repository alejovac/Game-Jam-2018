﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovimientoPersonaje : NetworkBehaviour {
    public float speed = 0.10f;
    public bool quieto;
    public Vector2 tamCelda;

    public Vector2 bordeIzquierdaAbajo;
    public Vector2 bordeDerechaArriba;

    private Animator anim;
    private bool isMoving;
    private bool isCharging;
    private Vector2 lastMovement;
    private Vector2 currentMovement;

    [SyncVar]
    public Vector2 posicionDestino;
    
    void Start()
    {
        posicionDestino = new Vector2(0, 0.5f);
        anim = GetComponent<Animator>();
    }

	void Update ()
    {
        isMoving = false;
        if (isLocalPlayer){
            Vector2 distanciaDestino = posicionDestino - new Vector2(transform.position.x, transform.position.y);
        quieto = distanciaDestino.magnitude < 0.05f;
        if (quieto)
        {
            float xAxis = (Input.GetKeyDown("right") ? 1 : 0) - (Input.GetKeyDown("left") ? 1 : 0);
            float yAxis = (Input.GetKeyDown("up") ? 1 : 0) - (Input.GetKeyDown("down") ? 1 : 0);

            Vector2 desplazamiento = new Vector2(xAxis * tamCelda.x, yAxis * tamCelda.y);

            var xValido = (posicionDestino.x + desplazamiento.x) < bordeDerechaArriba.x && (posicionDestino.x + desplazamiento.x) > bordeIzquierdaAbajo.x;
            var yValido = (posicionDestino.y + desplazamiento.y) < bordeDerechaArriba.y && (posicionDestino.y + desplazamiento.y) > bordeIzquierdaAbajo.y;

            if (xValido && yValido)
                posicionDestino = posicionDestino + new Vector2(xAxis, yAxis);

            //float xAxis = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            //float yAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //transform.Translate(xAxis, yAxis, 0);
            if (xValido && xAxis != 0)
            {
                isMoving = true;
                lastMovement = new Vector2(xAxis, 0f);
            }
            if (yValido & yAxis != 0)
            {
                isMoving = true;
                lastMovement = new Vector2(0f, yAxis);
            }
        }

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, posicionDestino.x, speed), Mathf.Lerp(transform.position.y, posicionDestino.y, speed), transform.position.z);
        }
        anim.SetFloat("MoveY", lastMovement.y);
        anim.SetFloat("MoveX", lastMovement.x);
        anim.SetBool("IsMoving", isMoving);
    }
}
