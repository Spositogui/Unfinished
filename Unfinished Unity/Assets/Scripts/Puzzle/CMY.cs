using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//esse script serve para habilitar e desabilitar a camera que foca na imagem CMY do puzzle de cores
public class CMY : MonoBehaviour {

    public GameObject cameraMain;
    public GameObject cameraCMY;

    private bool colisaoPlayer;
	
	void Start ()
    {
        colisaoPlayer = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((colisaoPlayer && !cameraCMY.gameObject.activeInHierarchy) && Input.GetKeyDown(KeyCode.E))
        {
            cameraCMY.gameObject.SetActive(true);
            cameraMain.gameObject.SetActive(false);
            Player.canMove = false;
        }

        if (cameraCMY.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            cameraCMY.gameObject.SetActive(false);
            cameraMain.gameObject.SetActive(true);
            Player.canMove = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            colisaoPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colisaoPlayer = false;
        }
    }
}
