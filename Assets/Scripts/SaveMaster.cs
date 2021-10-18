using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Le système de sauvegarde de donnée est assez inédit.
// En effet, on peut sauvegarder à certains points de la map (au tout début).
// Si on quitte, le bouton load du Menu de départ nous ramène à notre map sauvegarder.
// Pour récupérer nos Golds et bonus il faut appuyer une deuxième fois sur Load (bouton de la map) car la sauvegarde est liée à la map.
// Dans le cas où il n'y a aucune sauvegarde le Player est ammener au tout début du niveau 1 comme si c'était une nouvelle partie.

public class SaveMaster : MonoBehaviour
{
    public Player p;
    public GameMaster gm;
    public AttackTrigger at;

    public void SavePlayerInfo()
    {
        SaveSystem.SavePlayer(p, gm, at);
    }

    public void LoadPlayer()
    {
        
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            p.level = data.level;
            p.maxHealth = data.maxHealth;
            p.currentHealth = data.currHealth;
            at.dmg = data.attaque;
            p.speed = data.speed;
            gm.points = data.golds;
        }
    }

    public void LoadScene()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            p.level = data.level;
            SceneManager.LoadScene(data.level);
            p.currentHealth = 5;
            p.speed = 250;
            gm.points = 0;
            at.dmg = 20;
        }
        else
        {
            p.level = 1;
            SceneManager.LoadScene(p.level);
            p.currentHealth = 5;
        }
    
    }


}
