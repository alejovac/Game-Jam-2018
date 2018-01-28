using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GenerarBaseMapa : NetworkBehaviour {
    public int ancho = 16;
    public int alto = 9;
    
    public Celda[,] plano;
    public Sprite[,] imagenes;
    public int[,] imagenCodigo;

    public int yEntrada = -1;
    public int ySalida = -1;
    System.Random rand;

    public float xoffset = -1.0f;
    public float yoffset = -1.0f;

    public GameObject tubo;
    public GameObject piso;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            rand = new System.Random();
            plano = new Celda[ancho, alto];
            imagenes = new Sprite[ancho, alto];
            imagenCodigo = new int[ancho, alto];
            ClearPlano(Celda.vacia);

            GenerarCaminoPlano();
            //GenerarCaminoPlano();
            LimpiarCaminoPlano();
            CalcularImagenes();
            MezclarTuberias(3);

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

    private void CalcularImagenes()
    {
        for (int xx = 0; xx < ancho; xx++)
        {
            for (int yy = 0; yy < alto; yy++)
            {
                if (plano[xx, yy] == Celda.tubo)
                {
                    bool tuboIzq = (xx - 1) >= 0;
                    bool tuboAba = (yy - 1) >= 0;
                    bool tuboArr = (yy + 1) < alto;
                    bool tuboDer = (xx + 1) < ancho;

                    if (tuboIzq) tuboIzq = plano[xx - 1, yy] == Celda.tubo;
                    if (tuboDer) tuboDer = plano[xx + 1, yy] == Celda.tubo;
                    if (tuboArr) tuboArr = plano[xx, yy + 1] == Celda.tubo;
                    if (tuboAba) tuboAba = plano[xx, yy - 1] == Celda.tubo;

                    int coneccionCodigo = (tuboDer ? 1000 : 0) + (tuboArr ? 100 : 0) + (tuboIzq ? 10 : 0) + (tuboAba ? 1 : 0);
                    imagenCodigo[xx, yy] = coneccionCodigo;
                }
            }
        }
    }

    private void MezclarTuberias(int cantidad)
    {
        int intentos = 100;
        while (cantidad > 0 && intentos > 0)
        {
            int xx = rand.Next(1, ancho - 2);
            int yy = rand.Next(0, alto  - 1);

            if (plano[xx, yy] == Celda.tubo)
            {
                int sigX = rand.Next(1, ancho - 2);
                int sigY = rand.Next(0, alto - 1);

                if (plano[sigX, sigY] == Celda.vacia)
                {
                    plano[sigX, sigY] = Celda.tuboSuelto;
                    imagenes[sigX, sigY] = imagenes[xx, yy];
                    imagenCodigo[sigX, sigY] = imagenCodigo[xx, yy];

                    plano[xx, yy] = Celda.vacia;
                    imagenes[xx, yy] = null;
                    imagenCodigo[xx, yy] = 0;
                    cantidad--;
                }
            }

            intentos--;
        }
    }

    private void InstanciarPlano()
    {
        for (int xx = 0; xx < ancho; xx++)
        {
            for (int yy = 0; yy < alto; yy++)
            {
                if (plano[xx, yy] == Celda.tubo || plano[xx,yy] == Celda.tuboSuelto)
                {
                    GameObject tuboIns = Instantiate(tubo, new Vector3(xoffset + xx, yoffset + yy, -.15f), Quaternion.identity);
                    
                    tuboIns.GetComponent<UpdateTiles>().codigoImagen = imagenCodigo[xx, yy];

                    if (plano[xx, yy] == Celda.tubo)
                        Destroy(tuboIns.GetComponent<PegarsePersonaje>());

                    NetworkServer.Spawn(tuboIns);
                }

                if (xx > 0 && xx < ancho - 1)
                {
                    GameObject pisoIns = Instantiate(piso, new Vector3(xoffset + xx, yoffset + yy, 1), Quaternion.identity);
                    NetworkServer.Spawn(pisoIns);
                }
            }
        }
    }
    
}

public enum Celda { trampa, tubo, tuboSuelto, vacia }