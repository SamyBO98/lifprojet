using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Si le player rentre en collision avec un objet (ici un nuage) alors il sera poussé vers la gauche afin de simuler du vent.
// De plus ces nuages disparaissent ou bout d'un certain temps.
// Il faut donc se dépêcher et sauter de nuage en nuage afin d'atteindre la terre ferme.

public class Wind : MonoBehaviour
{
    Player player;


    void Start()
    {

        player = gameObject.GetComponentInParent<Player>();
        player.grounded = true;
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            player.rb2d.AddForce(Vector2.left * 20);
            player.transform.localScale = new Vector3(-1, 1, 1);
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                player.rb2d.AddForce(Vector2.left * 20);
                player.rb2d.velocity = new Vector2(-0.5f, -10);
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            player.rb2d.AddForce(Vector2.left * 20);
            player.transform.localScale = new Vector3(-1, 1, 1);
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                player.rb2d.AddForce(Vector2.left * 20);
                player.rb2d.velocity = new Vector2(-0.5f, -10);
            }

        }
    }

}
