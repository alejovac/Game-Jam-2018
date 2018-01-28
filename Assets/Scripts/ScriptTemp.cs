using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTemp : MonoBehaviour {
    public static int maxMove = 3;
    static System.Random succed = new System.Random();
    public float chanceDestroy = 0.3f;
    public float top;
    public float botton;
    public float right;
    public float left;

    void LateUpdate()
    {
        if (GetComponent<UpdateTiles>() == null) {
            if (succed.NextDouble() < chanceDestroy )
            {
                if (transform.position.x > left && transform.position.x < right && maxMove > 0)
                {
                    // Destroy(GetComponent<UpdateTiles>());
                    Destroy(GetComponent<PegarsePersonaje>());
                    Vector2 nuevaPosicion = new Vector2(succed.Next((int)left, (int)right), succed.Next((int)botton, (int)top) - 0.5f);
                    transform.position = nuevaPosicion;
                    maxMove--;
                }
            }

            Destroy(this);
        }
    }
}
