using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script permettant à une tourelle de se tourner à droite ou à gauche selon la position de notre joueur

public class Attack_Cone : MonoBehaviour
{
    public TurretAI turretAI;
    public bool left = false;

    void Awake()
    {
        turretAI = gameObject.GetComponentInParent<TurretAI>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (left)
            {
                turretAI.Attack(false);
            }
            else
            {
                turretAI.Attack(true);
            }
        }
    }

}
