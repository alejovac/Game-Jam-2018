using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GenerarBaseMapa : NetworkBehaviour {
    public int ancho = 16;
    public int alto = 9;
    
    public SyncList<Celda> plano;

    public int yEntrada = -1;
    public int ySalida = -1;
    System.Random rand;

    public float xoffset = -1.0f;
    public float yoffset = -1.0f;

    public GameObject tubo;

    // Use this for initialization
    void Start ()
    {
        if (!isServer)
            return;

        rand = new System.Random();
        plano = new SyncListStruct<Celda>();
        CmdClearPlano(Celda.vacia);
        
        GenerarCaminoPlano();
        GenerarCaminoPlano();
        LimpiarCaminoPlano();

        CmdInstanciarPlano();   
        Debug.Log("show alsdfsa;jfadj;lkfsdaj;");   
    }

    private int Posicion(int xx, int yy)
    {
        if (xx >= ancho)
            throw new System.IndexOutOfRangeException("Index: [" + xx + "," + yy + "]");

        return xx + yy * alto;
    }

    [Command]
    private void CmdClearPlano(Celda val)
    {
        for (int xx = 0; xx < ancho; xx++)
            for (int yy = 0; yy < alto; yy++)
                plano.Add(Celda.vacia);
    }

    private void GenerarCaminoPlano()
    {
        if (yEntrada < 0)
            yEntrada = rand.Next((int)(alto/2))*2;

        plano[Posicion(0, yEntrada)] = Celda.tubo;
        plano[Posicion(1, yEntrada)] = Celda.tubo;

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

                plano[Posicion(xx, yy)] = Celda.tubo;
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
                    if (plano[Posicion(xx, yy)] == Celda.tubo)
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
                                if (plano[Posicion(xx + despX, yy + despY)] == Celda.tubo)
                                    tubosAdyacentes += 1;
                            }
                        }

                        if (tubosAdyacentes == 1)
                        {
                            limpio = false;
                            plano[Posicion(xx, yy)] = Celda.vacia;
                        }
                    }
                }
            }
        }
    }

    [Command]
    private void CmdInstanciarPlano()
    {
        Debug.Log("show alsdfsa;jfadj;lkfsdaj;");
        for (int xx = 0; xx < ancho; xx++)
            for (int yy = 0; yy < alto; yy++)
                if (plano[Posicion(xx, yy)] == Celda.tubo)
                    Instantiate(tubo, new Vector3(xoffset + xx, yoffset + yy, -yy / 10), Quaternion.identity);
    }
    
}

public enum Celda { trampa, tubo, vacia }