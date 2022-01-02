using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiveMeAQuestion : MonoBehaviour
{
    [SerializeField] Image flagImage; // flag image for the hint
    [SerializeField] GameObject Sohpie;
    [SerializeField] GameObject player;
    // displaying the country to find
    [SerializeField] Text displayCountry;
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


    public  void printCountry()
    {
        // random number 0 or 1 , if 0 we display country name, otherwise we display flag
        int randCategory = Random.Range(0, 2);
        bool flag = randCategory == 0 ? true : false;

        Transform child;
        do
        {
            // random number between 0 to 233 : thats the country the player needs to find
            rand = Random.Range(0, numOfCountries);
            // getting the country in position rand
            child = countries.transform.GetChild(rand);
            // move Sophie to destination country
            if (randCategory == 1)
                flag = (setFlag(child.name.Substring(0, child.name.Length - nameOffSet))) && ((long.Parse(child.transform.GetChild(0).GetComponent<CountryInfo>().Population))  < 10000000);
        } while (long.Parse(child.transform.GetChild(0).GetComponent<CountryInfo>().Population) < 10000000 && flag);


        Sohpie.gameObject.SetActive(false);
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
            flagImage.gameObject.SetActive(true);
            displayCountry.gameObject.SetActive(false);
            setFlag(countryToFind);
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



    public bool setFlag(string name)
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
            return true;
        }
        catch (System.Exception e) // exception -> setting image to default white
        {
            flagImage.GetComponent<Image>().sprite = null;
            return false;
        }
    }
    public IEnumerator setDownloadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        flagImage.GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
