using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class TextManager : MonoBehaviour
{
    private Queue<string> introduction;
    [SerializeField] public Text text;
    [SerializeField] private TextMeshProUGUI intro;
    [SerializeField] private bool typingOver;
    [SerializeField] private GameObject generalManager;



    public void StartDialogue()
    {
        introduction = new Queue<string>();
        typingOver = true;

        foreach (string sentence in text.sentence)
        {
            introduction.Enqueue(sentence);
        }
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        AudioManager.instance.PlaySFX("UISelect");
        if (introduction.Count == 0 && typingOver == true)
        {
            EndDialogue();
            return;
        }
        else
        {
            if (typingOver == true)
            {
                string sentence = introduction.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }
        }
    }


    IEnumerator TypeSentence(string sentence)
    {
        intro.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            typingOver = false;
            intro.text += letter;
            yield return new WaitForSeconds(0f);
        }
        typingOver = true;
    }


    void EndDialogue()
    {
        StopAllCoroutines();
        intro.text = "";
        generalManager.GetComponent<UIManager>().NextScreen();
    }

}
