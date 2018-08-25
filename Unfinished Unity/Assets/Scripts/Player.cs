using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public Transform Check;
	public GameObject phonePrefab;
	public GameObject luzCel;
	public float speed;
	public static float auxSpeed;
	public bool destroyPhone;

	public static float move;
	public static bool seEscondeu;
    public static string estadoMental;
    public static float numEstadoMental;

	private Transform phoneInstatiate;
	private SpriteRenderer sprite;
    //LUZES E CORES ARCADES
    private SpriteRenderer tv;
    public Sprite red;
    public Sprite green;
    public Sprite blue;
	public Sprite cian;
	public Sprite magenta;
	public Sprite yellow;
	public Sprite white;
	public Sprite black;
    public GameObject luzRed;
    public GameObject luzGreen;
    public GameObject luzBlue;
    public GameObject luzTVW;
    public GameObject luzTVRed;
    public GameObject luzTVGreen;
    public GameObject luzTVBlue;
    public GameObject luzTVC;
    public GameObject luzTVM;
    public GameObject luzTVY;
    public GameObject somLuzBanheiro;
    //-------------------------------------
    private GameObject cartuchoRed;
    private GameObject cartuchoGreen;
    private GameObject cartuchoBlue;
    private Animator anim;
	private Rigidbody2D rb;
	private CapsuleCollider2D cap;
	private bool clonePhone;
	private AudioSource [] sounds;
	private AudioSource doorSound;
	private AudioSource laurenWalk;

    /*[SerializeField]
    private GameObject caixaDialogo;
    private bool tomNPC = false;
    private bool fliperamaRed = false;
    private bool fliperamaGreen = false;
    private bool fliperamaBlue = false;
    private bool posCartuchoRed;
    private bool posCartuchoGreen;
    private bool posCartuchoBlue;
    private bool pegouCatRed;
    private bool pegouCatGreen;
    private bool pegouCatBlue;*/



    private bool estadoDaLuz;
	private bool block = false;
	private bool facingRight = true ;
	private bool pegarCelular;
	public static bool canMove;
	private bool botaoE;
	private bool canHide;
	private int aux = 0;
	public float posx;
	public float posy;
    private bool apresentacaoTom = true;



	//variaveis de transição de cena
	private bool cena1 = false;
	private bool cena2l = false;
	private bool cena2r = false;
	private bool cena3l = false;
	private bool cena3t = false;
	private bool cena3r = false;
	private bool cena3_5 = false;
	private bool cena4l = false;
	private bool cena4r = false;
    private bool cena4t = false;
    private bool cena4_5 = false;
	private bool cena5l = false;
	private bool cena5b = false;
	private bool cena5r = false;
	private bool cena6 = false;
	private bool cena7 = false;

	public bool can;
	public GameObject cellphone;

    private void Awake()
    {
        estadoMental = "fine";
        numEstadoMental = 100;
    }

    void Start () 
	{
		clonePhone = true;
        cellphone.SetActive (false);
		can = true;
		seEscondeu = false;
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		phoneInstatiate = GameObject.Find ("insPhone").GetComponent<Transform> ();
		auxSpeed = speed;
		canHide = false;
		aux = 0;
		canMove = true;
		cap = GetComponent<CapsuleCollider2D> ();
        GameObject.FindGameObjectWithTag("Filtro").GetComponent<SpriteRenderer>().enabled = true;
        //Referente ao Puzzle ------------------
        /*tv = GameObject.FindGameObjectWithTag("Tv").GetComponent<SpriteRenderer>();
        cartuchoRed = GameObject.FindGameObjectWithTag("CartuchoRed");
        cartuchoRed.SetActive(true);
        cartuchoGreen = GameObject.FindGameObjectWithTag("CartuchoGreen");
        cartuchoGreen.SetActive(true);
        cartuchoBlue = GameObject.FindGameObjectWithTag("CartuchoBlue");
        cartuchoBlue.SetActive(true);
        caixaDialogo.SetActive(false);
        posCartuchoRed = false;
        posCartuchoGreen = false;
        posCartuchoBlue = false;
        pegouCatRed = false;
        pegouCatGreen = false;
        pegouCatBlue = false;*/
        
        //LUZES
        luzRed.SetActive(false);
        luzGreen.SetActive(false);
        luzBlue.SetActive(false);
        luzTVW.SetActive(false);
        luzTVRed.SetActive(false);
        luzTVGreen.SetActive(false);
        luzTVBlue.SetActive(false);
        luzTVC.SetActive(false);
        luzTVM.SetActive(false);
        luzTVY.SetActive(false);
        somLuzBanheiro.SetActive(false);
        // -------------------- //
        sounds = GetComponents<AudioSource> ();
		doorSound = sounds [0];
		laurenWalk = sounds [1]; // som passos da Lauren
    }
	

	void Update ()
	{
        posx = this.transform.position.x;
		posy = this.transform.position.y;
		estadoDaLuz = luzCel.GetComponent<LuzCelular> ().luzCelularEstado;
        //PuzzleRGB();

		//SHORTCUTS MANEIROS

		//DESABILITA O FILTRO MACABRO
		if (Input.GetKeyDown ("u") && GameObject.FindGameObjectWithTag ("Filtro").GetComponent<SpriteRenderer> ().enabled == true)
			GameObject.FindGameObjectWithTag ("Filtro").GetComponent<SpriteRenderer> ().enabled = false;
		else if (Input.GetKeyDown ("u") && GameObject.FindGameObjectWithTag ("Filtro").GetComponent<SpriteRenderer> ().enabled == false)
			GameObject.FindGameObjectWithTag ("Filtro").GetComponent<SpriteRenderer> ().enabled = true;
		//DESABILITA O FILTRO PETRO


		/*block recebe uma linha que vai da posição do personagem até a posição do Check e verifica se tem
		contato com a Layer chamada Block */
		block = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Block"));

		//verifica se a linha do personagem colidiu com a layer da cena 1 = dispensa
		cena1 = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc1"));
		//verifica se a linha do personagem colidiu com a layer da cena 2 left = cozinha na porta esquerda
		cena2l = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc2l"));
		//verifica se a linha do personagem colidiu com a layer da cena 2 right = cozinha na porta direita
		cena2r = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc2r"));
		//verifica se a linha do personagem colidiu com a layer da cena 3 left = sala de jantar na porta esquerda
		cena3l = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc3l"));
		//verifica se a linha do personagem colidiu com a layer da cena 3 top = sala de jantar na porta de cima que leva pro banheiro
		cena3t = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc3t"));
		//verifica se a linha do personagem colidiu com a layer da cena 3 right = sala de jantar na porta da direita
		cena3r = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc3r"));
		//verifica se a linha do personagem colidiu com a layer da cena 3_5 = banheiro
		cena3_5 = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc3_5"));
		//verifica se a linha do personagem colidiu com a layer da cena 4 left = salão principal na porta esquerda
		cena4l = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc4l"));
		//verifica se a linha do personagem colidiu com a layer da cena 4 right = salão principal na porta da direita
		cena4r = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc4r"));
		//verifica se a linha do personagem colidiu com a layer da cena 4 top = Sala de jogos que leva a salao principal
		cena4t = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc4t"));
		//verifica se a linha do personagem colidiu com a layer da cena 4_5 = salão principal que leva a sala de jogos
		cena4_5 = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc4_5"));
		//verifica se a linha do personagem colidiu com a layer da cena 5 left = quintal na porta de esquerda
		cena5l = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc5l"));
		//verifica se a linha do personagem colidiu com a layer da cena 5 bottom = quital na porta de baixo que leva pro jardim
		cena5b = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc5b"));
		//verifica se a linha do personagem colidiu com a layer da cena 5 right = quintal na porta da direita
		cena5r = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc5r"));
		//verifica se a linha do personagem colidiu com a layer da cena 6 = garagem
		cena6 = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc6"));
		//verifica se a linha do personagem colidiu com a layer da cena 7 = jardim
		cena7 = Physics2D.Linecast (transform.position, Check.position, 1 << LayerMask.NameToLayer ("Sc7"));

        //Verifica se a Lauren entrou em colisao com o Tom NPC, que esta na sala de jogos
        //tomNPC = Physics2D.Linecast(transform.position, Check.position, 1 << LayerMask.NameToLayer("TomNPC"));


        //se colidir com a parede, a velocidade dela fica 0 e mostra animação dela parada
        if (block) {
			speed = 0f;
			SetAnimations (0);
		} else
			speed = auxSpeed;


		//troca a posição do player para o proximo cenário

		if (cena2l && Input.GetKeyDown ("e")) {  //Se ela estiver na dispensa, encostar na porta e aperta o E, a posição dela muda para a cozinha na porta esquerda
			this.transform.position = new Vector3 (-29.2f, -1f, 0f);
			doorSound.Play ();
		}
		
		if (cena1 && Input.GetKeyDown ("e")) {  //Se ela estiver na cozinha, encostar na porta esquerda e apertar o E, a posição dela muda para a dispensa
			this.transform.position = new Vector3 (-48.88f, -1f, 0f);
			doorSound.Play ();
		}
		if (cena3l && Input.GetKeyDown ("e")) { //Se ela estiver na cozinha, encostar na porta direita e apertar o E, muda para sala de jantar na porta esquerda
			this.transform.position = new Vector3 (-7f, -1f, 0f);
			doorSound.Play ();
		}
		if (cena2r && Input.GetKeyDown ("e")) { //Se ela estiver na sala de jantar, encostar na porta esquerda e aperta o E, muda para cozinha na porta direita
			this.transform.position = new Vector3 (-20.77f, -1f, 0f);
			doorSound.Play ();
		}
		if (cena3_5 && Input.GetKeyDown ("e")) { //Se ela estiver na sala de jantar, encostar na porta cima e aperta o E, muda para banheiro
			this.transform.position = new Vector3 (3f, 24f, 0f);
            somLuzBanheiro.SetActive(true);
			doorSound.Play ();
		}
		if (cena4l && Input.GetKeyDown ("e")) { //Se ela estiver na sala de jantar, encostar na porta direita e apertar o E, muda para salão princ na porta esquerda
			this.transform.position = new Vector3 (20f, -2f, 0f);
			doorSound.Play ();

		}
		if (cena3t && Input.GetKeyDown ("e")) { //Se ela estiver no banheiro, encostar na porta e apertar o E, muda para a sala de jantar na porta cima
			this.transform.position = new Vector3 (-5f, -1f, 0f);
            somLuzBanheiro.SetActive(false);
            doorSound.Play ();
		}
		if (cena3r && Input.GetKeyDown ("e")) { //Se ela estiver no salão princ, encostar na porta esquerda e apertar o E, muda para sala de jantar na porta direita
			this.transform.position = new Vector3 (7f, -1f, 0f);
			doorSound.Play ();
		}
		if (cena5l && Input.GetKeyDown ("e")) { // Se ela estiver no salão princ, encostar na porta direita e apertar o E, muda para o quintal
			this.transform.position = new Vector3 (43f, -1.5f, 0f);
			doorSound.Play ();
		}
		if (cena4r && Input.GetKeyDown ("e") || Input.GetKeyDown ("h")) { // Se ela estiver no quintal, encostar na porta esquerda e apertar o E, muda para salão princ na porta direita
			this.transform.position = new Vector3 (30f, -2f, 0f);
			doorSound.Play ();
		}
		if (cena4_5 && Input.GetKeyDown ("e")) { // Se ela estiver no salão principal na porta do meio e apertar E, muda pra sala de jogos
			this.transform.position = new Vector3 (21f, 24f, 0f);
			doorSound.Play ();

            if (apresentacaoTom)//entra só uma vez nesse if
            {
                Dialogue1.estado = "apresentacao";
                apresentacaoTom = false;
				transform.gameObject.tag = "pEscondido";
            }

        }
		if (cena4t && Input.GetKeyDown ("e") && transform.gameObject.tag == "Player") { //Se ela estiver no salao de jogos na porta e apertar E, muda pra porta do meio no salao principal
			this.transform.position = new Vector3 (29f, -1.8f, 0f);
			doorSound.Play ();
		}

		if (cena6 || Input.GetKeyDown ("g")) { // Se ela estiver no quintal e seguir a placa da garagem, muda para a garagem
			this.transform.position = new Vector3 (67.5f, -1f, 0f);
			doorSound.Play ();
		}
		if (cena7 && Input.GetKeyDown ("e")) // Se ela estiver no quintal, encostar na porta baixo e apertar o E, muda para o jardim
		{
			this.transform.position = new Vector3 (42f, -26.5f, 0f);
			doorSound.Play ();
		}

		if (cena5b && Input.GetKeyDown ("e")) // Se ela estiver no jardim, encostar na porta e apertar E, muda para quintal na porta de baixo
		{
			this.transform.position = new Vector3 (45f, -1.5f, 0f);
			doorSound.Play ();
		}
		if (cena5r) // Se ela estiver na garagem e seguir na esquerda, muda para quintal na parte direita
		{
			this.transform.position = new Vector3 (57f, -1.5f, 0f);
			doorSound.Play ();
		}

        /*Faz com que a Lauren possa interagir com o NPC, e quando interage o Canvas é ativo e sua velocidade setada pra 0, velocidade so volta ao apertar E para sair do dialogo
        if (tomNPC && Globais.AUXBOOL == false && Input.GetKeyDown("e"))
        {
            this.canMove = false;
            caixaDialogo.SetActive(true);
            Globais.AUXBOOL = true;
            SetAnimations (0);
   
        }
        else if (Globais.AUXBOOL == true && Input.GetKeyDown("e"))
        {
            Globais.AUXBOOL = false;
            caixaDialogo.SetActive(false);
            this.canMove = true;
        }
        */



        //Instancia o Celular
        pegarCelular = Input.GetKeyDown(KeyCode.Tab);

        

        //Só pode pegar o celular se a lanterna estiver desligada e não estiver escondida
        if (pegarCelular && !estadoDaLuz && can && tag == "Player")
		{
			can = false;
			StartCoroutine ("Time");
			CriaPhone ();
		}


		botaoE = Input.GetKeyDown (KeyCode.E);

	
		//Esconder
		if (canHide) 
		{
			//Só pode se esconder se a lanterna estiver desligada e o celular não estiver na tela
			if (botaoE && can && !estadoDaLuz && clonePhone) 
			{
				can = false;
				aux++;
				StartCoroutine ("Timer");
				//desaparece
				if (aux == 1) 
				{
					seEscondeu = true;
					transform.gameObject.tag = "pEscondido";
					canMove = false;
					//rb.gravityScale = 0f;
					//cap.isTrigger = true;
					SetAnimations (7);
					speed = 0f;
				}
				//aparece
				else
				{
					aux = 0;
					seEscondeu = false;
					transform.gameObject.tag = "Player";
					canMove = true;
					//rb.gravityScale = 10f;
					//cap.isTrigger = false;
					SetAnimations (8);
					speed = auxSpeed;
				}
			}
		}

	}//fim do update

	void FixedUpdate()
	{
        //comandos de movimentação
        if (canMove) 
		{
			move = Input.GetAxis ("Horizontal");		
			rb.velocity = new Vector2 (move * speed, rb.velocity.y);

			if (move > 0f || move < 0f) 
			{
				if(!estadoDaLuz)
					SetAnimations (1);

				else if(estadoDaLuz)
					SetAnimations (2);
			}
			
			if (move < 1f && move > -1f) 
			{
				if (!estadoDaLuz)
					SetAnimations (0);
				else if (estadoDaLuz)
					SetAnimations (3);
			}
					
		}
		if (estadoDaLuz && !clonePhone) 
		{
			cellphone.SetActive (false);
			//Destroy (clonePhone);
			canMove = true;
			clonePhone = true;
			destroyPhone= true;
			SetAnimations (3);
		}
		//chama a função flip caso o personagem esteja olhando para a direção errada
		if((move < 0f && facingRight) ||(move > 0f && !facingRight))
		{
			Flip ();
		}
	}

    /*
    void PuzzleRGB()
    {

        //Esses 3 IF's verificam se ela já pegou as fitas dos arcades, se sim, ela consegue interagir, senão ela nao consegue
        if (posCartuchoRed && Input.GetKeyDown("e"))
        {
            cartuchoRed.SetActive(false);
            pegouCatRed = true;
        }
        if  (posCartuchoGreen && Input.GetKeyDown("e"))
        {
            cartuchoGreen.SetActive(false);
            pegouCatGreen = true;
        }
        if (posCartuchoBlue && Input.GetKeyDown("e"))
        {
            cartuchoBlue.SetActive(false);
            pegouCatBlue = true;
        }

        //Verifica se a Lauren colidiu com o primeiro fliperama e se apertou E e troca os sprites
        
        //puzzle.sprite = red;
        if (fliperamaRed && pegouCatRed && Input.GetKeyDown ("e") && GameObject.FindGameObjectWithTag ("FlipR").GetComponent<SpriteRenderer> ().enabled == false) {
            luzRed.SetActive(true);
            GameObject.FindGameObjectWithTag ("FlipR").GetComponent<SpriteRenderer> ().enabled = true;
        } else if (fliperamaRed && pegouCatRed && Input.GetKeyDown ("e") && GameObject.FindGameObjectWithTag ("FlipR").GetComponent<SpriteRenderer> ().enabled == true) {
            luzRed.SetActive(false);
            GameObject.FindGameObjectWithTag ("FlipR").GetComponent<SpriteRenderer> ().enabled = false;
        }
        //puzzle.sprite = green
        else if (fliperamaGreen && pegouCatGreen && Input.GetKeyDown ("e") && GameObject.FindGameObjectWithTag ("FlipG").GetComponent<SpriteRenderer> ().enabled == false) {
            luzGreen.SetActive(true);
            GameObject.FindGameObjectWithTag ("FlipG").GetComponent<SpriteRenderer> ().enabled = true;
		} else if (fliperamaGreen && pegouCatGreen && Input.GetKeyDown ("e") && GameObject.FindGameObjectWithTag ("FlipG").GetComponent<SpriteRenderer> ().enabled == true) {
            luzGreen.SetActive(false);
            GameObject.FindGameObjectWithTag ("FlipG").GetComponent<SpriteRenderer> ().enabled = false;
		}
        //puzzle.sprite = blue;
        else if (fliperamaBlue && pegouCatBlue && Input.GetKeyDown ("e") && GameObject.FindGameObjectWithTag ("FlipB").GetComponent<SpriteRenderer> ().enabled == false) {
            luzBlue.SetActive(true);
            GameObject.FindGameObjectWithTag ("FlipB").GetComponent<SpriteRenderer> ().enabled = true;
		} else if (fliperamaBlue && pegouCatBlue && Input.GetKeyDown ("e") && GameObject.FindGameObjectWithTag ("FlipB").GetComponent<SpriteRenderer> ().enabled == true) {
            luzBlue.SetActive(false);
            GameObject.FindGameObjectWithTag ("FlipB").GetComponent<SpriteRenderer> ().enabled = false;
		}

        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == false)
        { 
			tv.sprite = black;
            luzTVW.SetActive(false);
            luzTVRed.SetActive(false);
            luzTVGreen.SetActive(false);
            luzTVBlue.SetActive(false);
            luzTVC.SetActive(false);
            luzTVM.SetActive(false);
            luzTVY.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == true)
        { 
            tv.sprite = white;
            luzTVW.SetActive(true);
            luzTVRed.SetActive(false);
            luzTVGreen.SetActive(false);
            luzTVBlue.SetActive(false);
            luzTVC.SetActive(false);
            luzTVM.SetActive(false);
            luzTVY.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == false)
        { 
            tv.sprite = red;
            luzTVRed.SetActive(true);
            luzTVW.SetActive(false);
            luzTVGreen.SetActive(false);
            luzTVBlue.SetActive(false);
            luzTVC.SetActive(false);
            luzTVM.SetActive(false);
            luzTVY.SetActive(false);

        }
        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == false)
        { 
            tv.sprite = yellow;
            luzTVY.SetActive(true);
            luzTVRed.SetActive(false);
            luzTVW.SetActive(false);
            luzTVGreen.SetActive(false);
            luzTVBlue.SetActive(false);
            luzTVC.SetActive(false);
            luzTVM.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == true)
        { 
            tv.sprite = magenta;
            luzTVM.SetActive(true);
            luzTVY.SetActive(false);
            luzTVRed.SetActive(false);
            luzTVW.SetActive(false);
            luzTVGreen.SetActive(false);
            luzTVBlue.SetActive(false);
            luzTVC.SetActive(false);
            
        }
        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == false)
        { 
            tv.sprite = green;
            luzTVGreen.SetActive(true);
            luzTVM.SetActive(false);
            luzTVY.SetActive(false);
            luzTVRed.SetActive(false);
            luzTVW.SetActive(false);
            luzTVBlue.SetActive(false);
            luzTVC.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == true && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == true)
        { 
            tv.sprite = cian;
            luzTVC.SetActive(true);
            luzTVGreen.SetActive(false);
            luzTVM.SetActive(false);
            luzTVY.SetActive(false);
            luzTVRed.SetActive(false);
            luzTVW.SetActive(false);
            luzTVBlue.SetActive(false);
            
        }
        if (GameObject.FindGameObjectWithTag("FlipR").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipG").GetComponent<SpriteRenderer>().enabled == false && GameObject.FindGameObjectWithTag("FlipB").GetComponent<SpriteRenderer>().enabled == true)
        { 
            tv.sprite = blue;
            luzTVBlue.SetActive(true);
            luzTVC.SetActive(false);
            luzTVGreen.SetActive(false);
            luzTVM.SetActive(false);
            luzTVY.SetActive(false);
            luzTVRed.SetActive(false);
            luzTVW.SetActive(false);
            
        }



    }*/

	void Flip()
	{
		//troca o valor do facingRight, se for V vira F e se for F vira V
		facingRight = !facingRight;

		//Faz o flip do sprite
		transform.localScale = new Vector3(-transform.localScale.x, 
			transform.localScale.y, transform.localScale.z);
	}

	public void SetAnimations(int valor)
	{
		
		anim.SetInteger ("estado", valor);
		//anim.SetBool ("Walk", rb.velocity.x != 0f);
		//anim.SetBool ("Idle", rb.velocity.x == 0f);
	}

	public void CriaPhone()
	{
		//Thread.Sleep (240);

		if (!clonePhone) 
		{
			cellphone.SetActive (false);
			//Destroy (clonePhone);
			canMove = true;
			destroyPhone= true;
			clonePhone = true;
			SetAnimations (6);
		}
			
		else 
		{
			cellphone.SetActive (true);
			//Vector3 pos = new Vector3 (phoneInstatiate.position.x, phoneInstatiate.position.y, transform.position.z);
			//clonePhone = (GameObject)Instantiate (phonePrefab, transform.position, transform.rotation);
			canMove = false;
			destroyPhone= false;
			clonePhone = false;
			SetAnimations (4);
		}
	}
		

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Esconder"))
			canHide = true;

        /* Todos os 3 IFS abaixo são de comparacao com os fliperamas, isso faz com que ela consiga desabilitar e habilitar oq ela quiser
            if (other.CompareTag("FlipRed"))
            {
                fliperamaRed = true;
                fliperamaGreen = false;
                fliperamaBlue = false;
            }
            if (other.CompareTag("FlipGreen"))
            {
                fliperamaGreen = true;
                fliperamaRed = false;
                fliperamaRed = false;
            }
            if (other.CompareTag("FlipBlue"))
            {
                fliperamaBlue = true;
                fliperamaRed = false;
                fliperamaGreen = false;
            }
        // Compara de ela esta por cima dos cartuchos
        if (other.CompareTag("CartuchoRed"))
        {
            posCartuchoRed = true;
        }
        if (other.CompareTag("CartuchoGreen"))
        {
            posCartuchoGreen = true;
        }
        if (other.CompareTag("CartuchoBlue"))
        {
            posCartuchoBlue = true;
        }*/

    }

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Esconder"))
		{
			canHide = false;
			SetAnimations (8);
		}
        // 

        /*
        if (other.CompareTag("FlipRed"))
        {
            fliperamaRed = false;
        }
        if (other.CompareTag("FlipGreen"))
        {
            fliperamaGreen = false;
        }
        if (other.CompareTag("FlipBlue"))
        {
            fliperamaBlue = false;
        }
        // Compara de ela saiu dos cartuchos
        if (other.CompareTag("CartuchoRed"))
        {
            posCartuchoRed = false;
        }
        if (other.CompareTag("CartuchoGreen"))
        {
            posCartuchoGreen = false;
        }
        if (other.CompareTag("CartuchoBlue"))
        {
            posCartuchoBlue = false;
        }*/
    }

	public void SoundWalk()
	{
        laurenWalk.time = 0.1f;
		laurenWalk.Play ();
    }

	IEnumerator Time()
	{
		yield return new WaitForSeconds (1.1f);
		can = true;
	}
	IEnumerator Timer()
	{
		yield return new WaitForSeconds (1.5f);
		can = true;
	}	
}
