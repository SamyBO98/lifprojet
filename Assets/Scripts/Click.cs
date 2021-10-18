using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script permet de changer l'affichage de la barre de vie.
// Certains préfèrent des coeurs quand d'autres veulent un affichage un peu plus recherché
// On peut donc changer l'affichage en appuyant sur I du clavier ou bien sur R2 d'une manette de PS4
 
public class Click : MonoBehaviour
{
    public Canvas canvas;
    public Canvas canvas2;
    public bool appear;
    public bool appear2;

    public void Start()
    {
        canvas.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
        appear = false;
      


    }

    public void Update()
    {
        if(Input.GetKeyDown("i") || Input.GetKeyDown("joystick button 7"))
        {
            if (appear == true)
            {
                canvas.gameObject.SetActive(true);
                canvas2.gameObject.SetActive(false);
                appear = false;
            }
            else
            {
                canvas.gameObject.SetActive(false);
                canvas2.gameObject.SetActive(true);
                appear = true;
            }
        }
    }
}
