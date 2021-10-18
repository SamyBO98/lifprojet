using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Quand le player va entrer en collision avec une zone (ici un ressort) ce dernier va être propulsé dans les airs.

public class VolAir : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb2d;
    private Vector3 direction;
    public float force;
    private AudioManager am;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2d = player.GetComponent<Rigidbody2D>();
        force = rb2d.mass * force;
        am = FindObjectOfType<AudioManager>();
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
           
            direction = collider.contacts[0].normal;
            rb2d.AddForce((direction * -1) * force);
            am.Play("ressortFly");
        }
    }


        






}
