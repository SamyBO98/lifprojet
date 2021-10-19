using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Si le player rentre en collision avec une zone (ici de l'eau) alors ce dernier se mettra à nager.
// Il lui est impossible d'attaquer en nageant.
// Il peut par ailleur sauter pour prendre de la hauteur et se déplacer de droite à gauche.

public class Swim : MonoBehaviour
{
    public Player player;
    public Animator anim;
    public PlayerAttack pa;
    public bool swim;



    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
        pa = GetComponent<PlayerAttack>();

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Swim"))
        {
            anim.SetBool("Swim", true);
            player.grounded = true;
            player.rb2d.gravityScale = 0.1f;
            player.jumpPower = 100;
            player.speed = 100;
            swim = true;
            pa.enabled = false;


        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Swim"))
        {
            player.grounded = true;
            pa.enabled = false;
            anim.SetBool("Swim", true);
            player.rb2d.gravityScale = 0.1f;
            player.speed = 100f;
            player.jumpPower = 100f;
            swim = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.enabled = true;
        pa.enabled = true;
        player.rb2d.gravityScale = 5.5f;
        player.speed = 250f;
        player.jumpPower = 1000f;
        player.grounded = false;
        anim.SetBool("Swim", false);
        swim = false;

    }
}
