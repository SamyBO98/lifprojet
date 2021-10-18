using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant au player d'attaque soit avec la touche A du clavier soit avec la touche Carré de la manette PS4.
// Pour éviter au player d'attaquer en boucle une variable a été ajoutée pour éviter le spam de la touche A et donc éviter de tuer tout le monde trop rapidement.
// On a donc un délai (faible pour ne pas rendre le jeu ennuyant) avant de lancer la prochaine attaque.

public class PlayerAttack : MonoBehaviour
{

    public bool attacking = false;
    private float attackTimer = 0;
    public float attackCoolDown = 0.4f;

    public Collider2D attackTrigger;
    public Animator anim;
    private MenuPause mp;
    public Player p;
    public Swim s;
    private AudioManager am;



    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    void Update()
    {
        mp = GameObject.Find("Main Camera").GetComponent<MenuPause>();
        p = GameObject.Find("Player").GetComponent<Player>();
        s = GameObject.Find("Player").GetComponent<Swim>();
        am = FindObjectOfType<AudioManager>();
        if (mp.paused == false)
        {
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("joystick button 0")) && !attacking && !p.wallCheck)
            {

                attacking = true;
                attackTimer = attackCoolDown;
                attackTrigger.enabled = true;
                anim.Play("Attack_Animation_Player");
                 am.Play("swordPlayer");

            }

            if (attacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    attackTrigger.enabled = false;
                }
            }

            anim.SetBool("Attacking", attacking);
        }
    }


}
