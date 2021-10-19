using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère l'affichage de la vie du Boss
// Quand on rentre dans la zone on voit apparaître la vie du boss en bas et quand on le tue il disparait

public class BossCanvas : MonoBehaviour
{
    public GameObject canvasBoss;
    private BossAI ba;
    public GameObject canvas2;
    private AudioManager am;

    private void Start()
    {
        canvasBoss.SetActive(false);
        ba = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
        am = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        if(ba.living == false)
        {
            canvasBoss.SetActive(false);
            canvas2.SetActive(false);
            am.StopPlaying("BossMusic");
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasBoss.SetActive(true);
            //am.StopPlaying("jeu");
            am.Play("BossMusic");
           
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasBoss.SetActive(true);
            // am.StopPlaying("jeu");
            
            
        }
    }


 
}


