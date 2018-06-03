using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filtro : MonoBehaviour {

    public Material[] material;
    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
	}
	
	// Update is called once per frame
	void Update () {
        
        //DESABILITA SHADER QUANDO APERTA O

        if (Input.GetKeyDown("o") && rend.sharedMaterial == material[0])
            rend.sharedMaterial = material[1];
        
        else if (Input.GetKeyDown("o") && rend.sharedMaterial == material[1])
            rend.sharedMaterial = material[0];
    }
}
