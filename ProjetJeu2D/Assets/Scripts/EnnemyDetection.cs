using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand l'ennemi est arrivé vers le player alors il a une zone où il commence à attaquer le joueur.
// Selon sa position dans le combat (droite ou gauche) il est capable de repousser l'ennemi avec ses attaques.
// Certains ennemis puissants peuvent le repousser loin.
// Lorsqu'il nous attaque ce dernier met au player 1 de dégat et le rend rouge puis après un petit temps reprend sa couleur normale.

public class EnnemyDetection : MonoBehaviour
{
    public EnnemyAI ea;
    private Player p;
    public bool playerHere;
    public int attackDelay;
    public bool canAttack;
    public Animator anim;
    public bool walkRight;
    public int force;
    public int force2;
    private AudioManager am;


    private void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        ea = GameObject.FindGameObjectWithTag("Ennemy").GetComponent<EnnemyAI>();
        am = FindObjectOfType<AudioManager>();
        playerHere = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHere = true;
            canAttack = true;
            if (canAttack == true)
            {
                StartCoroutine(Knockback(0.01f, 250, p.transform.position));
                
            }

        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        playerHere = false;
        canAttack = false;
    }


    public IEnumerator Knockback(float knockBackDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0;
        p.rb2d.velocity = new Vector2(p.rb2d.velocity.x, 0);
        while (knockBackDuration > timer)
        {
            timer += Time.deltaTime;
            anim.Play(ea.hit);
            yield return new WaitForSeconds(0.5f);
            if (walkRight == false)
            {
                p.rb2d.AddForce(new Vector3(knockBackDirection.x * force, knockBackDirection.y, transform.position.z));
            }
            else
            {
                p.rb2d.AddForce(new Vector3(knockBackDirection.x * force2, knockBackDirection.y, transform.position.z));
            }
            p.Damage(1);
            p.sr.material.color = Color.red;
            yield return new WaitForSeconds(p.timeColored);
            p.sr.material.color = p.original;

            am.Play("ennemyLight");
            am.Play("ennemyBlack");
            am.Play("ennemyGreen");

        }


        yield return 0;

    }



    
}







