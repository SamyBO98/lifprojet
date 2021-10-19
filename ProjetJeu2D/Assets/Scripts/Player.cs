using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script permettant de controller le player.
// Ce dernier a des caractéristiques de base qui lui sont données à partir du moment où il entre dans la première map.
// Il peut se déplacer de droite à gauche (soit avec les flèches du clavier sois) avec le joystick et peut sauter avec Espace ou avec X de la manette PS4.
// Le sprite change aussi de sens selon sa direction.
// Il a la possibilité de faire un double saut et de faire une petite animation pour ce double saut (de la poussière).
// Il peut aussi grimper au mur s'il reconnait une surface grimpable (utilisation d'un Layer WALL).

public class Player : MonoBehaviour
{
    public float maxSpeed = 3;
    public float speed = 250f;
    public float jumpPower = 150f;
    public bool grounded;
    public bool doubleJump;
    public int  currentHealth;
    public int maxHealth = 5;
    public bool wallSliding;
    public bool facingRight = true;
    public Rigidbody2D rb2d;
    public Animator anim;
    public MenuPause mp;
    public SpriteRenderer sr;
    public Color original;
    public float timeColored = 0.3f;
    public ParticleSystem Dust;
    private GameMaster gm;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;
    public Swim s;
    private CheckpointController cpC;
    public Vector3 respawnPoint;
    private AudioManager am;
    public int level;
    public AttackTrigger at;
    public BossAI ba;


    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        s = gameObject.GetComponent<Swim>();
        at = gameObject.GetComponent<AttackTrigger>();
        am = FindObjectOfType<AudioManager>();
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
        grounded = true;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        cpC= GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointController>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentHealth = maxHealth;
            
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gm.points = 0;
        }

        ba = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();

    }


    void Update()
    {

        level = SceneManager.GetActiveScene().buildIndex;
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        if (mp.paused == false)
        {
            if (Input.GetAxis("Horizontal") < -0.1f)
            {

                transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
                am.Play("walking");

            }

            if (Input.GetAxis("Horizontal") > 0.1f)
            {

                transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
                am.Play("walking");

            }

            if (Input.GetButtonDown("Jump") && !wallSliding)
            {
                if (grounded)
                {

                    rb2d.AddForce(Vector2.up * jumpPower);
                    doubleJump = true;
                    am.Play("jump");
                }
                else
                {
                    if (doubleJump)
                    {
                        CreateDust();
                        doubleJump = false;
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                        rb2d.AddForce(Vector2.up * jumpPower);
                        am.Play("jump");
                    }
                }


            }
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if(currentHealth <= 0)
        {
            am.Play("die");
            am.StopPlaying("BossMusic");
            ba.fightStart = false;
            ba.currHealth = ba.maxHealth;
            ba.sr.material.color = ba.original;
            

            if(gm.points > 100)
            {
                gm.points = gm.points - 100;
            }
            transform.position = cpC.respawnPoint;
            Start();
            PlayerPrefs.DeleteKey("Coins");
            currentHealth = maxHealth;
            
        }

        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if(facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < -0.1f)
            {
                if (wallCheck)
                {
                    WallSliding();
                    anim.SetBool("Climb", wallCheck);
                    
                }
                
            }

        }

        if(wallCheck == false || grounded)
        {
            wallSliding = false;
            anim.SetBool("Climb", false);
           
            
            
        }
    }

    void WallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);

        wallSliding = true;
        

       

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                
                rb2d.AddForce(new Vector2(-1.5f, 1) * jumpPower);
            }
            else
            {
                rb2d.AddForce(new Vector2(1.5f, 1) * jumpPower);
            }
        }
    }

    void FixedUpdate()
    {

        Vector3 vitesseLimit = rb2d.velocity;
        vitesseLimit.y = rb2d.velocity.y;
        vitesseLimit.z = 0.0f;
        vitesseLimit.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");

        if (grounded)
        {

            rb2d.velocity = vitesseLimit;
            
        }

        if (grounded)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed/2) * h);
        }

        

        if(rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    public void Damage(int dmg)
    {
        if (currentHealth < dmg)
        {
            dmg = currentHealth;
        }
        currentHealth -= dmg;
        
    }

    public IEnumerator Knockback (float knockBackDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        while(knockBackDuration> timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(-knockBackDirection.x, knockBackDirection.y + knockBackPower*3, transform.position.z));
            sr.material.color = Color.red;
            yield return new WaitForSeconds(timeColored);
            sr.material.color = original;
        }
        

        yield return 0;

    }


    void CreateDust()
    {
        Dust.Play();
    }

    public IEnumerator Flasher()
    {

        sr.material.color = Color.red;
        yield return new WaitForSeconds(1);
        sr.material.color = Color.white;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            
            Destroy(collision.gameObject);
            gm.points += 5;
            am.Play("money");
           
        }
        if (collision.tag == "FallDetector")
        {
          transform.position = cpC.respawnPoint;
        }
        if (collision.tag == "Checkpoint")
        {
             if (collision.GetComponent<CheckpointController>().checkpointReached)
            {
                cpC = collision.GetComponent<CheckpointController>();
            }
            
        }
    }
}
