using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeProgessoBateria : MonoBehaviour {

    public PorcentagemBateria statusBarra;
    public GameObject barraDeProgresso;
    public Text porcentagemProgresso;
    public float maxPorcentagem;
    public float porcentagemAtual;
    private float contador = 0.0f;

    // Use this for initialization
    void Start()
    {
        statusBarra = this.gameObject.GetComponent<PorcentagemBateria>();
    }

    // Update is called once per frame
    void Update()
    {
        barraDeProgresso.transform.localScale = new Vector3(statusBarra.TamanhoBarra(porcentagemAtual, maxPorcentagem), barraDeProgresso.transform.localScale.y, barraDeProgresso.transform.localScale.z); //Faz com que a porcentagem da barra vá crescendo conforme o tamanho dela
        porcentagemProgresso.text = statusBarra.PorcentagemBarra(porcentagemAtual, maxPorcentagem, 100) + "%"; // atualiza o texto da barra conforme a barra cresce

        if (porcentagemAtual < maxPorcentagem && EstadosCelular.estadoCelular == "ligado")
        {
            porcentagemAtual += Time.deltaTime * 0.0016f; // se a porcentagem for menor dq o valor max(100%) continua aumentando a barra, o 1.67 é o valor de 100/60 (para fazer q com 100% tenha se passado 60s que é o tempo do especial)
        }
        else if (porcentagemAtual < maxPorcentagem && EstadosCelular.estadoCelular == "lanterna")
        {
            porcentagemAtual += Time.deltaTime * 0.008f; // se a porcentagem for menor dq o valor max(100%) continua aumentando a barra, o 1.67 é o valor de 100/60 (para fazer q com 100% tenha se passado 60s que é o tempo do especial)
        }
        else
        {
            porcentagemProgresso.text = "100%"; // se a porcentagem for maior que o max o texto dela fica como 100%
        }
        contador += Time.deltaTime; //comeca o contador para fazer com que ele possa usar o especial e resetar a barra
        if (contador >= 600.0f && Input.GetButtonDown("Jump")) // se chegar a 60s e apertar o SPACE a porcentagem da barra é resetada e sua contagem tb
        {
            porcentagemAtual = 0f;
            contador = 0f;
        }
    }
}
