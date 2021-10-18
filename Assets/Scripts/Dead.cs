using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script permet au Player de mourir s'il tombe dans le vide.
// S'il rentre en collision avec un collider en bas de la map alors ce collider lui infligera des dégats équivalent à sa vie courante

public class Dead : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            
            player.Damage(player.currentHealth);

        }
    }






}
