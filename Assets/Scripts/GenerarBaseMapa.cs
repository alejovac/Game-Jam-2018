using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GenerarBaseMapa : NetworkBehaviour {
    public int ancho = 16;
    public int alto = 9;
    
    public Celda[,] plano;
    public Sprite[,] imagenes;

    public int yEntrada = -1;
    public int ySalida = -1;
    System.Random rand;

    public float xoffset = -1.0f;
    public float yoffset = -1.0f;

    public GameObject tubo;

    public SetImagenes imagenesServer;
    public SetImagenes imagenesCliente;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            rand = new System.Random();
            plano = new Celda[ancho, alto];
            imagenes = new Sprite[ancho, alto];
            ClearPlano(Celda.vacia);

            GenerarCaminoPlano();
            //GenerarCaminoPlano();
            LimpiarCaminoPlano();
            CalcularImagenes(imagenesServer);

            InstanciarPlano();
        }
    }
    
    private void ClearPlano(Celda val)
    {
        for (int xx = 0; xx < ancho; xx++)
            for (int yy = 0; yy < alto; yy++)
                plano[xx,yy] = Celda.vacia;
    }
    
    private void GenerarCaminoPlano()
    {
        if (yEntrada < 0)
            yEntrada = rand.Next((int)(alto/2))*2;

        plano[0, yEntrada] = Celda.tubo;
        plano[1, yEntrada] = Celda.tubo;

        int xx = 1;
        int yy = yEntrada;

        float direccionAnt = 0;

        while (xx < ancho- 1)
        {
            float direccion = direccionAnt + ((float)rand.Next(3)-1) * Mathf.PI / 2f;
            //direccionAnt = direccion;

            int despX = Mathf.RoundToInt(Mathf.Cos(direccion));
            int despY = Mathf.RoundToInt(Mathf.Sin(direccion));

            for (int repeat = 0; repeat < 2 && xx < ancho - 1; repeat++)
            {
                xx = Mathf.Clamp(xx + despX, 1, ancho - 1);
                yy = Mathf.Clamp(yy + despY, 0, alto - 1);

                plano[xx, yy] = Celda.tubo;
            }
        }
    }
    
    private void LimpiarCaminoPlano()
    {
        bool limpio = false;
        while (!limpio)
        {
            limpio = true;
            for (int xx = 1; xx < ancho-1; xx++)
            {
                for (int yy = 0; yy < alto; yy++)
                {
                    if (plano[xx, yy] == Celda.tubo)
                    {
                        int tubosAdyacentes = 0;
                        for (float direccion = 0; direccion < 1.8f * Mathf.PI; direccion += Mathf.PI / 2f)
                        {
                            int despX = Mathf.RoundToInt(Mathf.Cos(direccion));
                            int despY = Mathf.RoundToInt(Mathf.Sin(direccion));

                            bool xValido = xx + despX < ancho && xx + despX >= 0;
                            bool yValido = yy + despY < alto && yy + despY >= 0;

                            if (xValido && yValido)
                            {
                                if (plano[xx + despX, yy + despY] == Celda.tubo)
                                    tubosAdyacentes += 1;
                            }
                        }

                        if (tubosAdyacentes == 1)
                        {
                            limpio = false;
                            plano[xx, yy] = Celda.vacia;
                        }
                    }
                }
            }
        }
    }

    private void CalcularImagenes(SetImagenes setImagenes)
    {
        for (int xx = 0; xx < ancho; xx++)
        {
            for (int yy = 0; yy < alto; yy++)
            {
                if (plano[xx, yy] == Celda.tubo)
                {
                    bool tuboIzq = (xx - 1) > 0;
                    bool tuboAba = (yy - 1) > 0;
                    bool tuboArr = (yy + 1) < alto - 1;
                    bool tuboDer = (xx + 1) < ancho - 1;

                    if (tuboIzq) tuboIzq = plano[xx - 1, yy] == Celda.tubo;
                    if (tuboDer) tuboDer = plano[xx + 1, yy] == Celda.tubo;
                    if (tuboArr) tuboArr = plano[xx, yy + 1] == Celda.tubo;
                    if (tuboAba) tuboAba = plano[xx, yy - 1] == Celda.tubo;

                    int coneccionCodigo = (tuboDer ? 1000 : 0) + (tuboArr ? 100 : 0) + (tuboIzq ? 10 : 0) + (tuboAba ? 1 : 0);
                    switch (coneccionCodigo)
                    {
                        case 1111: imagenes[xx,yy] = setImagenes.tuboTile_cruz; break;

                        case 1110: imagenes[xx, yy] = setImagenes.tuboTile_formaT_ArrDerIzq; break;
                        case 1101: imagenes[xx, yy] = setImagenes.tuboTile_formaT_DerAbaArr; break;
                        case 1011: imagenes[xx, yy] = setImagenes.tuboTile_formaT_AbaDerIzq; break;
                        case 0111: imagenes[xx, yy] = setImagenes.tuboTile_formaT_IqzAbaArr; break;

                        case 0011: imagenes[xx, yy] = setImagenes.tuboTile_codo_AbaIzq; break;
                        case 1001: imagenes[xx, yy] = setImagenes.tuboTile_codo_AbaDer; break;
                        case 0110: imagenes[xx, yy] = setImagenes.tuboTile_codo_ArrIzq; break;
                        case 1100: imagenes[xx, yy] = setImagenes.tuboTile_codo_ArrDer; break;

                        case 1010: imagenes[xx, yy] = setImagenes.tuboTile_linea_hori; break;
                        case 0101: imagenes[xx, yy] = setImagenes.tuboTile_linea_vert; break;
                    }
                }
            }
        }
    }

    private void InstanciarPlano()
    {
        for (int xx = 0; xx < ancho; xx++)
        {
            for (int yy = 0; yy < alto; yy++)
            {
                if (plano[xx, yy] == Celda.tubo)
                {
                    GameObject tuboIns = Instantiate(tubo, new Vector3(xoffset + xx, yoffset + yy, -.15f), Quaternion.identity);
                    tuboIns.GetComponentInChildren<SpriteRenderer>().sprite = imagenes[xx, yy];
                    NetworkServer.Spawn(tuboIns);
                }
            }
        }
    }
    
}

public enum Celda { trampa, tubo, vacia }