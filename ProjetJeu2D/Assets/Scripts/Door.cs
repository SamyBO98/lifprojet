using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Ce script permet de changer de niveau à l'aide de portes.
// Quand on appuye sur E ou sur Rond de la manette PS4 vers une porte on est transportée vers une autre zone.
// On ne perd pas nos PV ni nos dégats ni nos golds grâce à une fonction Save qui enregistre tout ces paramètre au moment où on appuye sur E.

public class Door : MonoBehaviour
{

    public int SceneToLoad;
    public GameMaster gm;
    public Image img;
    public Player p;
    public AttackTrigger at;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        img.gameObject.SetActive(false);
        p = GameObject.Find("Player").GetComponent<Player>();
        at = GameObject.FindGameObjectWithTag("Attack").GetComponent<AttackTrigger>();

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.InputTxt.text = ("[E] to enter");
            
            img.gameObject.SetActive(true);
            
        }
        if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2"))
        {
            SaveScore();
            SceneManager.LoadScene(SceneToLoad);

        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2"))
            {

                SaveScore();
                SceneManager.LoadScene(SceneToLoad);
                
            }

        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            gm.InputTxt.text = (" ");
            img.gameObject.SetActive(false);
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("MaxHealth", p.maxHealth);
        PlayerPrefs.SetFloat("Speed", p.speed);
        PlayerPrefs.SetInt("Coins", gm.points);
        PlayerPrefs.SetInt("CurrentHealth", p.currentHealth);
        PlayerPrefs.SetInt("Damage", at.dmg);
       
    }

}
