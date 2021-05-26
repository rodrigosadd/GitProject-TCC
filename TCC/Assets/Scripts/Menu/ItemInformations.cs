using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformations : MonoBehaviour
{
    public Text sentenceText;
    public float delayBetweenLetters;
    [TextArea]
    public string sentence;

    void Start()
    {
        StartCoroutine("TypeSentence");
    }

    IEnumerator TypeSentence()
    {
        sentenceText.text = ""; 

        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(delayBetweenLetters);
            sentenceText.text += letter;
            yield return null;
        }
    }
}
