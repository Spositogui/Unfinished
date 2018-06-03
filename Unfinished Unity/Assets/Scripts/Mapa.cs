using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mapa : MonoBehaviour 
{
	private bool retornarJogo;
	private bool arrowRight;
	private bool arrowLeft;
	private bool arrowUp;
	private bool arrowDown;

	private float posY;
	private float posX;



	void Start () 
	{
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		retornarJogo = Input.GetKeyDown (KeyCode.Escape);
		arrowRight = Input.GetKey(KeyCode.RightArrow);
		arrowLeft = Input.GetKey (KeyCode.LeftArrow);
		arrowUp = Input.GetKey (KeyCode.UpArrow);
		arrowDown = Input.GetKeyDown (KeyCode.DownArrow);

		posX= GetComponent<Transform> ().position.x;
		posY = GetComponent<Transform> ().position.y;
		 

		if (retornarJogo) 
		{
			
			SceneManager.LoadScene ("Demo01");
		}

		if (arrowRight && posX < 4.7f)
			transform.position = new Vector3 (transform.position.x + 0.2f, transform.position.y
				,transform.position.z);
		if(arrowLeft && posX > -4.8f)
			transform.position = new Vector3 (transform.position.x - 0.2f, transform.position.y
				,transform.position.z);

	}
}
