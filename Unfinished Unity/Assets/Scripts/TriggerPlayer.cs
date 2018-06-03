using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//essse script serve inicialmente para arrumar problemas de escoderijo do player
public class TriggerPlayer : MonoBehaviour 
{

	private Collider2D colisorP;

	void Start () 
	{
		colisorP = GetComponent<Collider2D> ();	
	}

	void Update () 
	{
		if(Player.seEscondeu)
			transform.gameObject.tag = "Player";
		else
			transform.gameObject.tag = "TriggerPlayer";
	}
}
