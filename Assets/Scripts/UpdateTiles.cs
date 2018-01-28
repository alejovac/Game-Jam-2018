using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateTiles : NetworkBehaviour
{
    [SyncVar]
    public int codigoImagen = 0;
    public bool actualizo = false;
    public SpriteRenderer imagen;
    public SetImagenes imagenesServer;
    public SetImagenes imagenesClient;
    
    void Update () {
        if (codigoImagen != 0 && !actualizo) {
            actualizo = true;
            SetImagenes setImagenes = isServer ? imagenesServer : imagenesClient;

            switch (codigoImagen)
            {
                case 1111: imagen.sprite = setImagenes.tuboTile_cruz; break;

                case 1110: imagen.sprite = setImagenes.tuboTile_formaT_ArrDerIzq; break;
                case 1101: imagen.sprite = setImagenes.tuboTile_formaT_DerAbaArr; break;
                case 1011: imagen.sprite = setImagenes.tuboTile_formaT_AbaDerIzq; break;
                case 0111: imagen.sprite = setImagenes.tuboTile_formaT_IqzAbaArr; break;

                case 0011: imagen.sprite = setImagenes.tuboTile_codo_AbaIzq; break;
                case 1001: imagen.sprite = setImagenes.tuboTile_codo_AbaDer; break;
                case 0110: imagen.sprite = setImagenes.tuboTile_codo_ArrIzq; break;
                case 1100: imagen.sprite = setImagenes.tuboTile_codo_ArrDer; break;

                case 1010: imagen.sprite = setImagenes.tuboTile_linea_hori; break;
                case 0101: imagen.sprite = setImagenes.tuboTile_linea_vert; break;

                case 1000: imagen.sprite = setImagenes.tuboTile_terminal_Izq; break;
                case 0010: imagen.sprite = setImagenes.tuboTile_terminal_Der; break;
            }
        }
    }
}
