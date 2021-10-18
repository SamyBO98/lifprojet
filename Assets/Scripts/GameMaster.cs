using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Ce script permet de récupérer les bonnes valeurs après avoir changer de niveau.
// On récupère donc toutes nos améliorations et toutes nos golds quand on change de niveau.
// Ce script permet aussi l'affichage en temps réel des nombres de pièces (golds) que nous avons.

public class GameMaster : MonoBehaviour
{

    public int points;
    public Text pointsTxt;
    public Text InputTxt;
    public Player p;
    public AttackTrigger at;
    
   

    private void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        at = GameObject.FindGameObjectWithTag("Attack").GetComponent<AttackTrigger>();
        if (PlayerPrefs.HasKey("MaxHealth"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                p.maxHealth = PlayerPrefs.GetInt("MaxHealth");
            }

        }

        if (PlayerPrefs.HasKey("Speed"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                p.speed = PlayerPrefs.GetFloat("Speed");

            }

        }

        if (PlayerPrefs.HasKey("Coins"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                points = PlayerPrefs.GetInt("Coins");
            }

        }

        if (PlayerPrefs.HasKey("CurrentHealth"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                p.currentHealth = p.maxHealth;
            }
            else
            {
                p.currentHealth = PlayerPrefs.GetInt("CurrentHealth");
            }

        }
        if (PlayerPrefs.HasKey("Damage"))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                at.dmg = PlayerPrefs.GetInt("Damage");
            }

        }

    }
    private void Update()
    {
        
        pointsTxt.text = ("Golds: " + points);
        
    }


}
