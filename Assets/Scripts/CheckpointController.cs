using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// Quand on meurt on ne réapparait plus à la base grâce à ce script
// Ce dernier nous permet de réapparaitre au niveau du dernier checkpoint pris

public class CheckpointController : MonoBehaviour
{
    public Sprite redFlag;
    public Sprite greenFlag;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;
    private Player p;
    public Vector3 respawnPoint;
    public int cpt;
    public int id;

    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint = p.transform.position;
        checkpointReached = false;
        cpt = 0;
        id = 0;
    }

    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetReached(true);

        }

    }

    public void SetReached(bool isReached)
    {

        if (!checkpointReached)
        {

            cpt++;

            checkpointReached = isReached;
           
            respawnPoint = p.transform.position;
            checkpointSpriteRenderer.sprite = greenFlag;
        }

    }

}
  
