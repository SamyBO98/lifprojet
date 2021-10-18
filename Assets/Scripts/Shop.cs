using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Ce script gère le Shop.
// Quand on se met vers l'entrée du Shop, une fenêtre apparait permettant au player de faire des achats.
// Selon ses golds (son nombre de pièces) le player pourra acheter une ou plusieurs améliorations.
// S'il n'est plus capable d'acheter une amélioration (par manque d'argent ou parcequ'il a épuisé le stock) alors le bouton se grise et est inutilisable.
// On peut naviguer grâce à la souris ou grâce au pavé à gauche de la manette de PS4(le DPAD).
// Pour effectuer un achat il suffit soit de cliquer avec la souris soit d'appuyer sur la touche R1 de la manette PS4.

public class Shop : MonoBehaviour
{
    public GameObject ShopControl;
    private Player p;
    private GameMaster gm;
    public Text speedTxt;
    public Button speedButton;
    public int speedCost;
    public int speedBoost;
    public MenuPause mp;
    public int currHlCost;
    public int currHlBoost;
    public Text currHealthTxt;
    public Button CurrentHealthButton;
    public Text attackTxt;
    public Button attackButton;
    public int attackCost;
    public int attackBoost;
    private AttackTrigger at;
    public Button maxHealthButton;
    public int maxHealthUpCost;
    public int maxHealthUpBoost;
    public Text maxHealth;
    private AudioManager am;



    private void Start()
    {
        ShopControl.SetActive(false);
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        at = GameObject.FindGameObjectWithTag("Attack").GetComponent<AttackTrigger>();
        am = FindObjectOfType<AudioManager>();



    }

    private void Update()
    {
        speedTxt.text = ("Speed: " + p.speed);
        currHealthTxt.text = ("CurrentHealth: " + p.currentHealth);
        attackTxt.text = ("Attaque: " + at.dmg);
        maxHealth.text = ("Vie max: " + p.maxHealth);
        if (gm.points >= speedCost)
        {
            speedButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            speedButton.GetComponent<Image>().color = Color.grey;
        }

        if (gm.points <= currHlCost || p.currentHealth == p.maxHealth)
        {
            CurrentHealthButton.GetComponent<Image>().color = Color.grey;
        }
        else
        {

            CurrentHealthButton.GetComponent<Image>().color = Color.white;
        }

        if (gm.points >= attackCost)
        {
            attackButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            attackButton.GetComponent<Image>().color = Color.grey;

        }

        if (gm.points >= maxHealthUpCost && p.maxHealth != 10)
        {
            maxHealthButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            maxHealthButton.GetComponent<Image>().color = Color.grey;
        }

        if (mp.paused == true)
        {
            ShopControl.SetActive(false);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShopControl.SetActive(true);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShopControl.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShopControl.SetActive(false);
    }

    public void Speed()
    {

        if (gm.points >= speedCost)
        {
            gm.points -= speedCost;
            p.speed += speedBoost;
            am.Play("click");

        }

    }

    public void CurrHealth()
    {
        if (gm.points >= currHlCost)
        {

            if (p.currentHealth < p.maxHealth)
            {
                gm.points -= currHlCost;
                p.currentHealth += currHlBoost;
                am.Play("click");
            }

        }
    }

    public void AttackBoost()
    {
        if (gm.points >= attackCost)
        {
            gm.points -= attackCost;
            at.dmg += attackBoost;
            am.Play("click");
        }
    }

    public void MaxHealthBoost()
    {
       if(gm.points >= maxHealthUpCost && p.maxHealth < 10)
        {
            gm.points -= maxHealthUpCost;
            p.maxHealth += maxHealthUpBoost;
            am.Play("click");
        }
            
        
    }
}
