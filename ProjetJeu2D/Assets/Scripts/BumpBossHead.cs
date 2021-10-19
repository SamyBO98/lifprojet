using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand on saute sur la tête du boss le player peut rebondir dessus.
// Cela évite de se mettre sur sa tête (un endroit qui peut amener le Boss à ne plus savoir où tirer)

public class BumpBossHead : MonoBehaviour
{
    private Player p;


    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Knockback(0.02f, 350, p.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Knockback(0.02f, 350, p.transform.position);
        }
    }


    public void Knockback(float knockBackDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0;
        p.rb2d.velocity = new Vector2(p.rb2d.velocity.x, 0);
        while (knockBackDuration > timer)
        {
            timer += Time.deltaTime;
            p.rb2d.AddForce(new Vector3(-knockBackDirection.x, knockBackDirection.y + knockBackPower * 3, transform.position.z));

        }

    }
}
