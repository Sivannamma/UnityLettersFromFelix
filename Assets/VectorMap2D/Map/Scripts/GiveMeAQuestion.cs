using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GiveMeAQuestion : MonoBehaviour
{
    ArrayList populationChildIndexes;
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
    private int nameOffSet = 6; // in order to remove the word "layer"
    private string countryToFind;

    public Material InvisibleMaterial, SelectedMaterial;
    static GameObject PreviousSelectedObj = null;
    Transform SelectedParentObj;
    private List<KeyValuePair<string,string>> question;
    private Transform child;

    private void Start()
    {
        if (gameMode.Equals("Mode2"))
        {
            question = new List<KeyValuePair<string, string>>();
            readFromFile();
            
        }
        if (gameMode.Equals("Mode1"))
        {
            populationChildIndexes = new ArrayList();
            for (int i = 0; i < countries.transform.GetChildCount(); i++)
            {
                Transform child = countries.transform.GetChild(i).GetChild(0);
                string name = child.GetComponent<CountryInfo>().name;
                if (long.Parse(child.GetComponent<CountryInfo>().Population) > 8000000 && isFlag(name))
                {
                    populationChildIndexes.Add(i);
                }
            }

        }

    }
    public bool isFlag(string name)
{
        if(name.Equals("Lao PDR") || name.Equals("Dem. Rep. Korea") || name.Equals("W. Sahara") || name.Contains("D'Ivoire") || name.Equals("Dem. Rep. Congo") || name.Equals("Congo"))
             return false;
        return true;
}
    public void Update()
    {
        if(isThereImage)
            flagImage.sprite = globalFlagImage;
    }

    public void readFromFile()
    {
        QuestionsHolder q = new QuestionsHolder();
        question = q.questions();
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
        if(displayQuestionCountry.text.Length > 65)
        {
            displayQuestionCountry.fontSize = 40;
        }
        else
        {
            displayQuestionCountry.fontSize = 50;
        }
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
        int randCategory = 0; // variable to indicate if its flag or country
                                                        
        globalFlagImage = null;
        flagImage.sprite = null;
        isThereImage = false;
       
        rand = Random.Range(0, populationChildIndexes.Count); // random country for the question
            // getting the country in position rand
            rand = (int)(populationChildIndexes[rand]);
            child = countries.transform.GetChild((int)(rand));
             randCategory = Random.Range(0, 2);
        if(randCategory==1)
              setFlag(child.name.Substring(0, child.name.Length - nameOffSet));

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

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    isThereImage = false;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    isThereImage = false;
                    break;
                case UnityWebRequest.Result.Success:
                    string result = webRequest.downloadHandler.text;
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
                    break;
            }
        }
    }

    public void setFlag(string name)
    {
        try
        {
            string API_URL = "https://restcountries.com/v3.1/name/" + name;
            StartCoroutine(GetRequest(API_URL));
        }
        catch (System.Exception e) 
        {
            isThereImage = false;
        }
    }
    public IEnumerator setDownloadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        globalFlagImage = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        isThereImage = true;      
    }
}
