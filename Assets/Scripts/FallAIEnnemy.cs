using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand un ennemi s'approche des piques, il est comme attiré par ces derniers et tombe dedans rapidement.

public class FallAIEnnemy : MonoBehaviour
{
    private EnnemyAI ea;
    private Rigidbody2D rb2d;

    private void Start()
    {
        ea = GameObject.FindGameObjectWithTag("Ennemy").GetComponent<EnnemyAI>();
        rb2d = ea.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            rb2d.gravityScale = 50;
        }
    }
}
