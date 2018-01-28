using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColaScript : NetworkBehaviour {
	//NetworkIdentity.ClientAuthorityCallback;
	public Sonidos DeepsSonidos;
	public Sonidos LubbSonidos;
	[SyncVar]
	public int codigo = 0;
	AudioClip nuevoSonido = null;

	//lista

	// Use this for initialization
	void Update () {
		/*Debug.Log (codigo);
		switch (codigo) {
			case 201: nuevoSonido = DeepsSonidos.perdida1; break;
			case 202: nuevoSonido = DeepsSonidos.perdida2; break;
			case 203: nuevoSonido = DeepsSonidos.perdida3; break;
			case 204: nuevoSonido = DeepsSonidos.alto; break;
			case 205: nuevoSonido = DeepsSonidos.cuidado; break;
			case 206: nuevoSonido = DeepsSonidos.retrocede; break;
			case 207: nuevoSonido = DeepsSonidos.aqui; break;
			case 208: nuevoSonido = DeepsSonidos.unodostres; break;
			case 209: nuevoSonido = DeepsSonidos.genial; break;
			case 210: nuevoSonido = DeepsSonidos.ven; break;
			case 211: nuevoSonido = DeepsSonidos.ahora; break;
			case 212: nuevoSonido = DeepsSonidos.porPoco; break;
			case 213: nuevoSonido = DeepsSonidos.tututu; break;
			case 214: nuevoSonido = DeepsSonidos.vamos; break;
			case 101: nuevoSonido = LubbSonidos.perdida1; break;
			case 102: nuevoSonido = LubbSonidos.perdida2; break;
			case 103: nuevoSonido = LubbSonidos.perdida3; break;
			case 104: nuevoSonido = LubbSonidos.alto; break;
			case 105: nuevoSonido = LubbSonidos.cuidado; break;
			case 106: nuevoSonido = LubbSonidos.retrocede; break;
			case 107: nuevoSonido = LubbSonidos.aqui; break;
			case 108: nuevoSonido = LubbSonidos.unodostres; break;
			case 109: nuevoSonido = LubbSonidos.genial; break;
			case 110: nuevoSonido = LubbSonidos.ven; break;
			case 111: nuevoSonido = LubbSonidos.ahora; break;
			case 112: nuevoSonido = LubbSonidos.porPoco; break;
			case 113: nuevoSonido = LubbSonidos.tututu; break;
			case 114: nuevoSonido = LubbSonidos.vamos; break;
		}
		if(codigo != 0){
			Debug.Log (codigo);
			AudioSource.PlayClipAtPoint (nuevoSonido, transform.position);
		}*/
	}

	public void reproducir(){
		switch (codigo) {
		case 201: nuevoSonido = DeepsSonidos.perdida1; break;
		case 202: nuevoSonido = DeepsSonidos.perdida2; break;
		case 203: nuevoSonido = DeepsSonidos.perdida3; break;
		case 204: nuevoSonido = DeepsSonidos.alto; break;
		case 205: nuevoSonido = DeepsSonidos.cuidado; break;
		case 206: nuevoSonido = DeepsSonidos.retrocede; break;
		case 207: nuevoSonido = DeepsSonidos.aqui; break;
		case 208: nuevoSonido = DeepsSonidos.unodostres; break;
		case 209: nuevoSonido = DeepsSonidos.genial; break;
		case 210: nuevoSonido = DeepsSonidos.ven; break;
		case 211: nuevoSonido = DeepsSonidos.ahora; break;
		case 212: nuevoSonido = DeepsSonidos.porPoco; break;
		case 213: nuevoSonido = DeepsSonidos.tututu; break;
		case 214: nuevoSonido = DeepsSonidos.vamos; break;
		case 101: nuevoSonido = LubbSonidos.perdida1; break;
		case 102: nuevoSonido = LubbSonidos.perdida2; break;
		case 103: nuevoSonido = LubbSonidos.perdida3; break;
		case 104: nuevoSonido = LubbSonidos.alto; break;
		case 105: nuevoSonido = LubbSonidos.cuidado; break;
		case 106: nuevoSonido = LubbSonidos.retrocede; break;
		case 107: nuevoSonido = LubbSonidos.aqui; break;
		case 108: nuevoSonido = LubbSonidos.unodostres; break;
		case 109: nuevoSonido = LubbSonidos.genial; break;
		case 110: nuevoSonido = LubbSonidos.ven; break;
		case 111: nuevoSonido = LubbSonidos.ahora; break;
		case 112: nuevoSonido = LubbSonidos.porPoco; break;
		case 113: nuevoSonido = LubbSonidos.tututu; break;
		case 114: nuevoSonido = LubbSonidos.vamos; break;
		}
		if(codigo != 0){
			AudioSource.PlayClipAtPoint (nuevoSonido, transform.position);
		}
	}


	void LateUpdate (){
		if(codigo != 0){
			codigo = 0;
		}
	}

}
