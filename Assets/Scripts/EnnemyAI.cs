using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant de contrôler un ennemi
// À une certaine distance l'ennemi commence à pourchasser le joueur. Si cette distance est trop grande alors il s'arrête.
// S'il rentre en collision avec une tourelle ou avec un autre ennemi, il est capable de sauter par dessus pour l'éviter.
// L'ennemi peut prendre des dégats et est repoussé vers la gauche ou la droite selon le sens du combat. (variable kncback et knckback2).
// Certains ennemis lourds peuvent ne pas être repoussé ce qui les rend encore plus redoutable.
// Quand il est tué il nous rapport 20 golds et un peu de vie.

public class EnnemyAI : MonoBehaviour
{
    public Transform player;
    public float rangeFollow;
    public float movespeed;
    public Rigidbody2D rb2d;
    public float distance;
    public Animator anim;
    public string walk;
    public string idle;
    private bool hasJump;
    public EnnemyDetection ed;
    public int currHealth;
    public int maxHealth;
    public string hit;
    public GameMaster gm;
    public ParticleSystem bloodEffect;
    private SpriteRenderer sr;
    private Color original;
    public int knckback;
    public int knckback2;



    public Player p;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim.Play(idle);
        movespeed = 3;
        hasJump = false;
        rangeFollow = 6;
        ed = gameObject.GetComponentInChildren<EnnemyDetection>();
        currHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        original = sr.color;

        


    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);

        if(distance < rangeFollow && ed.playerHere == false)
        {
            FollowPlayer();
            anim.Play(walk);
            anim.SetBool("follow", true);
        }
        else
        {
            StopFollowPlayer();
            anim.SetBool("follow", false);

        }


        Die();

    }

   private void FollowPlayer()
    {
        
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(movespeed, 0);
            transform.localScale = new Vector2(1, 1);
            ed.walkRight = true;
        }
        else 
        {
            rb2d.velocity = new Vector2(-movespeed, 0);
            transform.localScale = new Vector2(-1, 1);
            ed.walkRight = false;
        }
    }

    private void StopFollowPlayer()
    {
        
        rb2d.velocity = Vector2.zero;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("turret") && hasJump == false)
        {
            rb2d.AddForce(Vector2.up * 600);
           
        }
        if(collision.collider.CompareTag("TrashMob") && hasJump == false)
        {
            rb2d.AddForce(Vector2.up * 600);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("turret") && hasJump == false)
        {
            rb2d.AddForce(Vector2.up * 600);
            
        }

        if (collision.collider.CompareTag("TrashMob") && hasJump == false)
        {
            rb2d.AddForce(Vector2.up * 600);
        }

 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StartCoroutine(DelayGravityTurret());
    }

    private IEnumerator DelayGravityTurret()
    {
        yield return new WaitForSeconds(1);
        rb2d.gravityScale = 20;
        yield return new WaitForSeconds(0.5f);
        rb2d.gravityScale = 0.5f;
        yield return 0;
    }

    public void Damage(int dmg)
    {

        currHealth -= dmg;
        BloodEffect();
        KnockbackEnnemy(0.01f, transform.position);
    }

    public void Die()
    {
        if (currHealth <= 0)
        {
            p.sr.material.color = p.original;
            Destroy(gameObject);
            gm.points += 20;
            if (p.currentHealth < p.maxHealth)
            {
                p.currentHealth = p.currentHealth + 1;
            }
        }

    }

    private void BloodEffect()
    {
        bloodEffect.Play();
    }

    public void KnockbackEnnemy(float knockBackDuration, Vector3 knockBackDirection)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        while (knockBackDuration > timer)
        {
            timer += Time.deltaTime;

            if (ed.walkRight == false)
            {
                rb2d.AddForce(new Vector3(-knockBackDirection.x * knckback, knockBackDirection.y, transform.position.z));
            }
            if (ed.walkRight == true)
            {
                rb2d.AddForce(new Vector3(knockBackDirection.x * knckback2, knockBackDirection.y, transform.position.z));
            }




        }


    }











}
