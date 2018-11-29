using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellphoneScript : MonoBehaviour {

    public GameObject jogador;
    public GameObject mainCan;
	public GameObject mapCan;
    public GameObject notasMusicais;

    public static int i;
    public static bool celularLigado;
    public static bool ligouMusica = false;

    private Animator anim;
    private Transform posPlayer;
    private AudioSource[] sounds;
    private AudioSource scrollSound;
    private AudioSource music;

    private bool right;
	private bool left;
	private bool up;
	private bool down;
    private bool aux;
	
	void Awake()
	{
		i = 1;
        notasMusicais.SetActive(false);
    }

	void Start () 
	{
        notasMusicais.SetActive(false);
		anim = GetComponent<Animator> ();
		posPlayer = GameObject.Find ("Player").GetComponent<Transform> ();
		sounds = GetComponents<AudioSource> ();
		scrollSound = sounds [0];
        music = sounds[1];
	}

    void OnEnable()
    {
        aux = false;
        i = 1;
    }

    void OnDisable()
    {
        aux = false;
        notasMusicais.SetActive(false);
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
		
		if (!mapCan.activeInHierarchy) //camera dois esta desativada
		{

			//muda de aplicativo para a direita
			if (right) 
			{
				i++;
				if (i > 4)
					i = 1;
				SetAnimation ();
				scrollSound.Play ();
			}
			//muda de aplicativo para a esquerda	
			if (left) 
			{
				i--;
				if (i < 1)
					i = 4;
				SetAnimation ();
				scrollSound.Play ();
			}
			//app1 para app4
			if (i == 1 && down) 
			{
				i = 3;
				SetAnimation ();
				scrollSound.Play ();
			}
			//app3 para app1
			if (i == 3 && up) 
			{
				i = 1;
				SetAnimation ();
				scrollSound.Play ();
			}
			//app2 para app4
			if (i == 2 && down) 
			{
				i = 4;
				SetAnimation ();
				scrollSound.Play ();
			}
			//app4 para app2
			if (i == 4 && up) 
			{
				i = 2;
				SetAnimation ();
				scrollSound.Play ();
			}
		}
	}


	public void SetAnimation()
	{
		anim.SetInteger ("apps", i);
	}

	public void AcessaApp()
	{
		//app Mapa
		if (i == 1) 
		{
			if(mainCan.activeInHierarchy)
				mainCan.gameObject.SetActive(false);
			else
				mainCan.gameObject.SetActive(true);

			if(mapCan.activeInHierarchy)
				mapCan.gameObject.SetActive(false);
			else
				mapCan.gameObject.SetActive(true);

		}
		//app Lanterna, também acessivel com Input F;
		if (i == 2) 
		{
			LuzCelular teste = GameObject.Find ("luzCel").GetComponent<LuzCelular> ();
			i = 1;
			teste.EstadoLuz ();
		}
        //música
        if(i == 3)
        {
            if(aux == false)
            {                
                music.Play();
                notasMusicais.SetActive(true);
                aux = true;
                ligouMusica = true;
                EstadosCelular.estadoCelular = "musica";
            }
            else
            {
                music.Stop();
                notasMusicais.SetActive(false);
                aux = false;
                ligouMusica = false;
                EstadosCelular.estadoCelular = "ligado";
            }
        }

        
    }
    
}
