using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script controllant les déplacements et l'attaque du boss
// À une certaine distance le Boss détecte l'ennemi et commence à le pourchaser
// Quand le boss a plus de 50% de ses PV il lance une boule de feu et lorsqu'il a moins de 50% de ses PV
// il en lance 2 et augmente sa vitesse de déplacement

public class BossAI : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public Slider hpBar;
    public ParticleSystem bloodEffect;
    private GameMaster gm;
    public bool living;
    private Animator anim;
    public string idle;
    public string walk;
    public float distance;
    private Player p;
    public Transform player;
    public int movespeed;
    private Rigidbody2D rb2d;
    public int rangeFollow;
    public GameObject bullet;
    public GameObject hitStart;
    public GameObject bullet2;
    public GameObject hitStart2;
    public float bulletTimer;
    public float shootInterval;
    public float shootInterval2;
    public bool fightStart;
    public SpriteRenderer sr;
    private AudioManager am;
    public Color original;




    private void Start()
    {
        currHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        living = true;
        anim.Play(idle);
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        fightStart = false;
        am = FindObjectOfType<AudioManager>();
        original = sr.color;
    }

    private void Update()
    {

        distance = Vector2.Distance(transform.position, player.position);
        if (distance < rangeFollow)
        {
            FollowPlayer();
            anim.Play(walk);

        }
        else
        {
            StopFollowPlayer();

        }
        hpBar.value = currHealth;
        Attack();
        Die();
    }



    public void Damage(int dmg)
    {
        am.Play("bossAttacked2");   // map paradise + night
        am.Play("bossAttacked");   // map forest day 
        am.Play("bossAttacked1"); // map usine
        currHealth -= dmg;
        BloodEffect();
        

    }

    private void BloodEffect()
    {
        bloodEffect.Play();
    }

    public void Die()
    {
        if (currHealth <= 0)
        {
            Destroy(gameObject);
            p.sr.material.color = p.original;

            gm.points += 100;
            living = false;
        }
        
    }

    private void FollowPlayer()
    {
        fightStart = true;
        if (transform.position.x < player.position.x)
        {
            if (currHealth > maxHealth / 2)
            {
                rb2d.velocity = new Vector2(movespeed, 0);
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                rb2d.velocity = new Vector2(movespeed + 1.5f, 0);
                transform.localScale = new Vector2(1, 1);
                sr.material.color = Color.red;
            }
            
        }
        if (transform.position.x > player.position.x)
        {
            if (currHealth > maxHealth / 2)
            {
                rb2d.velocity = new Vector2(-movespeed, 0);
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                rb2d.velocity = new Vector2(-movespeed - 1.5f, 0);
                transform.localScale = new Vector2(-1, 1);
                sr.material.color = Color.red;
            }
            
        }
    }

    private void StopFollowPlayer()
    {

        rb2d.velocity = Vector2.zero;

    }


    private void Attack()
    {
        if(fightStart == true && currHealth > maxHealth/2) { 
        bulletTimer += Time.deltaTime;
            if (bulletTimer >= shootInterval)
            {
                Vector2 direction = player.transform.position - transform.position;
                direction.Normalize();
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, hitStart.transform.position, hitStart.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * 10;
                bulletTimer = 0;
                am.Play("bossBullet");
                am.Play("bossFire");
            }
        }

        if(fightStart == true && currHealth <= maxHealth / 2)
        {
            bulletTimer += Time.deltaTime;
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            if (bulletTimer >= shootInterval)
            {

                GameObject bulletClone;
                bulletClone = Instantiate(bullet, hitStart.transform.position, hitStart.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * 10;
                GameObject bulletClone2;
                bulletClone2 = Instantiate(bullet2, hitStart.transform.position, hitStart.transform.rotation) as GameObject;
                bulletClone2.GetComponent<Rigidbody2D>().velocity = direction * 5;
                bulletTimer = 0;
                am.Play("bossBullet");
                am.Play("bossFire");

            }

        }
    }





}
