using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {


	public Transform wallCheck;
	public Transform playerCheck;
	public Transform cCheck;
	public Transform pCheck;
	private SpriteRenderer sprite;
	private Animator anim;
	private Rigidbody2D rb;
	private GameObject target;
	private GameObject chaseSound;
	public GameObject objectSomFundo;
	private GameObject deathSound;

	public bool facingRight = true;
	public bool tochedwall = false;
	public static bool detected = false;
	public bool behind = false;
	private bool c5;
	private bool c4;
	private bool c3;
	private bool c2;
	private bool c1;
	private bool c2l;
	private bool c3l;
	private bool c4l;
	private bool c5l;
	private bool c6;
	public float speed;
	public bool playerMorreu = false;
	private bool canMove;
	private Player lauren;
	private Transform wall;

	private AudioSource audioDeFundo;
	private AudioSource [] sons;

	[SerializeField]

	private GameObject gameOver;

	void Start () 
	{
		sons = GetComponents<AudioSource> ();
		sprite = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag ("Player");
		chaseSound = GameObject.FindWithTag ("AudioChase");
        deathSound = GameObject.FindWithTag("DeathSound");
        deathSound.SetActive(false);
        audioDeFundo = objectSomFundo.GetComponent<AudioSource> ();
		gameOver.SetActive (false);
		canMove = true;
		anim = GetComponent<Animator> ();
		wall = GameObject.Find ("Wall").GetComponent<Transform> ();
		lauren = GameObject.Find("Player").GetComponent<Player> ();
		sons [2].volume = 0f;
	}


	void Update () 
	{

		/*wall recebe uma linha que vai da posição do personagem atéa posição do wallCheck e verifica se tem
		contato com a Layer chamada Wall*/
		tochedwall = Physics2D.Linecast (transform.position, wallCheck.position, 1 << LayerMask.NameToLayer ("Wall"));
		detected = Physics2D.Linecast (transform.position, playerCheck.position, 1 << LayerMask.NameToLayer ("Player"));
		behind = Physics2D.Linecast (transform.position, pCheck.position, 1 << LayerMask.NameToLayer ("Player"));
		c5 = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc5r"));
		c4 = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc4r"));
		c3 = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc3r"));
		c2 = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc2r"));
		c1 = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc1"));
		c2l = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc2l"));
		c3l = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc3l"));
		c4l = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc4l"));
		c5l = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc5l"));
		c6 = Physics2D.Linecast (transform.position, cCheck.position, 1 << LayerMask.NameToLayer ("Sc6"));


		if (playerMorreu == true)
		{
			gameOver.SetActive (true);
            target.SetActive(false);
            chaseSound.GetComponent<AudioSource>().volume = 0f;
            deathSound.SetActive(true);
        }

		if (tochedwall)
			Flip ();

		if (!detected) {
			sons [2].Play ();
		}
		if (detected && lauren.tag == "Player") {
			tochedwall = false;
			wall.position = new Vector3 (-52f, 0f, 0f);
			chaseSound.GetComponent<AudioSource> ().volume = 0.5f;
			audioDeFundo.volume = 0f;
			sons [2].volume = 0.2f;
            transform.gameObject.tag = "Follow";
		}
		else {
			chaseSound.GetComponent<AudioSource> ().volume = 0f;
			audioDeFundo.volume = 0.3f;
            transform.gameObject.tag = "Enemy";
		}
		if (behind) {
			chaseSound.GetComponent<AudioSource> ().volume = 0.5f;
			audioDeFundo.volume = 0f;
		} 
		if (behind && lauren.tag == "Player") {
			Flip ();
		}


		if(c5)
			this.transform.position = new Vector3 (57f, -1.5f, 0f);
		if(c4)
			this.transform.position = new Vector3 (30f, -2f, 0f);
		if(c3)
			this.transform.position = new Vector3 (7f, -1f, 0f);
		if(c2)
			this.transform.position = new Vector3 (-20.77f, -1f, 0f);
		if(c1)
			this.transform.position = new Vector3 (-48.88f, -1f, 0f);
		if(c2l)
			this.transform.position = new Vector3 (-29.2f, -1f, 0f);
		if(c3l)
			this.transform.position = new Vector3 (-7f, -1f, 0f);
		if(c4l)
			this.transform.position = new Vector3 (20f, -2f, 0f);
		if(c5l)
			this.transform.position = new Vector3 (43f, -1.5f, 0f);
		if(c6)
			this.transform.position = new Vector3 (67.5f, -1f, 0f);
		
    }

	void FixedUpdate(){

		if (canMove) 
		{
			//comandos de movimentação automatica
			rb.velocity = new Vector2 (speed, rb.velocity.y);
		}
	}
	void Flip(){
		//troca o valor do facingRight, se for V vira F e se for F vira V
		facingRight = !facingRight;
		//Faz o flip do sprite
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		//Muda o valor da velocida para V ou F
		speed *= -1;
	}

	public void GameOver()
	{
		SceneManager.LoadScene ("Menu");
	}
    public void SavePoint()
    {
        SceneManager.LoadScene("Demo01");
    }
	public void SetAnimations(int valor)
	{

		anim.SetInteger ("estado", valor);
	}

	void OnTriggerEnter2D(Collider2D collider){
		
		if (collider.gameObject.CompareTag ("TriggerPlayer")) {
			canMove = false;
			playerMorreu = true;
			anim.Play ("GameOver");
			speed = 0f;
			SetAnimations (1);
		}

	}

	public void SoundStomp()
	{
		if (detected || behind)
			sons [1].Play ();
		else
			sons [1].Stop ();
	}
}