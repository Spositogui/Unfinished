using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.PostProcessing;

public class Sanidade : MonoBehaviour
{

    public static bool perseguicao = false;
    public static float saudeEstresse = 100f;

    private float tempoSubtracao = 2f;
    private float tempoAdicao = 0f;
    private float myValue = 3;
    private float changePerSec = -1;
    private bool aumentaContagem = false;

    public PostProcessingProfile ppProfile;

    void Awake()
    {
        var colorG = ppProfile.colorGrading.settings;
        colorG = ppProfile.colorGrading.settings;
        colorG.basic.saturation = 1f;
        ppProfile.colorGrading.settings = colorG;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        /*M PARA REALIZAR TESTES 
        if (perseguicao == false && Input.GetKeyDown(KeyCode.M))
            perseguicao = true;
        else if (perseguicao == true && Input.GetKeyDown(KeyCode.M))
            perseguicao = false;
        */

        AumentaEstresse();
        DiminuiEstresse();
        ChecaEstadoEstresse();
        print(saudeEstresse);
        print("Estado Perseguição: " + perseguicao);
    }

    void AumentaEstresse()
    {
        //SE INICIAR PERSEGUIÇÃO A SAUDE MENTAL DA LAUREN DIMINUI
        if (perseguicao)
        {
            aumentaContagem = false;
            tempoSubtracao -= Time.deltaTime;
            if (tempoSubtracao <= 0)
            {
                if (saudeEstresse >= 5)
                {
                    saudeEstresse -= 5f;
                    tempoSubtracao = 2f;
                }
            }
        }
    }

    void DiminuiEstresse()
    {
        //SE ABRIR O APP DE MUSICA A SAUDE MENTAL VAI AUMENTANDO
        if (EstadosCelular.estadoCelular == "musica")
        {
            aumentaContagem = true;
            tempoSubtracao -= Time.deltaTime;
            if (tempoSubtracao <= 0)
            {
                if (saudeEstresse >= 0 && saudeEstresse < 100)
                    saudeEstresse += 5f;
                tempoSubtracao = 2f;
            }
        }
    }

    void ChecaEstadoEstresse()
    {
        //Criei essas variaveis para acessar o componente ColorGrading do PostProcessing 
        var colorG = ppProfile.colorGrading.settings;
        colorG = ppProfile.colorGrading.settings;

        //Altera os valores para aumentar a saturacao ou diminuir de acordo com o estado
        if (myValue == 0 || aumentaContagem == true)
            changePerSec = 0.05f;
        if (myValue == 1 || aumentaContagem == false)
            changePerSec = -0.05f;

        if (saudeEstresse >= 70 && aumentaContagem == true)
        {
            Player.auxSpeed = 3;
            //Diminui & Aumenta a saturacao da mainCam de acordo com o numero da saude do estresse, todos os scripts 
            //abaixo fazem a mesma coisa porem com outros valores
            myValue = Mathf.Clamp(myValue + changePerSec * Time.deltaTime, 0.9f, 1f);
            colorG.basic.saturation = myValue;
            ppProfile.colorGrading.settings = colorG;
        }
        else if (saudeEstresse < 70 && saudeEstresse >= 50)
        {
            Player.auxSpeed = 2.7f;
            myValue = Mathf.Clamp(myValue + changePerSec * Time.deltaTime, 0.7f, 0.8f);
            colorG.basic.saturation = myValue;
            ppProfile.colorGrading.settings = colorG;
        }
        else if (saudeEstresse < 50 && saudeEstresse >= 30)
        {
            Player.auxSpeed = 2.5f;
            myValue = Mathf.Clamp(myValue + changePerSec * Time.deltaTime, 0.5f, 0.6f);
            colorG.basic.saturation = myValue;
            ppProfile.colorGrading.settings = colorG;
        }
        else if (saudeEstresse < 30 && saudeEstresse >= 0)
        {
            Player.auxSpeed = 2.0f;
            myValue = Mathf.Clamp(myValue + changePerSec * Time.deltaTime, 0f, 0.4f);
            colorG.basic.saturation = myValue;
            ppProfile.colorGrading.settings = colorG;
        }
    }
}
