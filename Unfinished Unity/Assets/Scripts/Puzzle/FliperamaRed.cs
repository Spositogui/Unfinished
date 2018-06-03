using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliperamaRed : MonoBehaviour {

    public static bool FliperamaR_ligado;
    public static bool limiteAtivacaoR;
    public GameObject luzR;

    private SpriteRenderer spriteRed;
    private bool colisaoPlayer;

	private AudioSource ligando;

	public GameObject lauren;

    private void Awake()
    {
        spriteRed = GetComponent<SpriteRenderer>();
		ligando = GetComponent<AudioSource> ();
    }

    void Start()
    {
        FliperamaR_ligado = false;
        colisaoPlayer = false;
        spriteRed.enabled = false;
        luzR.gameObject.SetActive(false);
        limiteAtivacaoR = false;
    }

    void Update()
    {
        if ((Dialogue1.estado == "puzzleIniciado" || Dialogue1.estado == "CianoFeito" 
            || Dialogue1.estado == "MagentaFeita") && !limiteAtivacaoR)
        {
			if (colisaoPlayer && Input.GetKeyDown(KeyCode.E) && lauren.transform.gameObject.tag == "Player")
                FliperamaR_ligado = true;

            if (FliperamaR_ligado)
            {
                spriteRed.enabled = true;
                luzR.gameObject.SetActive(true);
                limiteAtivacaoR = true;
                GerenciadorPuzzle.combinacoes++;
				ligando.Play ();
            }
            else
            {
                spriteRed.enabled = false;
                luzR.gameObject.SetActive(false);
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
