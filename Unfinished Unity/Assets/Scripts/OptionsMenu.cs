using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEditor;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject videoSettings;
    [SerializeField]
    private GameObject audioSettings;
    public static bool aux = false;

    public AudioMixerSnapshot paused;// recebe o snapshot
    public AudioMixerSnapshot unpaused;//recebe o snapshot
    public AudioMixer masterMixer;
    public AudioMixer effectsMixer;
    private float slidercount = 0f;
    private Slider slider;
    private Light light;


    void Start()
    {
        optionsMenu.SetActive(false);
        videoSettings.SetActive(false);
        audioSettings.SetActive(false);
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Options();

    }

    void Options()
    {
        //verifica se o ESC foi apertado e mostra o Canvas de OptionsMenu e Pausa o jogo
        if (aux == false && Input.GetKeyDown(KeyCode.Escape))
        {
            aux = true;
            optionsMenu.SetActive(true);
            Pause();
        }
        //verifica se o ESC foi apertado e remove o Canvas de OptionsMenu e despausa o jogo
        else if (aux == true && Input.GetKeyDown(KeyCode.Escape))
        {
            aux = false;
            optionsMenu.SetActive(false);
            Pause();
        }


    }

    public void VideoConfig()
    {
        videoSettings.SetActive(true);
        audioSettings.SetActive(false);
    }

    public void AudioConfig()
    {
        audioSettings.SetActive(true);
        videoSettings.SetActive(false);
    }

    public void GoMenu()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        SceneManager.LoadScene("Menu");
    }

    //pausa o jogo
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        TrasicaoAudio();
    }
    //responsavel por fazer a troca do audio em determinado tempo
    public void TrasicaoAudio()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(.01f);
        }
        else
        {
            unpaused.TransitionTo(.01f);
        }
    }

    // Pega o Slider do Canvas VideoSettings e atribui o valor dele para a intensidade da luz na camera
    public void BrightnessControll(Slider slider)
    {
        if (aux == true)
            light.intensity = slider.value;

    }

    //Serve para acessar o componente do audio Mixer(volume), e trocar conforme o slider
    public void SetMasterVol(float masterVol)
    {
        masterMixer.SetFloat("masterVol", masterVol);
    }

    public void SetEffectsVol(float effectVol)
    {
        effectsMixer.SetFloat("effectsVol", effectVol);
    }
}
