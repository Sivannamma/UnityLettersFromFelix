using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public class CameraCtrl : MonoBehaviour
{
	[SerializeField] Image flagImage;
	[SerializeField] string mode;
	// our test that displaying country to find
	[SerializeField] Text displayCountry;
	[SerializeField] Text displayHint;
	public Material InvisibleMaterial, SelectedMaterial;
	static GameObject PreviousSelectedObj = null;

	[SerializeField]
	GameObject player;

	public GameObject CountryInfoPanel;
	public Text CountryName, Sovereignt, Population, Economy, Continent, Region, Subregion;

    Vector3 startPointForMove = new Vector3(), mousePosForMove = new Vector3(), moveVector = new Vector3();
    // Use this for initialization
    void Start()
	{
        if (mode.Equals("PlayMode"))
        {
			EarthMapBuilder.SetPointsVisible();
        }
    }

	// Update is called once per frame
	void Update()
	{

		float wheelDelta = Input.GetAxis("Mouse ScrollWheel");
		if (wheelDelta > 0)
		{
			gameObject.GetComponent<Camera>().fieldOfView--;
			if (gameObject.GetComponent<Camera>().fieldOfView < 10)
				gameObject.GetComponent<Camera>().fieldOfView = 10;
		}
		else if (wheelDelta < 0)
		{
			gameObject.GetComponent<Camera>().fieldOfView++;
			if (gameObject.GetComponent<Camera>().fieldOfView > 65)
				gameObject.GetComponent<Camera>().fieldOfView = 65;

			if (Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).x > 0)
				transform.localPosition += new Vector3(Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).x,0,0);
			if (Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).x < Screen.width)
				transform.localPosition -= new Vector3(Screen.width - Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).x, 0, 0);
			if (Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).y > 0)
				transform.localPosition += new Vector3(0,Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).y, 0);
			if (Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).y < Screen.height)
				transform.localPosition -= new Vector3(0, Screen.height - Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).y, 0);

		}
		if(true)
		//if (!RectTransformUtility.RectangleContainsScreenPoint(EarthMapBuilder.instance.SettingsPanel, Input.mousePosition) && !RectTransformUtility.RectangleContainsScreenPoint(EarthMapBuilder.instance.ColorPicker, Input.mousePosition))
		{
            #region Raycast
            if (Input.GetMouseButtonDown(0) && !player.GetComponent<checkCollisionWithPanel>().checkCollision(Input.mousePosition))
            {
				//------------------
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit, (float)2475f))
				{
					if (hit.transform != EarthMapBuilder.instance.MapBack.transform)
					{
						//Debug.Log("HIT " + hit.transform.name);
						Transform SelectedParentObj = hit.transform.parent;

						if (PreviousSelectedObj != null)
						{
							Transform PreviousParentObj = PreviousSelectedObj.transform.parent;
							PreviousSelectedObj.GetComponent<MeshRenderer>().material = InvisibleMaterial;
							for (int ch = 0; ch < PreviousParentObj.childCount; ch++)
							{
								
									PreviousParentObj.GetChild(ch).GetComponent<MeshRenderer>().material = InvisibleMaterial;
                               
							}
						}

						hit.transform.GetComponent<MeshRenderer>().material = SelectedMaterial;
						for (int ch = 0; ch < SelectedParentObj.childCount; ch++)
						{
							SelectedParentObj.GetChild(ch).GetComponent<MeshRenderer>().material = SelectedMaterial;
						}


						ShowCountryInfo(hit.transform.GetComponent<CountryInfo>());
						CountryInfoPanel.SetActive(true);
						PreviousSelectedObj = hit.transform.gameObject;
						GiveMeAQuestion.settingPrevious(PreviousSelectedObj);

					}
					else
                    {
						if (PreviousSelectedObj != null)
						{
							Transform PreviousParentObj = PreviousSelectedObj.transform.parent;

								PreviousSelectedObj.GetComponent<MeshRenderer>().material = InvisibleMaterial;
							for (int ch = 0; ch < PreviousParentObj.childCount; ch++)
							{
								PreviousParentObj.GetChild(ch).GetComponent<MeshRenderer>().material = InvisibleMaterial;
							}

							CountryInfoPanel.SetActive(false);
						}
					}
				}
				else
                {
					if (PreviousSelectedObj != null)
					{
						Transform PreviousParentObj = PreviousSelectedObj.transform.parent;
						PreviousSelectedObj.GetComponent<MeshRenderer>().material = InvisibleMaterial;
						for (int ch = 0; ch < PreviousParentObj.childCount; ch++)
						{
							PreviousParentObj.GetChild(ch).GetComponent<MeshRenderer>().material = InvisibleMaterial;
						}

						CountryInfoPanel.SetActive(false);
					}
				}
				//------------------
			}
            #endregion

            #region MapMoving
            if (Input.GetMouseButtonDown(1))
			{
				startPointForMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			}

			if (Input.GetMouseButton(1))
			{
				mousePosForMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
				moveVector = mousePosForMove - startPointForMove;

				if (moveVector.x > 0 && Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).x <= 0)
				{
					if (-Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).x < moveVector.x)
						moveVector.x = -Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).x;
					transform.localPosition -= new Vector3(moveVector.x * (gameObject.GetComponent<Camera>().fieldOfView / 65f), 0, 0);
				}
				if (moveVector.x < 0 && Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).x >= Screen.width)
				{
					if (Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).x-Screen.width < -moveVector.x)
						moveVector.x = -Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).x + Screen.width;
					transform.localPosition -= new Vector3(moveVector.x * (gameObject.GetComponent<Camera>().fieldOfView / 65f), 0, 0);
				}
				if (moveVector.y > 0 && Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).y <= 0)
				{
					if (-Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).y < moveVector.y)
						moveVector.y = -Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).y;
					transform.localPosition -= new Vector3(0, moveVector.y * (gameObject.GetComponent<Camera>().fieldOfView / 65f), 0);
				}
				if (moveVector.y < 0 && Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).y >= Screen.height)
				{
					if (Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).y - Screen.height < -moveVector.y)
						moveVector.y = -Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).y + Screen.height;
					transform.localPosition -= new Vector3(0, moveVector.y * (gameObject.GetComponent<Camera>().fieldOfView / 65f), 0);
				}


				//if (Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.max).x < Screen.width && Camera.main.WorldToScreenPoint(EarthMapBuilder.instance.MapBack.GetComponent<BoxCollider>().bounds.min).x > 0)
				//	transform.localPosition -= new Vector3(moveVector.x * (gameObject.GetComponent<Camera>().fieldOfView/65f), moveVector.y*(gameObject.GetComponent<Camera>().fieldOfView / 65f), 0);

				startPointForMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0); ;
			}
            #endregion
        }
    }

	// function that is responsible in displaying the mini country dialog with the information
    void ShowCountryInfo(CountryInfo info)
    {
		CountryName.text = info.transform.name;
		if (Models.Factories.EarthCountryFactory.capitals.ContainsKey(info.transform.name))
			Sovereignt.text = Models.Factories.EarthCountryFactory.capitals[info.transform.name];
		else
			Sovereignt.text = "";
		Population.text = info.Population;
		if (info.Economy.Contains("nonG7"))
		{
			var s = info.Economy.Replace("nonG7", "G7");
			info.Economy = s;
		}
		Economy.text = info.Economy;
		Continent.text = info.Continent;
		Region.text = info.Region;
		Subregion.text = info.Subregion;


		// show image of the flag : 
		setFlag(info.transform.name);



		if (mode.Equals("PlayMode")) // if we are in play mode, we want to check if the country we clicked if the country we asked the user to find
		{
			checkCorrectCountry(info.transform.name); // we send the country we clicked
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
					//Debug.Log(pages[page] + ": Error: " + webRequest.error);
					flagImage.GetComponent<Image>().sprite = null;
					break;
				case UnityWebRequest.Result.ProtocolError:
					//Debug.Log(pages[page] + ": HTTP Error: " + webRequest.error);
					flagImage.GetComponent<Image>().sprite = null;
					break;
				case UnityWebRequest.Result.Success:
					//Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
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
			// requesting the image url :
			/*			WebRequest web = WebRequest.Create(API_URL);

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
						JArray json =(JArray)(jsonData);
						foreach(JObject elem in json){
							JObject flags = (JObject)(elem.GetValue("flags"));
							if(flags!=null)
								url = flags.GetValue("png").ToString();
						}
							StartCoroutine(setDownloadImage(url));*/ // starting coroutine in order to display the image from the url we got
		}
		catch(System.Exception e) // exception -> setting image to default white
        {
			flagImage.GetComponent<Image>().sprite = null;
		}
	}
	public IEnumerator setDownloadImage(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		flagImage.GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
	}

	public void checkCorrectCountry(string name)
    {
		
		if (name.Equals(displayCountry.text) )
		{
			
			displayHint.gameObject.SetActive(false); // hiding the hint
			Mover.setDance(true); // setting dance to true
			
			player.GetComponent<Score>().resetGuess(name); // reseting score because we found the country
		}
		else
		{
			
			Mover.setDance(false);
			if(!displayCountry.text.Equals("Country")) // only if we started the game
				player.GetComponent<Score>().setGuesses();
		}
	}

	public static void setInvisible(GameObject prev)
    {
		PreviousSelectedObj = prev;

	}
}
