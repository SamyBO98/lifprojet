using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Quand nous sommes dans la map Océan les piques nous repoussent et selon où se trouve le player il le repousse à droite ou à gauche.
// Ce script permet donc de repousser le player à droite et de lui mettre 1 de dégat.
// Après avoir pris 1 dégat, le player devient rouge puis reprend sa couleur normale.

public class EnnemiesOceanRight : MonoBehaviour
{
    private Player player;
    private AudioManager am;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        am = FindObjectOfType<AudioManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(KnockbackOceanLeft(0.01f, 250, player.transform.position));
        }
    }


    public IEnumerator KnockbackOceanLeft(float knockBackDuration, float knockBackPower, Vector3 knockBackDirection)
    {
        float timer = 0;
        player.rb2d.velocity = new Vector2(player.rb2d.velocity.x, 0);
        while (knockBackDuration > timer)
        {
            timer += Time.deltaTime;
            player.rb2d.AddForce(new Vector3(-knockBackDirection.x * 20, knockBackDirection.y, transform.position.z));
            player.Damage(1);
            player.sr.material.color = Color.red;
            yield return new WaitForSeconds(player.timeColored);
            player.sr.material.color = player.original;
            am.Play("spikes");



        }


        yield return 0;

    }
}
