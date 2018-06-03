using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LuzCelular : MonoBehaviour 
{

	public GameObject Celular;

	private bool ligado;
	private Light luzCelular;
	public bool luzCelularEstado;
	private Player lauren;
	private float mover;

	public bool can;
	public GameObject cellphone;
	// Use this for initialization
	void Awake () 
	{
		can = true;
		luzCelular = GetComponent<Light> ();
		luzCelular.enabled = false;
		lauren = GameObject.Find("Player").GetComponent<Player> ();
		luzCelularEstado = false;
    }
	
	// Update is called once per frame
	void Update () 
	{
		mover = Player.move;
		//Só pode pegar a lanterna se não estiver escondida
		if (Input.GetKeyDown (KeyCode.F) && can && lauren.tag == "Player") {
			can = false;
			StartCoroutine ("Tempo");
			EstadoLuz ();
		}

	}

	public void EstadoLuz()
	{
		//atraso para ativar a lantera (mudar depois pois fica dando lag se acender a lantera muitas vezes)
		Thread.Sleep (120);
		luzCelularEstado=luzCelular.enabled = !luzCelular.enabled;
        
    }
	IEnumerator Tempo()
	{
		yield return new WaitForSeconds (0.5f);
		can = true;
	}
}
