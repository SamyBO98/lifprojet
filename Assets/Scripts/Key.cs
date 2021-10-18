using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand on rentre en collision avec la clé cette dernière disparaît comme si elle allait dans notre inventaire.

public class Key : MonoBehaviour
{
    public bool haveKey;
     private AudioManager am;

    private void Start()
    {   am = FindObjectOfType<AudioManager>();
        haveKey = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            haveKey = true;
            am.Play("wallFall");
        }
    }
}
