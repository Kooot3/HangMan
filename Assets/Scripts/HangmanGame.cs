using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace Hangman.Scripts
{
    public class HangmanGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private int hp = 7;
        [SerializeField] private TextMeshProUGUI _textLetters;
        [SerializeField] private TextMeshProUGUI _textHp;
        
        private List<char> guessedLetters = new List<char>();
        private List<char> wrongTriedLetter = new List<char>();
        private string initialWord = "";
        public RestartGame gameManegerWin;
        public RestartGame gameManagerLost;

        private string[] words =
        {
            "Apple",
            "Dad",
            "Deer",
            "Tree",
            "Pain"
        };

        private string wordToGuess = "";
        
        private KeyCode lastKeyPressed;

        private void Start()
        {
            var randomIndex = Random.Range(0, words.Length);

            wordToGuess = words[randomIndex];
            
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                initialWord+= " _";
            }
            _textField.text = initialWord;
            _textHp.text = "Hp left = " + hp.ToString();
        }
        void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey)
            {
                // Debug.Log("Detected key code: " + e.keyCode);

                if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
                {
                    ProcessKey(e.keyCode);
                    
                    lastKeyPressed = e.keyCode;
                }
            }
        }

        private void ProcessKey(KeyCode key)
        {
            print("Key Pressed: " + key);

            char pressedKeyString = key.ToString()[0];

            string wordUppercase = wordToGuess.ToUpper();
            
            bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
            bool letterWasGuessed = guessedLetters.Contains(pressedKeyString);

            if (!wordContainsPressedKey && !wrongTriedLetter.Contains(pressedKeyString))
            {
                wrongTriedLetter.Add(pressedKeyString);
                hp -= 1;

                if (hp <= 0)
                {
                    print("You Lost");
                }
                else
                {
                    _textHp.text = "Hp left = " + hp.ToString();
                }
            }
            
            if (wordContainsPressedKey && !letterWasGuessed)
            {
                guessedLetters.Add(pressedKeyString);
            }

            string stringToPrint = "";
            for (int i = 0; i < wordUppercase.Length; i++)
            {
                char letterInWord = wordUppercase[i];

                if (guessedLetters.Contains(letterInWord))
                {
                    stringToPrint += letterInWord;
                }
                else
                {
                    stringToPrint += "_";
                }
            }

            if (wordUppercase == stringToPrint)
            {
                print("You win!");
            }
            
            // print(string.Join(", ", guessedLetters));
            print(stringToPrint);
            _textField.text = stringToPrint;
        }
    }
}
