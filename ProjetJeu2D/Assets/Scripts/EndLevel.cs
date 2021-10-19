using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Quand on tue un boss, une clé tombe et si on a pas ramassé la clé, le portail ne s'ouvre pas.
// On vérifie donc que la clé est en notre possession. Si oui on ouvre le passage, si non on affiche un texte demandant la clé.

public class EndLevel : MonoBehaviour
{
    public GameObject keyAsk;
    public Key k;

    private void Start()
    {
        keyAsk.SetActive(false);
        k = GameObject.FindGameObjectWithTag("Key").GetComponent<Key>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && k.haveKey == false)
        {
            keyAsk.SetActive(true);
        }
        if( k.haveKey == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && k.haveKey == false)
        {
            keyAsk.SetActive(true);
        }
        if (k.haveKey == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        keyAsk.SetActive(false);
    }
}
