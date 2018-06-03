using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phone : MonoBehaviour 
{
	
	private Transform posPlayer;

	private Animator anim;

	private int i;
	private bool right;
	private bool left;
	private bool up;
	private bool down;

	public GameObject jogador;

	void Start () 
	{
		i = 1;
		anim = GetComponent<Animator> ();
		posPlayer = GameObject.Find ("Player").GetComponent<Transform> ();

	}
	

	void Update () 
	{
		right = Input.GetKeyDown (KeyCode.RightArrow);
		left = Input.GetKeyDown (KeyCode.LeftArrow);
		up = Input.GetKeyDown(KeyCode.UpArrow);
		down = Input.GetKeyDown(KeyCode.DownArrow);

		//acessa os aplicativos
		if (Input.GetKeyDown (KeyCode.Space))
			AcessaApp ();
	
		//muda de aplicativo para a direita
		if (right) 
		{
			i++;
			SetAnimation ();
		}
		//muda de aplicativo para a esquerda	
		if (left) 
		{
			i--;
			SetAnimation ();
		}
		//do primeiro app vai para o ultimo
		if (i < 1 && left) 
		{
			i = 6;
			SetAnimation ();
		}
		//do ultimo app volta para o primeiro
		if (i > 6 && right) 
		{
			i = 1;
			SetAnimation ();
		}

		//app1 para app4
		if (i == 1 && down) 
		{
			i = 4;
			SetAnimation ();
		}
		//app4 para app1
		if (i == 4 && up) 
		{
			i = 1;
			SetAnimation ();
		}
		//app2 para app5
		if (i == 2 && down) 
		{
			i = 5;
			SetAnimation ();
		}
		//app5 para app2
		if (i == 5 && up) 
		{
			i = 2;
			SetAnimation ();
		}
		//app3 para app6
		if (i == 3 && down) 
		{
			i = 6;
			SetAnimation ();
		}
		//app6 para app3
		if (i == 6 && up)
		{
			i = 3;
			SetAnimation ();
		}
	}


	public void SetAnimation()
	{
		anim.SetBool ("app1", i==1);
		anim.SetBool ("app2", i==2);
		anim.SetBool ("app3", i==3);
		anim.SetBool ("app4", i==4);
		anim.SetBool ("app5", i==5);
		anim.SetBool ("app6", i==6);
	}

	public void AcessaApp()
	{
		//app Mapa
		if (i == 1) 
		{
			
			SceneManager.LoadScene ("Mapa");

		}
		//app Lanterna, também acessivel com Input F;
		else if (i == 2) 
		{
			LuzCelular teste = GameObject.Find ("luzCel").GetComponent<LuzCelular> ();
			teste.EstadoLuz ();
		}
	}

}
