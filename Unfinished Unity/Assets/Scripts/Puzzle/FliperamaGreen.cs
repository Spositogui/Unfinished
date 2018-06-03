using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliperamaGreen : MonoBehaviour {

    public static bool FliperamaG_ligado;
    public static bool limiteAtivacaoG;
    public GameObject luzG;

    private bool colisaoPlayer;
    private SpriteRenderer spriteG;

	private AudioSource ligando;

	public GameObject lauren;

    private void Awake()
    {
        spriteG = GetComponent<SpriteRenderer>();
		ligando = GetComponent<AudioSource> ();
    }

    void Start()
    {
        FliperamaG_ligado = false;
        colisaoPlayer = false;
        luzG.gameObject.SetActive(false);
        spriteG.enabled = false;
        limiteAtivacaoG = false;
    }

    void Update()
    {
        if ((Dialogue1.estado == "puzzleIniciado" || Dialogue1.estado == "CianoFeito"
            || Dialogue1.estado == "MagentaFeita") && !limiteAtivacaoG)
        {
			if (colisaoPlayer && Input.GetKeyDown(KeyCode.E) && lauren.transform.gameObject.tag == "Player")
                FliperamaG_ligado = true;

            if (FliperamaG_ligado)
            {
                spriteG.enabled = true;
                luzG.gameObject.SetActive(true);
                limiteAtivacaoG = true;
                GerenciadorPuzzle.combinacoes++;
				ligando.Play ();
            }
            else
            {
                spriteG.enabled = false;
                luzG.gameObject.SetActive(false);
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
