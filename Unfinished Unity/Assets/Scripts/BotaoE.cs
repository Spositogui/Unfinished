using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoE : MonoBehaviour 
{
	private SpriteRenderer spriteBotao;
	private bool temColisao;

	void Awake () 
	{

		spriteBotao = GetComponent<SpriteRenderer> ();
		//o botao fica invisivel
		spriteBotao.enabled = false;

		temColisao = false;
	}
	

	void Update () 
	{
		if (temColisao)
			spriteBotao.enabled = true;
		else if (!temColisao)
			spriteBotao.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D personagem)
	{
		if (personagem.CompareTag ("Player") || personagem.CompareTag("pEscondido"))
			temColisao = true;
	}
	void OnTriggerExit2D(Collider2D personagem)
	{
		if (personagem.CompareTag ("Player") || personagem.CompareTag("pEscondido"))
			temColisao = false;
	}
}
