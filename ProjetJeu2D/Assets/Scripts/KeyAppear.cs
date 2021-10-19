using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand le boss meurt la cage qui renfermait la clé disparait et permet au joueur de la récupérer.
// Mise en place de ce système car le joueur pouvait en étant bon récupérer la clé sans avoir tuer le Boss.

public class KeyAppear: MonoBehaviour
{
    private BossAI ba;
    private Rigidbody2D rb2d;
    private Key k;
    private AudioManager am;
    

    private void Start()
    {
        ba = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
        k = GameObject.FindGameObjectWithTag("Key").GetComponent<Key>();
        rb2d = k.GetComponent<Rigidbody2D>();
        am = FindObjectOfType<AudioManager>();
      
    }

    private void Update()
    {
        CageDisappear();
    }

    private void CageDisappear()
    {
        if(ba.living == false)
        {
            Destroy(gameObject);
            rb2d.gravityScale = 1;
           
        }
    }
}
