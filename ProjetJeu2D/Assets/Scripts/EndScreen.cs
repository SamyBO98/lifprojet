using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ce script permet au bouton de fin de nous ramener à Menu Principal (Scene 0).

public class EndScreen : MonoBehaviour
{
  
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
