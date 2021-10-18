using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand le player rentre en collision avec le sol, l'animation Idle se déclenche.

public class GroundCheck : MonoBehaviour
{

    public Player player;
    void Start()
    {

        player = gameObject.GetComponentInParent<Player>();
    }  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.isTrigger) {
            player.grounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!col.isTrigger) {
            player.grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!col.isTrigger) {
            player.grounded = false;
        }
    }
}
