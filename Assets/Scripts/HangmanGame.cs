using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class HangmanGame : MonoBehaviour
{
    [FormerlySerializedAs("_textField")] [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private int hp = 7;
    [FormerlySerializedAs("_textHp")] [SerializeField] private TextMeshProUGUI textHp;
    [FormerlySerializedAs("_cluesText")] [SerializeField] private TextMeshProUGUI cluesText;
    [FormerlySerializedAs("_textLetters")] [SerializeField] private TextMeshProUGUI wrongLetters;

    private readonly List<char> _guessedLetters = new List<char>();
    private readonly List<char> _wrongTriedLetter = new List<char>();
    private string _initialWord = "";
        
    private readonly string[] _words =
    {
        "Apple",
        "Sea",
        "Deer",
        "Tree",
        "Pain"
    };

    private readonly string[] _clues =
    {
        "Steve Jobs",
        "Big blue",
        "Horned",
        "Tall green",
        "Rain and"
    };   

    private string _wordToGuess = "";
    private string _clue = "";
    private KeyCode _lastKeyPressed;
    private void Start()
    {
        var randomIndex = Random.Range(0, _words.Length);
        _wordToGuess = _words[randomIndex];
        _clue = _clues[randomIndex];
        cluesText.text = _clue;
            
        for (int i = 0; i < _wordToGuess.Length; i++)
        {
            _initialWord+= " _";
        }
        textField.text = _initialWord;
        textHp.text = "Hp left = " + hp.ToString();
    }
    private void OnGUI()
    {
        var e = Event.current;
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
        char pressedKeyString = key.ToString()[0];
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
                textHp.text = "Hp left = " + hp.ToString();
                wrongLetters.text +=  pressedKeyString + " ";
            }
        }
            
        if (wordContainsPressedKey && !letterWasGuessed)
        {
            _guessedLetters.Add(pressedKeyString);
        }

        string stringToPrint = "";
        for (var i = 0; i < wordUppercase.Length; i++)
        {
            var letterInWord = wordUppercase[i];

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
        textField.text = stringToPrint;
    }
}