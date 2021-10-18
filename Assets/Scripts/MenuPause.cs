using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script permettant au joueur de mettre Pause dans son jeu en appuyant soit sur Echap du clavier ou sur le bouton Options de la manette PS4.
// Pour appuyer sur un bouton on peut utiliser la souris ou la touche R1 de la manette PS4.
// On peut naviguer avec la souris ou avec le joystick de la manette PS4.
// On arrête le temps quand on met Pause et plusieurs choix s'offrent à nous.

public class MenuPause : MonoBehaviour
{
    public GameObject PauseUI;
    Vector3 originalPos;
    public Player p;
    public GameMaster gm;

    public bool paused = false;
    private AudioManager am;

   


    void Start()
    {

        PauseUI.SetActive(false);
        p = GameObject.Find("Player").GetComponent<Player>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        am = FindObjectOfType<AudioManager>();
    }

    public void Update()
    {

        
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
           
            PauseUI.SetActive(true);
            Time.timeScale = 0;
            
            
                am.StopPlaying("bgmcalme"); // map Paradise + ocean 
                am.Play("bgmcalme");        // map Paradise + ocean 
              
                am.StopPlaying("jeu"); // map forest day +  +usine  + shop
                am.Play("jeu");       // map forest day + usine +shop
                
                am.StopPlaying("night"); // map night 
                am.Play("night");        // map night

                

        }

        if (!paused)
        {
            
                PauseUI.SetActive(false);
                Time.timeScale = 1;
                am.Play("menu");
                
                
        }
    }

    public void Resume()
    {
            am.Play("click");
            paused = false;
    }

    public void Restart()
    {
            am.Play("click");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        PlayerPrefs.DeleteKey("MaxHealth");
        PlayerPrefs.DeleteKey("Speed");
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("CurrentHealth");
        PlayerPrefs.DeleteKey("Damage"); 
        am.Play("click");
        gm.points = 0;
        SceneManager.LoadScene(0);
        
    }

}
