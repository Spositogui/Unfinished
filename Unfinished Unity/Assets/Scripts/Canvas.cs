using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour {

    public GameObject cameraMain;
    public GameObject cameraCMY;
    
    // Update is called once per frame
    void Update()
    {
        if ((OptionsMenu.aux && !cameraCMY.gameObject.activeInHierarchy) && Input.GetKeyDown(KeyCode.E))
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
}
