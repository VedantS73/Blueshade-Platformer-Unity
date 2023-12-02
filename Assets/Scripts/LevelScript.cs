using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel >= PlayerPrefs.GetInt("ReachedLevel"))
        {
            PlayerPrefs.SetInt("ReachedLevel", currentLevel + 1);
        }
        Debug.Log("Player just unlocked Level " + PlayerPrefs.GetInt("ReachedLevel"));
    }
}
