using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliperamaBlue : MonoBehaviour {

    public static bool FliperamaB_ligado;
    public static bool limiteAtivacaoB;
    public GameObject luzB;

    private SpriteRenderer spriteB;
    private bool colisaoPlayer;

	private AudioSource ligando;

	public GameObject lauren;

    private void Awake()
    {
        spriteB = GetComponent<SpriteRenderer>();
		ligando = GetComponent<AudioSource> ();
    }

    void Start()
    {
        FliperamaB_ligado = false;
        colisaoPlayer = false;
        spriteB.enabled = false;
        limiteAtivacaoB = false;
    }

    void Update()
    {

        if ((Dialogue1.estado == "puzzleIniciado" || Dialogue1.estado == "CianoFeito"
            || Dialogue1.estado == "MagentaFeita") && !limiteAtivacaoB)
        {
			if (colisaoPlayer && Input.GetKeyDown(KeyCode.E) && lauren.transform.gameObject.tag == "Player")
                FliperamaB_ligado = true;

            if (FliperamaB_ligado)
            {
                spriteB.enabled = true;
                luzB.gameObject.SetActive(true);
                limiteAtivacaoB = true;
                GerenciadorPuzzle.combinacoes++;
				ligando.Play ();
            }
            else
            {
                spriteB.enabled = false;
                luzB.gameObject.SetActive(false);
            }
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            colisaoPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            colisaoPlayer = false;
    }
}
