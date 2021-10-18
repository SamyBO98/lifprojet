using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère la caméra. 
// Quand le player se déplace la caméra se déplace aussi en même temps que lui mais avec un petit temps de décalage pour ajouter un effet agréable
// On peut aussi mettre des valeurs min et max de caméra pour que la caméra ne suive pas le joueur jusqu'à l'infinie dans le cas où ce dernier 
// tombe dans le vide

public class CameraFollow : MonoBehaviour
{

    private Vector2 velocity;
    public float camX;
    public float camY;

    public GameObject player;

    public bool bornes;
    public Vector3 minCameraPosition;
    public Vector3 maxCameraPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, camX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, camY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bornes)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                                             Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                                             Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));

        }
    }

    public void SetMinPositon()
    {
        minCameraPosition = gameObject.transform.position;
    }

    public void SetMaxPosition()
    {
        maxCameraPosition = gameObject.transform.position;
    }
}
