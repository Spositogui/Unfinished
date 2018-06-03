using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Button startText;
    public Button exitText;
    // Use this for initialization
    void Start()
    {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

    }

    public void ExitPress()
    {
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void StartLevel()
    {
        //Aqui colocar o nome do level que deve carregar ao clicar no jogo
        SceneManager.LoadScene("Demo01");
    }

    public void ExitGame()
    {
        Application.Quit();

    }
    // Update is called once per frame
    void Update()
    {

    }
}
