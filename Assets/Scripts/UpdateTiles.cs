using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTiles : MonoBehaviour {
    public Sprite crossSprite;

    public Sprite TspriteULR;
    public Sprite TspriteDLR;
    public Sprite TspriteUDR;
    public Sprite TspriteUDL;

    public Sprite curvedSpriteUL;
    public Sprite curvedSpriteUR;
    public Sprite curvedSpriteDL;
    public Sprite curvedSpriteDR;

    public Sprite lineSpriteH;
    public Sprite lineSpriteV;

    public ConeccionTubo conectionUp;
    public ConeccionTubo conectionDown;
    public ConeccionTubo conectionLeft;
    public ConeccionTubo conectionRight;

    public SpriteRenderer imagen;

    bool prevTouchUp = false;
    bool prevTouchDown = false;
    bool prevTouchLeft = false;
    bool prevTouchRight = false;
    
	void Update () {
        bool cambiaUp    = conectionUp   .conectado != prevTouchUp   ;
        bool cambiaDown  = conectionDown .conectado != prevTouchDown ;
        bool cambiaLeft  = conectionLeft .conectado != prevTouchLeft ;
        bool cambiaRight = conectionRight.conectado != prevTouchRight;

        if (cambiaUp || cambiaDown || cambiaLeft || cambiaRight)
        {
            prevTouchUp    = conectionUp   .conectado;
            prevTouchDown  = conectionDown .conectado;
            prevTouchLeft  = conectionLeft .conectado;
            prevTouchRight = conectionRight.conectado;
            
            int coneccionCodigo = (prevTouchRight ? 1000 : 0) + (prevTouchUp ? 100 : 0) + (prevTouchLeft ? 10 : 0) + (prevTouchDown ? 1 : 0);
            switch (coneccionCodigo)
            {
                case 1111: imagen.sprite = crossSprite; break;

                case 1110: imagen.sprite = TspriteULR; break;
                case 1101: imagen.sprite = TspriteUDR; break;
                case 1011: imagen.sprite = TspriteDLR; break;
                case 0111: imagen.sprite = TspriteUDL; break;

                case 0011: imagen.sprite = curvedSpriteDL; break;
                case 1001: imagen.sprite = curvedSpriteDR; break;
                case 0110: imagen.sprite = curvedSpriteUL; break;
                case 1100: imagen.sprite = curvedSpriteUR; break;

                case 1010: imagen.sprite = lineSpriteH; break;
                case 0101: imagen.sprite = lineSpriteV; break;
            }

        }
	}
}
