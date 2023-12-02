using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBoxScript : MonoBehaviour
{
    [SerializeField][TextArea]
    private List<string> dialogueLines;
    private int lineIndex;
    private TMP_Text text;
    public float typingSpeed;
    public GameObject continueBtn;
    public GameObject UIPanel;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(Type());
        UIPanel.gameObject.SetActive(false);
    }
    void Update()
    {
        if(text.text == dialogueLines[lineIndex])
        {
            continueBtn.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach(char letter in dialogueLines[lineIndex].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueBtn.SetActive(false);
        if(lineIndex < dialogueLines.Count-1)
        {
            lineIndex++;
            text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            UIPanel.gameObject.SetActive(true);
            Destroy(transform.parent.gameObject);
        }
    }
}
