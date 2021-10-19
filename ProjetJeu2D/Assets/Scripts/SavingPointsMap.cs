using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Si le joueur est sur une case pour sauvegarder alors il peut faire Pause puis Save. Sinon le bouton est grisée.
// Cela évite au player de save n'importe où (en sautant dans un trou ou sur des piques).

public class SavingPointsMap : MonoBehaviour
{
 
    public GameObject saveControl;
    public MenuPause mp;
    public Button save;



    private void Start()
    {
        saveControl.SetActive(false);
    }

    private void Update()
    {
        if(mp.paused == true)
        {
            saveControl.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            saveControl.SetActive(true);
            save.interactable = true;
 
           
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            save.interactable = true;
            saveControl.SetActive(true);
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        saveControl.SetActive(false);
        save.interactable = false;
    }
}
