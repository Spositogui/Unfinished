using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaBotao : MonoBehaviour
{
    public GameObject botaoMenu;
    public void AtivarBotaoMenu()
    {
        botaoMenu.gameObject.SetActive(true);
    }
}
