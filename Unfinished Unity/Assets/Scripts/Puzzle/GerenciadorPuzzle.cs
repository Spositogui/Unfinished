using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorPuzzle : MonoBehaviour {

    public static int numeroDeFitas;
    public static int combinacoes;
    public GameObject luz;
    //Fantasmas
    public GameObject burgh;
    public GameObject gu;
    public GameObject guih;

    //fitas
    public GameObject FitaR;
    public GameObject FitaB;
    public GameObject FitaG;

    //luzes
    public GameObject luzBlue;
    public GameObject luzRed;
    public GameObject luzGreen;
    public GameObject luzCiano;
    public GameObject luzMagenta;
    public GameObject luzYellow;
	public GameObject lauren;

    private Animator tvAnimation;
    public static string coresFeitas;

    private Camera cameraScript;

    private void Awake()
    {
        tvAnimation = GetComponent<Animator>();
        cameraScript = GameObject.Find("Main Camera").GetComponent<Camera>();
        
        //set luz
        luzBlue.gameObject.SetActive(false);
        luzRed.gameObject.SetActive(false);
        luzGreen.gameObject.SetActive(false);
        luzCiano.gameObject.SetActive(false);
        luzMagenta.gameObject.SetActive(false);
        luzYellow.gameObject.SetActive(false);
        burgh.SetActive(false);
        gu.SetActive(false);
        guih.SetActive(false);
        combinacoes = 0;
    }

    void Start ()
    {
        numeroDeFitas = 0;
        FitaR.gameObject.SetActive(false);
        FitaB.gameObject.SetActive(false);
        FitaG.gameObject.SetActive(false);
        coresFeitas = "null";
    }
	

	void Update ()
    {
        if (numeroDeFitas == 3)
        {
            Dialogue1.estado = "pegouFitas";
            numeroDeFitas++;
        }
      
        if (Dialogue1.estado == "falas.AcharFitas" && numeroDeFitas == 0) //ativar objs fitas
        { 
            FitaR.gameObject.SetActive(true);
            FitaB.gameObject.SetActive(true);
            FitaG.gameObject.SetActive(true);
        }

        if (combinacoes == 2)
        {
            if (Dialogue1.estado == "puzzleIniciado" && coresFeitas == "null")
            {
                if (Ciano())
                {
                    Dialogue1.estado = coresFeitas = "CianoFeito";
                    Invoke("ResetPuzzle", 0.7f);
					StartCoroutine ("Reset");
                    cameraScript.ShakeCamera(0.8f, 0.05f);
                }
                else
                {
                    Invoke("ResetPuzzle", 0.7f);
                }
            }
            else if (coresFeitas == "CianoFeito")
            {
                if (Magenta())
                {
                    Dialogue1.estado = coresFeitas = "MagentaFeita";
                    Invoke("ResetPuzzle", 0.7f);
					StartCoroutine ("Reset");
                    cameraScript.ShakeCamera(0.8f, 0.05f);
                }
                else
                {
                    Invoke("ResetPuzzle", 0.7f);
                }
            }
            else if (coresFeitas == "MagentaFeita")
            {
                if (Yellow())
                {
                    Invoke("ResetPuzzle", 0.7f);
                    Invoke("InvocaPersonagens", 4f);
                    Invoke("PuzzleFinalizado", 5f);
                    Invoke("EndDemo", 8f);
                    lauren.transform.gameObject.tag = "pEscondido";
                    StartCoroutine ("Reset");
                    cameraScript.ShakeCamera(3f, 0.06f);

                }
                else
                {
                    Invoke("ResetPuzzle", 0.7f);
                }
            }
        }
        SetAnimations();

    }

    private bool Ciano()
    {
        if (FliperamaBlue.FliperamaB_ligado && FliperamaGreen.FliperamaG_ligado)
            return true;
        else
            return false;
    }

    private bool Magenta()
    {
        if (FliperamaRed.FliperamaR_ligado && FliperamaBlue.FliperamaB_ligado)
            return true;
        else
            return false;
    }

    private bool Yellow()
    {
        if (FliperamaRed.FliperamaR_ligado && FliperamaGreen.FliperamaG_ligado)
            return true;
        else
            return false;
    }

    void ResetPuzzle()
    {
        //set luz
        luzBlue.gameObject.SetActive(false);
        luzRed.gameObject.SetActive(false);
        luzGreen.gameObject.SetActive(false);
        luzCiano.gameObject.SetActive(false);
        luzMagenta.gameObject.SetActive(false);
        luzYellow.gameObject.SetActive(false);

        tvAnimation.SetInteger("Cores", 7);
        FliperamaRed.FliperamaR_ligado = false;
        FliperamaRed.limiteAtivacaoR = false;

        FliperamaGreen.FliperamaG_ligado = false;
        FliperamaGreen.limiteAtivacaoG = false;


        FliperamaBlue.FliperamaB_ligado = false;
        FliperamaBlue.limiteAtivacaoB = false;


        combinacoes = 0;
    }

    void InvocaPersonagens()
    {
        FliperamaRed.FliperamaR_ligado = true;
        FliperamaGreen.FliperamaG_ligado = true;
        FliperamaBlue.FliperamaB_ligado = true;
    }

    void PuzzleFinalizado()
    {
        Dialogue1.estado = coresFeitas = "puzzleFinalizado";
        burgh.SetActive(true);
        gu.SetActive(true);
        guih.SetActive(true);
    }

    void SetAnimations()
    {
        if (FliperamaRed.FliperamaR_ligado)
        { 
            tvAnimation.SetInteger("Cores", 1);
            luzRed.gameObject.SetActive(true);
        }

        if (FliperamaGreen.FliperamaG_ligado)
        {
            tvAnimation.SetInteger("Cores", 2);
            luzGreen.gameObject.SetActive(true);
        }

        if (FliperamaBlue.FliperamaB_ligado)
        {
            tvAnimation.SetInteger("Cores", 3);
            luzBlue.gameObject.SetActive(true);
        }

        if (FliperamaGreen.FliperamaG_ligado && FliperamaBlue.FliperamaB_ligado)
        {
            tvAnimation.SetInteger("Cores", 5);
            luzGreen.gameObject.SetActive(false);
            luzBlue.gameObject.SetActive(false);
            luzCiano.gameObject.SetActive(true);
        }

        if (FliperamaRed.FliperamaR_ligado && FliperamaBlue.FliperamaB_ligado)
        {
            tvAnimation.SetInteger("Cores", 4);
            luzBlue.gameObject.SetActive(false);
            luzRed.gameObject.SetActive(false);
            luzMagenta.gameObject.SetActive(true);
        }
        if (FliperamaGreen.FliperamaG_ligado && FliperamaRed.FliperamaR_ligado)
        {
            tvAnimation.SetInteger("Cores", 6);
            luzRed.gameObject.SetActive(false);
            luzGreen.gameObject.SetActive(false);
            luzYellow.gameObject.SetActive(true);
        }
        if (FliperamaRed.FliperamaR_ligado && FliperamaGreen.FliperamaG_ligado && FliperamaBlue.FliperamaB_ligado)
        {
            tvAnimation.SetInteger("Cores", 8);
            luzRed.gameObject.SetActive(false);
            luzGreen.gameObject.SetActive(false);
            luzBlue.gameObject.SetActive(false);
        }
    }

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (4f);
		lauren.transform.gameObject.tag = "Player";
	}

    void EndDemo()
    {
        SceneManager.LoadScene("DemoEnd");
    }
}
