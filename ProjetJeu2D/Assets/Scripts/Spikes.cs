using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Des piques sont mis sur la map.
// Si la player rentre en collision avec ces piques alors il prendra 1 de dégat, sera propulsé en l'air, changera de couleur (rouge) pour enfin revenir à sa couleur
// initiale.
// Dans les airs il peut bouger et donc éviter de retomber sur ces piques sinon le même scénario se fait.

public class Spikes : MonoBehaviour
{
    private Player player;
    private AudioManager am;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        am = FindObjectOfType<AudioManager>();
       
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            player.Damage(1);
            StartCoroutine(player.Knockback(0.02f, 350, player.transform.position));
            player.doubleJump = false;
            am.Play("spikes");


        }

    }


        






}
