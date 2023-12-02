using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int ReachedLevel;
    public Button[] buttons;

    // Start is called before the first frame update
    private void Awake()
    {
        int ReachedLevel = PlayerPrefs.GetInt("ReachedLevel",1);
    }

    void Start()
    {
        for(int i = 0; i <= buttons.Length-1; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i <= ReachedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int levelindex)
    {
        SceneManager.LoadScene(levelindex);
    }
}
