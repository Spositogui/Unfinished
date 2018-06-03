using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue1 : MonoBehaviour
{

    public Text textoAtual;
    public GameObject fantasma_Tom;
    public GameObject painel;
    public static string estado;
	public GameObject lauren;

    private float timeFalas;
    private int posicaoFala;
    private bool colisaoPlayer;


    //falas
    private string[] textos =
        {   "- O... Olá, não se assuste. Eu sou Tom, o Fantasma Camarada! Posso te ajudar a sair daqui",
            "Você precisa encontrar fitas que estão espalhadas pela mansão e usá-las naqueles fliperamas bem ali >>> ",
            "Ao todo são três fitas, estas são coloridas respectivamente com as cores VERMELHO, VERDE e AZUL. Vai lá, eu sei que você consegue!",
            "Só tome cuidado com ELE ...",
            "Muito bem, você conseguiu encontrá-las! Agora você precisa acertar a combinação.",
            "Você precisa colocar as fitas nos Fliperamas de forma com que a cor que vai aparecer na Tv, ali em cima, fique na sequência do Padrão CMY. Boa Sorte!",
            "Muito bem, você já acertou uma cobinação! Faltam só mais duas.",
            "Acertou outra, agora só falta uma combinação. Se você conseguir eu e meus amigos poderemos te ajudar.",
            "Muito bem, você é incrivel. Ai vem eles !",
            "Muito obrigado por nos ajudar, agora vamos retribuir o favor !"


        };


    void Start ()
    {
        estado = "";
        timeFalas = 0;
        posicaoFala = 0;
        fantasma_Tom.gameObject.SetActive(false);
        painel.gameObject.SetActive(false);
        colisaoPlayer = false;

    }


    void Update()
    {
		if (estado == "apresentacao")//inicia a conversa quando player entra na sala e muda o estado
        {
            Apresentacao();
            timeFalas += Time.deltaTime;//Mathf.RoundToInt converte  para int
        }

        if (colisaoPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            if (estado == "falas.AcharFitas" )//altera a fala apos apresentacao
            {
                posicaoFala++;
                Player.canMove = false;
				lauren.transform.gameObject.tag = "Player";

                if (posicaoFala <= 3)
                    FalasIniciais();
                else
                {
                    painel.gameObject.SetActive(false);
                    posicaoFala = 0;
                    Player.canMove = true;
                }
            }

            if (estado == "pegouFitas" || estado == "puzzleIniciado")
            {
                Player.canMove = false;
                AchouFitas(); 
            }
        }

        //Fala quando acerta primeira combinação
        if (estado == "CianoFeito")
        {
            textoAtual.text = textos[6];
            painel.gameObject.SetActive(true);
            Player.canMove = false;
            posicaoFala = 5;
            estado = "puzzleIniciado";
            Invoke("AchouFitas", 4f);
			lauren.transform.gameObject.tag = "pEscondido";
        }

        //Fala quando acerta segunda combinação
        if (estado == "MagentaFeita")
        {
            textoAtual.text = textos[7];
            painel.gameObject.SetActive(true);
            Player.canMove = false;
            posicaoFala = 5;
            estado = "puzzleIniciado";
            Invoke("AchouFitas", 4f);
			lauren.transform.gameObject.tag = "pEscondido";
        }

        //fim do puzzle
        if (estado == "puzzleFinalizado")
        {
            textoAtual.text = textos[8];
            painel.gameObject.SetActive(true);
            Player.canMove = false;
            posicaoFala = 9;
            estado = "fimPuzzle";
            Invoke("FimPuzzle", 2f);
        }

		if (estado == "fimPuzzle" && colisaoPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            textoAtual.text = textos[9];
            painel.gameObject.SetActive(true);
            Player.canMove = false;
            Invoke("FimPuzzle", 2f);
        }
    }

    //primeira coisa que o Fantasma do Tom fala apos aparecer---------
    private void Apresentacao()
    {
        if (timeFalas > 2 && timeFalas < 4)
        {
            fantasma_Tom.gameObject.SetActive(true);
        }
        else if (timeFalas > 4 && timeFalas < 5)
        {
            //ativa texto
            painel.gameObject.SetActive(true);
            textoAtual.text = textos[0];
            estado = "falas.AcharFitas";
        }
    }
    
    //troca o texto inicial--------
    private void FalasIniciais()
    {
        //loop até achar as fita
        if (posicaoFala == 1 && !painel.gameObject.activeInHierarchy)
        {
            painel.gameObject.SetActive(true);
        }

        textoAtual.text = textos[posicaoFala];
    }

    //                      ----------------
    private void AchouFitas()
    {
        if (posicaoFala == 5)
        {
            estado = "puzzleIniciado";
            posicaoFala--;
            painel.gameObject.SetActive(false);
            Player.canMove = true;
        }
        else if (posicaoFala == 4)
        {
            posicaoFala++;
            textoAtual.text = textos[posicaoFala];
            painel.gameObject.SetActive(true);
        }
        else if (posicaoFala < 4)
        { 
            posicaoFala = 4;
            textoAtual.text = textos[posicaoFala];
            painel.gameObject.SetActive(true);
        }
    }

    private void FimPuzzle()
    {
        painel.gameObject.SetActive(false);
        //Player.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Player") || collision.CompareTag("pEscondido"))
        {
            //alguma coisa
            colisaoPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.CompareTag("Player") || collision.CompareTag("pEscondido"))
        {
            //alguma coisa
            colisaoPlayer = false;
        }
    }
}
