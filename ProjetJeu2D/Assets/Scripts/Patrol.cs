using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant de controler l'ennemi patrouilleur.
// Ce dernier tire un rayon en bas et lorsque se rayon ne rencontre plus rien l'ennemi va marcher dans la direction opposée.
// Quand il est tué il rapporte 20 golds et beaucoup de vie.

public class Patrol : MonoBehaviour
{
    public float speed;
    public float dfspeed;
    public float distance;
    public bool movingLeft;
    public int curHealth;
    public int maxHealth;
    public Transform groundDetection;
    public Animator anim;
    public Player player;
    public GameMaster gm;
    public string hurt;

    private void Start()
    {
        dfspeed = speed;
        curHealth = maxHealth;
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
        Die();

   }    

    public void Damage(int dmg)
    {
        curHealth -= dmg;
        anim.Play(hurt);
    }

    public void Die()
    {
        if(curHealth<= 0)
        {
            player.sr.material.color = player.original;
            Destroy(gameObject);
            gm.points += 20;
            if (player.currentHealth < player.maxHealth)
            {
                player.currentHealth = player.currentHealth + 2;
            }
        }
    }

   




} 
