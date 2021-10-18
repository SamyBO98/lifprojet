using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Certains ennemis patrouillent le long des plateformes.
// Quand le player colle cette ennemi au niveau du dos alors cet ennemi s'arrête et est en alerte.
// Quand on quitte cette zone ce patrouilleur continue de patrouiller.

public class EnnemyTurn : MonoBehaviour
{
    public Patrol pat;
    public Animator anim;
    public string back;
    public string walk;

    private void Start()
    {
        pat = gameObject.GetComponentInParent<Patrol>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pat.anim.Play(back);
     
            pat.speed = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pat.anim.Play(back);
        
            pat.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pat.anim.Play(walk);
        pat.speed = pat.dfspeed;
    }
}
