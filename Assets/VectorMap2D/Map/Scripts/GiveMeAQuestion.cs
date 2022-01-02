using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;


public class GiveMeAQuestion : MonoBehaviour
{
    private bool isThereImage = false;
    private Sprite globalFlagImage;
    [SerializeField][Tooltip("Which mode we want to execute")] string gameMode;
    [SerializeField] Image flagImage; // flag image for the hint
    // displaying the country to find
    [SerializeField] Text displayCountry;
    [SerializeField] Text displayQuestionCountry;
    // displaying hint
    [SerializeField] Text displayHint;
    // recieving the countries
    [SerializeField] GameObject countries;

    // saving our random country
    private int rand = 0;
    private int numOfCountries = 233;
    private int nameOffSet = 6; // in order to remove the word "layer"
    private string countryToFind;

    public Material InvisibleMaterial, SelectedMaterial;
    static GameObject PreviousSelectedObj = null;
    Transform SelectedParentObj;
    private List<KeyValuePair<string,string>> question;

    private void Start()
    {
        if (gameMode.Equals("Mode2"))
        {
            question = new List<KeyValuePair<string, string>>();
            readFromFile();
            
        }
    }

    public void readFromFile()
    {
        string fileName = "Assets/Questions/countriesQuestionsMode2";

        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string [] currentLine = line.Split('-');
                question.Add(new KeyValuePair<string, string>(currentLine[0], "\"" + currentLine[1] + "\""));
            }
        }
    }
    public void mode2()
    {
        int random = Random.Range(0, question.Count);
        countryToFind = question[random].Value;
        // going over our countries to detect at which position is the country we randomed.
        for(int i = 0; i < countries.transform.GetChildCount(); i++)
        {
            Transform child = countries.transform.GetChild(i).GetChild(0);
            if (child.name.Equals(countryToFind))
            {
                rand = i;
            }
        }
        displayQuestionCountry.gameObject.SetActive(true);
        displayQuestionCountry.text = question[random].Key;
        displayCountry.text = question[random].Value;
        displayHint.gameObject.SetActive(false);
        displayHint.text = "";
    }
    public void printCountry()
    {
        Score.resetGuessQuestion();
        if (gameMode.Equals("Mode1"))
            mode1();
        else
            mode2();
    }

        public  void mode1()
    {
        // random number 0 or 1 , if 0 we display country name, otherwise we display flag
        // int randCategory = Random.Range(0, 2);
        int randCategory = 0;
        Transform child;
        //int maxIteration = 3;
        do
        {
            // random number between 0 to 233 : thats the country the player needs to find
            rand = Random.Range(0, numOfCountries);
         //   maxIteration--;
            // getting the country in position rand
            child = countries.transform.GetChild(rand);
            // move Sophie to destination country
    /*        if (randCategory == 1)
            {
                setFlag(child.name.Substring(0, child.name.Length - nameOffSet));
                if (isThereImage && long.Parse(child.transform.GetChild(0).GetComponent<CountryInfo>().Population) > 10000000)
                    break;
            }
            if (maxIteration == 0)
            {
                randCategory = 0;
            }*/
        } while (long.Parse(child.transform.GetChild(0).GetComponent<CountryInfo>().Population) < 10000000);

        // saving the name minus substring of the word layer at the end
        countryToFind = child.name.Substring(0, child.name.Length - nameOffSet);

        if (randCategory == 0)
        {
            // setting text to be active true : 
            displayCountry.gameObject.SetActive(true);
            flagImage.gameObject.SetActive(false);
        }
        else
        {
            // setting image to be active true : 
            flagImage.sprite = globalFlagImage;
            flagImage.gameObject.SetActive(true);
            displayCountry.gameObject.SetActive(false);
        }
        // displaying the country to the user
        displayCountry.text = countryToFind;
        displayHint.text = "";
        displayHint.gameObject.SetActive(false);
        setInvisible();
    }
    public void hint() // displaying the continent as a hint
    {
        if (!countryToFind.Equals(""))
        {
            displayHint.gameObject.SetActive(true);
            string hint = countries.transform.GetChild(rand).GetChild(0).GetComponent<CountryInfo>().Continent;
            displayHint.text = "Search at " + hint;
        }
    }

    public void showCountry() // reveal the country the player needs to find
    {
        setInvisible();

        if (PreviousSelectedObj != null) {
            Transform PreviousParentObj = PreviousSelectedObj.transform.parent;
            for (int ch = 0; ch < PreviousParentObj.childCount; ch++)
                {
                    PreviousParentObj.GetChild(ch).GetComponent<MeshRenderer>().material = InvisibleMaterial;
                }
            }
        SelectedParentObj = countries.transform.GetChild(rand);
        CameraCtrl.setInvisible(SelectedParentObj.transform.GetChild(0).gameObject);
        
       for (int ch = 0; ch < SelectedParentObj.childCount; ch++)
        {
            SelectedParentObj.GetChild(ch).GetComponent<MeshRenderer>().material = SelectedMaterial;
        }

        settingPrevious(SelectedParentObj.transform.GetChild(0).gameObject);
    }

    public static void settingPrevious(GameObject prev) // setting the previous country to be invinsible
    {
        PreviousSelectedObj = prev;
    }
    public void setInvisible()
    {
        if (PreviousSelectedObj != null)
        {
            Transform PreviousParentObj = PreviousSelectedObj.transform;
            for (int ch = 0; ch < PreviousParentObj.childCount; ch++)
            {
                PreviousParentObj.GetChild(ch).GetComponent<MeshRenderer>().material = InvisibleMaterial;
            }
        }

    }



    public void setFlag(string name)
    {
        try
        {
            string API_URL = "https://restcountries.com/v3.1/name/" + name;
            // requesting the image url :
            System.Net.WebRequest web = WebRequest.Create(API_URL);
            web.Method = "GET";
            HttpWebResponse response = null;

            response = (HttpWebResponse)web.GetResponse();


            string result = "";
            using (Stream stream = response.GetResponseStream()) // returns a json in string format
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd(); // assigning it to our string
                sr.Close(); // close the stream reader
            }
            string url = "";
            var jsonData = JsonConvert.DeserializeObject(result); // convers string to an object, in our case json.
            JArray json = (JArray)(jsonData);
            foreach (JObject elem in json)
            {
                JObject flags = (JObject)(elem.GetValue("flags"));
                if (flags != null)
                    url = flags.GetValue("png").ToString();
            }
            StartCoroutine(setDownloadImage(url)); // starting coroutine in order to display the image from the url we got
        }
        catch (System.Exception e) // exception -> setting image to default white
        {
            // flagImage.GetComponent<Image>().sprite = null;
            isThereImage = false;
            Debug.Log("null image");
        }
    }
    public IEnumerator setDownloadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        // flagImage.GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        if (www.texture == null)
            Debug.Log("texture null");
        globalFlagImage = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        isThereImage = true;
    }
}
