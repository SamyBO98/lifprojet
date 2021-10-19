using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script s'occupant du Menu du Début (fonctions pour les boutons New Game et Quitte).
// On peut naviguer avec la souris ou avec le joystick de la manette PS4.
// Pour appuyer sur un bouton on peut utiliser la souris ou la touche R1 de la manette PS4.

public class MainMenu : MonoBehaviour
{   
    private AudioManager am;
 

    void Start(){
        am = FindObjectOfType<AudioManager>();
    }

    public void PlayGame()
    {  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.DeleteAll();
        SaveSystem.Delete();
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame(){
        Application.Quit();
    }
}
