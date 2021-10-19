using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// L'ennemi patrouilleur peut lui aussi mettre des dégats.
// Si le player rentre en collision frontale avec lui ce dernier met 1 de dégats sans s'arrêter de patrouiller.
// Il chance lui aussi la couleur du player qui, en prenant 1 dégat, devient rouge puis reprend sa couleur normale.

public class PatrolDetectEnnemy : MonoBehaviour

{
    public Patrol pat;
    public Animator anim;
    public Player player;
    public string walk;
    private AudioManager am;

    private void Start()
    {
        pat = gameObject.GetComponentInParent<Patrol>();
        am = FindObjectOfType<AudioManager>();
        GameObject player = GameObject.Find("Player");

    }
    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(KnockbackTrashMob(0.02f, 250, player.transform.position));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pat.anim.Play(walk);
        pat.speed = pat.dfspeed;
    }

    public IEnumerator KnockbackTrashMob(float knockBackDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0;
        player.rb2d.velocity = new Vector2(player.rb2d.velocity.x, 0);
        while (knockBackDuration > timer)
        {
            timer += Time.deltaTime;
            if (pat.movingLeft == true)
            {
                player.rb2d.AddForce(new Vector3(knockBackDirection.x * 10, knockBackDirection.y + knockBackPower * 3, transform.position.z));
                player.Damage(1);
                am.Play("trashChamp");
                am.Play("trashGollem");
                am.Play("trashSpider");
            }

            if(pat.movingLeft == false)
            {
                player.rb2d.AddForce(new Vector3(-knockBackDirection.x * 10, knockBackDirection.y + knockBackPower * 3, transform.position.z));
                player.Damage(1);
                am.Play("trashChamp");
                am.Play("trashGollem");
                am.Play("trashSpider");
            }

            player.sr.material.color = Color.red;
            yield return new WaitForSeconds(player.timeColored);
            player.sr.material.color = player.original;



        }


        yield return 0;

    }
}
