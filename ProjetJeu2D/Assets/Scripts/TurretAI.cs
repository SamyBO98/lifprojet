using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant à la tourelle d'attaquer le player.
// Cette tourelle suit notre position et nous tire dessus.
// Lorsqu'elle est à la moitié de ses PV elle devient grise.
// Lorsqu'elle est détruite elle nous donne 50 golds et nous rend des PV.

public class TurretAI : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;
    public float distance;
    public float range;
    public float shootInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;
    public bool awake = false;
    public bool lookRight = true;

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootLeft;
    public Transform shootRight;
    public SpriteRenderer sr;
    public Player player;
    public GameMaster gm;
    private AudioManager am;

     void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        am = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookRight", lookRight);
        RangeCheck();

        if(target.transform.position.x > transform.position.x)
        {
            lookRight = true;
        }

        if (target.position.x < transform.position.x)
        {
            lookRight = false;
        }

        if(curHealth <= 50)
        {
            sr.material.color = Color.grey;
            
        }

        if(curHealth <= 0)
        {
            Destroy(gameObject);
            gm.points += 50;
            player.sr.material.color = player.original;
            if (player.currentHealth < player.maxHealth)
            {
                player.currentHealth = player.currentHealth + 1;
            }
        }
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance < range)
        {
            awake = true;
            Attack(lookRight);
        }
         if(distance > range)
        {
            awake = false;
        }
    }

    public void Attack(bool attackRight)
    {
        bulletTimer += Time.deltaTime;
        if(bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            if (!attackRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootLeft.transform.position, shootLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                am.Play("electricity");
                bulletTimer = 0;
            }

            if (attackRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootRight.transform.position, shootRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
                am.Play("electricity");
            }
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }

  
}
