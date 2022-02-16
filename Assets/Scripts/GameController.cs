using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public Text wordToFindField;
    private float time;
    private string[] wordsLocal = { "mira", "sam", "suheyl", "robert", "john", "james", "mary jane" };
    private string chosenWord;
    private string hiddenWord;

    // Start is called before the first frame update
    void Start()
    {
        chosenWord = wordsLocal[Random.Range(0, wordsLocal.Length)];

        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            if(char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else
            {
                hiddenWord += "-";
            }
        }
        wordToFindField.text = hiddenWord;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeField.text = time.ToString();

    }

    private void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString().ToLower();
            
            if(chosenWord.Contains(pressedLetter))
            {
                int index = chosenWord.IndexOf(pressedLetter);
                while(index != -1)
                {
                    hiddenWord = hiddenWord.Substring(0, index) + pressedLetter + hiddenWord.Substring(index + 1);
                    chosenWord = chosenWord.Substring(0, index) + "-" + chosenWord.Substring(index + 1);
                    index = chosenWord.IndexOf(pressedLetter);
                }
                wordToFindField.text = hiddenWord;
            }

        }
        else
        {

        }
    }
}
