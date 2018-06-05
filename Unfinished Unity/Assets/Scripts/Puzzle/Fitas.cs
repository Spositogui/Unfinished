using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitas : MonoBehaviour {

    private bool colisaoPlayer;
    private AudioSource audioFita;
 
	
	void Start ()
    {
        colisaoPlayer = false;
        audioFita = GameObject.FindGameObjectWithTag("Fitas").GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (colisaoPlayer && Input.GetKeyDown(KeyCode.E) &&  Dialogue1.estado == "falas.AcharFitas")
        {
            this.gameObject.SetActive(false);
            GerenciadorPuzzle.numeroDeFitas++;
            print("Numero de Fitas: " + GerenciadorPuzzle.numeroDeFitas);
            audioFita.time = 0.5f;
            audioFita.Play();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            colisaoPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colisaoPlayer = false;
        }
    }
}
