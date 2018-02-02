using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ElegirTilePiso : NetworkBehaviour {
    static System.Random rand = new System.Random();
    [SyncVar]
    public int imgChoose = -1;
    bool actualizo = false;

    public SetImagenes imagenesServer;
    public SetImagenes imagenesCliente;

    // Use this for initialization
    void Start () {
        imgChoose = rand.Next(3);
	}

    // Update is called once per frame
    void Update()
    {
        if (imgChoose >= 0 && !actualizo)
        {
            SetImagenes imagenes = isServer ? imagenesServer : imagenesCliente;
            Sprite[] opciones = { imagenes.pisoTile_normal, imagenes.pisoTile_1, imagenes.pisoTile_2, imagenes.pisoTile_3 };

            if (rand.NextDouble() < .8)
                GetComponent<SpriteRenderer>().sprite = imagenes.pisoTile_normal;
            else GetComponent<SpriteRenderer>().sprite = opciones[imgChoose];

            actualizo = true;
        }
    }
}
