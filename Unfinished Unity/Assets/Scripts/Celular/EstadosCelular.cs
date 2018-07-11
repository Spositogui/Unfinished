using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadosCelular : MonoBehaviour {

    public GameObject celular;
    public GameObject luzCel;

    public static string estadoCelular;
    public static float bateria;
    public static bool celularLigado;
    public static bool musica;

    private bool estadoDaLuz;

    private void Awake()
    {
        estadoCelular = "desligado";
        bateria = 100f;
        musica = false;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {

        print(bateria);
        print(estadoCelular);

        estadoDaLuz = luzCel.GetComponent<LuzCelular>().luzCelularEstado;

        if (celular.activeSelf)
        {
            estadoCelular = "ligado";
            CellphoneScript.celularLigado = true;
        }
        if (celular.activeSelf == false)
        {
            estadoCelular = "desligado";
            CellphoneScript.celularLigado = false;
        }
        if (estadoDaLuz)
        {
            estadoCelular = "lanterna";
        }
        if (musica)
        {
            estadoCelular = "musica";
        }

        ChecaEstadosCelular();
	}

    public void ChecaEstadosCelular()
    {
        if (estadoCelular == "desligado")
        {
            bateria -= 0f;
        }
        else if (estadoCelular == "ligado")
        {
            StartCoroutine(CelularLigado());
        }
        else if (estadoCelular == "carregando")
        {

        }
        else if (CellphoneScript.celularLigado == false && estadoCelular == "lanterna")
        {
            StartCoroutine(LanternaLigada());
        }
        else if (estadoCelular == "musica")
        {

        }
        else if (estadoCelular == "outrosApps")
        {

        }
    }

    IEnumerator CelularLigado()
    {
        yield return new WaitForSeconds(1);
        bateria -= 0.0016f;
    }
    IEnumerator LanternaLigada()
    {
        yield return new WaitForSeconds(1);
        bateria -= 0.008f;
    }
}
