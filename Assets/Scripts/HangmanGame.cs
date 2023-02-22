using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Hangman.Scripts
{
    public class HangmanGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private int hp = 7;
        [SerializeField] private TextMeshProUGUI _textHp;
        [SerializeField] private TextMeshProUGUI _cluesText;
        [SerializeField] private TextMeshProUGUI _textLetters;
        
        private List<char> _guessedLetters = new List<char>();
        private List<char> _wrongTriedLetter = new List<char>();
        private string initialWord = "";
        
        private string[] _words =
        {
            "Apple",
            "Sea",
            "Deer",
            "Tree",
            "Pain"
        };

        private string[] _clues =
        {
            "Steve Jobs",
            "Большое синее",
            "Рогатый",
            "Высокое,зеленое",
            "Упал и чувствуешь"
        };   

        private string _wordToGuess = "";
        private string _clue = "";
        private KeyCode _lastKeyPressed;
        private void Start()
        {
            var randomIndex = Random.Range(0, _words.Length);
            _wordToGuess = _words[randomIndex];
            _clue = _clues[randomIndex];
            _cluesText.text = _clue;
            
            for (int i = 0; i < _wordToGuess.Length; i++)
            {
                initialWord+= " _";
            }
            _textField.text = initialWord;
            _textHp.text = "Hp left = " + hp.ToString();
        }
        private void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode != KeyCode.None && _lastKeyPressed != e.keyCode) 
                    ProcessKey(e.keyCode);
                    
                _lastKeyPressed = e.keyCode;
                
            }
        }

        private void ProcessKey(KeyCode key)
        {
            print("Key Pressed: " + key);
            var pressedKeyString = key.ToString()[0];
            string wordUppercase = _wordToGuess.ToUpper();
            
            bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
            bool letterWasGuessed = _guessedLetters.Contains(pressedKeyString);

            if (!wordContainsPressedKey && !_wrongTriedLetter.Contains(pressedKeyString))
            {
                _wrongTriedLetter.Add(pressedKeyString);
                hp -= 1;

                if (hp <= 0)
                {
                    print("You Lost");
                    SceneManager.LoadScene("YouLost");
                }
                else
                {
                    _textHp.text = "Hp left = " + hp.ToString();
                }
            }
            
            if (wordContainsPressedKey && !letterWasGuessed)
            {
                _guessedLetters.Add(pressedKeyString);
            }

            string stringToPrint = "";
            for (int i = 0; i < wordUppercase.Length; i++)
            {
                char letterInWord = wordUppercase[i];

                if (_guessedLetters.Contains(letterInWord))
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
                SceneManager.LoadScene("YouWin");
            }
            
           
            print(stringToPrint);
            _textField.text = stringToPrint;
        }
    }
}
