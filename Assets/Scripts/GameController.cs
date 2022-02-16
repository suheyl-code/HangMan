using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public Text wordToFindField;
    public GameObject[] hangMan;
    public GameObject winText;
    public GameObject loseText;
    public GameObject replayButton;
    private float time;
    private string[] words = File.ReadAllLines(@"Assets/Words.txt");
    private string chosenWord;
    private string hiddenWord;
    private int fails;
    private bool isGameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        chosenWord = words[Random.Range(0, words.Length)];

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
        if(!isGameEnd)
        {
            time += Time.deltaTime;
            timeField.text = time.ToString();
        }

    }

    private void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString().ToLower();
            //fails = 0;
            if (chosenWord.Contains(pressedLetter))
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
            // Adding hangman body parts
            else
            {
                hangMan[fails].SetActive(true);
                fails++;
            }

            // in case of losing game
            if(fails == hangMan.Length)
            {
                loseText.SetActive(true);
                replayButton.SetActive(true);
                isGameEnd = true;
            }

            // in case of winning game
            if(!hiddenWord.Contains("-"))
            {
                winText.SetActive(true);
                replayButton.SetActive(true);
                isGameEnd = true;
            }
        }
        
    }
}
