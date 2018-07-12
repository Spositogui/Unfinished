using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BateriaCelular : MonoBehaviour {

    Image bateriaSprite;

    float bateriaAtual = EstadosCelular.bateria;
    string estado = EstadosCelular.estadoCelular;

    public static float bateria;

	// Use this for initialization
	void Start () {
        bateriaSprite = GetComponent<Image>();
        bateria = bateriaAtual;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

}
