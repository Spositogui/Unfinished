using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBateria : MonoBehaviour {

    public Image bateriaSprite;
    private float bateriaAtual;

    public GameObject celular;
    public GameObject luzCel;

    public static string estadoCelular;
    public static float bateria;
    public static bool celularLigado;

    private bool estadoDaLuz;

    private void Awake()
    {
        estadoCelular = "desligado";
        bateria = 100f;
    }

    private void Start()
    {
        bateriaSprite.GetComponent<Image>();
        bateriaAtual = bateriaSprite.fillAmount;
    }

    // Update is called once per frame
    void Update()
    {

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

        if (estadoCelular == "ligado")
        {
            StartCoroutine(CelularLigadoBat());
        }
        else if (estadoCelular == "carregando")
        {

        }
        else if (estadoCelular == "lanterna")
        {
            StartCoroutine(LanternaLigadaBat());
        }
        else if (estadoCelular == "outrosApps")
        {

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
        else if (estadoCelular == "lanterna")
        {
            StartCoroutine(LanternaLigada());
        }
        else if (estadoCelular == "outrosApps")
        {

        }
    }

    //ATUALIZA O FLOAT DA BATERIA
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
    IEnumerator MusicaLigada()

    {
        yield return new WaitForSeconds(1);
        bateria -= 0.0048f;
    }

    //ATUALIZA O SPRITE DA BATERIA
    IEnumerator CelularLigadoBat()
    {
        bateriaSprite.fillAmount = bateriaAtual;
        do
        {
            yield return bateriaSprite.fillAmount -= 0.000016f;
        } while (estadoCelular == "ligado");
        bateriaAtual = bateriaSprite.fillAmount;
    }
    IEnumerator LanternaLigadaBat()
    {
        bateriaSprite.fillAmount = bateriaAtual;
        do
        {
            yield return bateriaSprite.fillAmount -= 0.00008f;
        } while (estadoCelular == "lanterna");
        bateriaAtual = bateriaSprite.fillAmount;
    }
    IEnumerator MusicaLigadaBat()
    {
        bateriaSprite.fillAmount = bateriaAtual;
        do
        {
            yield return bateriaSprite.fillAmount -= 0.000048f;
        } while (estadoCelular == "musica");
        bateriaAtual = bateriaSprite.fillAmount;
    }

}
