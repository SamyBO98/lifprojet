using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant à la clé de tomber lorsque le player tue le boss.

public class FinalDoor : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Key k;
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        k = GameObject.FindGameObjectWithTag("Key").GetComponent<Key>();
         
    }

    private void Update()
    {

        Fall();
        
    }

    private void Fall()
    {
        if (k.haveKey == true)
        { 
               
            rb2d.gravityScale = 1;
            
        }
          
    }
}
