using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanidade : MonoBehaviour {

    public static bool perseguicao = false;

    private float saudeEstresse = 100f;
    private float tempoSubtracao = 2f;
    private float tempoAdicao = 0f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (perseguicao == false && Input.GetKeyDown(KeyCode.M))
            perseguicao = true;
        else if (perseguicao == true && Input.GetKeyDown(KeyCode.M))
            perseguicao = false;
        AumentaEstresse();
        DiminuiEstresse();
        ChecaEstadoEstresse();
        print(saudeEstresse);
        print("Estado Perseguição: " + perseguicao);
	}

    void AumentaEstresse()
    {
        if (perseguicao)
        {
            tempoSubtracao -= Time.deltaTime;
            if (tempoSubtracao <= 0)
            {
                if (saudeEstresse >= 0)
                {
                    saudeEstresse -= 20f;
                    tempoSubtracao = 2f;
                }
            }
        }
    }

    void DiminuiEstresse()
    {
        if (EstadosCelular.estadoCelular == "musica")
        {
            tempoSubtracao -= Time.deltaTime;
            if (tempoSubtracao <= 0)
            {
                if(saudeEstresse >= 0 && saudeEstresse < 100)
                    saudeEstresse += 2f;
                    tempoSubtracao = 2f;
            }
        }
    }

    void ChecaEstadoEstresse()
    {
        if (saudeEstresse >= 70)
            Player.auxSpeed = 3;
        else if (saudeEstresse < 70 && saudeEstresse >= 50)
            Player.auxSpeed = 2.7f;
        else if (saudeEstresse < 50 && saudeEstresse >= 30)
        {
            Player.auxSpeed = 2.5f;
        }
        else if (saudeEstresse < 30 && saudeEstresse >= 0)
            Player.auxSpeed = 2.0f;
    }
}
