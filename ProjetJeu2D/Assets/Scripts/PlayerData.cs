using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// On récupère les informations du player afin de les sauvegarder et les réutiliser plus tard.

[System.Serializable]
public class PlayerData
{
    public int level;
    public int maxHealth;
    public int currHealth;
    public int attaque;
    public float speed;
    public int golds;
    public float[] position;


    public PlayerData(Player player, GameMaster gm, AttackTrigger at)
    {
        level = player.level;
        maxHealth = player.maxHealth;
        currHealth = player.currentHealth;
        attaque = at.dmg;
        speed = player.speed;
        golds = gm.points;
        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
