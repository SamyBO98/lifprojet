using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script controlant les dégats qu'inflige notre joueur et à qui il les inflige

public class AttackTrigger : MonoBehaviour
{

    public int dmg;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger != true && collision.CompareTag("turret"))
        {
            collision.SendMessageUpwards("Damage", dmg);
            
        }

        if(collision.isTrigger != true && collision.CompareTag("TrashMob"))
        {
            collision.SendMessageUpwards("Damage", dmg);
            

        }

        if(collision.isTrigger != true && collision.CompareTag("Ennemy"))
        {
            collision.SendMessageUpwards("Damage", dmg);
        }

        if(collision.isTrigger != true && collision.CompareTag("Boss"))
        {
            collision.SendMessageUpwards("Damage", dmg);
        }

    } 

  
}
