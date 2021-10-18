using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Ce script gère les dégats que fais une balle (balle envoyée pour le boss ou par la tourelle)
// Cette balle fait donc 1 de dégat et rend notre Player rouge avant de revenir à sa couleur normale

public class Bullet : MonoBehaviour
{
  
    Player player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(collision.isTrigger != true & collision.tag != "turret")
        {
            if (collision.CompareTag("Player"))
            {
                player.Damage(1);
                StartCoroutine(Couleur());

            }
        }
       
    } 


    IEnumerator Couleur() {
        player.sr.material.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        player.sr.material.color = player.original;
        Destroy(gameObject);

    }


    






}








       



