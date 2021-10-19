using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Certaines plateformes ne sont pas très solides et tombent après un certains temps où le joueur reste dessus.
// Après un petit temps, la plateforme se grise et commence à tomber 
// Utilisation dans les nuages de la map Paradise.

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float fallDelay;
    public SpriteRenderer sr;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay/2);
        sr.material.color = Color.grey;
        yield return new WaitForSeconds(fallDelay/2);
        rb2d.isKinematic = false;
        GetComponent<Collider2D>().isTrigger = true;
        rb2d.gravityScale = 5;
        yield return 0;
    }
}
