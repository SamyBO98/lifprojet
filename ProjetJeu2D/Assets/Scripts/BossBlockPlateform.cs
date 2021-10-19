using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Quand on rentre dans l'arène du boss un bloc tombe et nous barre la route. On ne peut plus faire machine arrièr. Le vrai combat commence donc.
// Ce script gère la chute de ce bloc.
// Quand on rentre en collision avec un collider invisible, le mécanisme se met en route.
// Si le Player meurt ou retourne en arrière au moment où le bloc tombe alors le Player aura la possibilité de grimper sur la face extérieur de ce mur pour 
// revenir et tenter sa chance auprès de ce boss

public class BossBlockPlateform : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float fallDelay;
    public SpriteRenderer sr;
    public Collider2D coll;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay / 2);
        rb2d.isKinematic = false;
        yield return new WaitForSeconds(0.8f);
        coll.isTrigger = false;
        rb2d.gravityScale = 5;
        yield return 0;
    }
}
