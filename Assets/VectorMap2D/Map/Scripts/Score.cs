using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int guess; // variable to increment the guesses accorindly
    [SerializeField] Text guessText; // the text that displays the number of the current country guesses
    [SerializeField] GameObject dialog; // statistic dialog, when the player wants to finish the game, we will show all the countries and guesses.
    private Dictionary<string, int> hm; // hashmap , in order to save the country we gave the player, and the number of guesses it tooks the player to find

    // Start is called before the first frame update
    void Start()
    {
        // initialization
        guess = 0;
        hm = new Dictionary<string, int>();
    }


    // when we are wrong, the guesses increment by one, and we display the text with the current new guess
    public void setGuesses()
    {
        // take score and increment by one
        guess++;
        // display current guess
        guessText.text ="Guesses : " + guess;
    }

    // when we get the country correctly, we save the country with the number of guesses it took. and reset the guesses for the next country
    public void resetGuess(string country)
    {
        guess++;
        // saving score with the country name
        if (hm.ContainsKey(country)) // if we already got that country, we update the guesses
        {
            hm[country] = guess;
        }
        else // if its the first time we recieve that country, we add it
        {
            hm.Add(country, guess);
        }

        guess = 0;
        guessText.text="Guesses : " + 0;
    }
    public void Update()
    {
        // when we activate the statistic dialog, we want to display the hashmap results, so we call setStatistics()
        if(dialog.gameObject.activeSelf== true)
        {
            setStatistics();
        }
    }

    // function that sets the string to be displayed for the statistics
    public void setStatistics()
    { 
        string result = "";
        int index = 1;
        // iterating on the hackmap, we get key - country, value - guesses
        foreach (var item in hm)
        {
            result += index + ". " + item.Key + " : " + item.Value + "\n" ;
            index++;
        }
        // accessing the text displayer, which is in the dialog, child number 3.
        dialog.transform.GetChild(2).GetComponent<Text>().text=result;
    }
}
