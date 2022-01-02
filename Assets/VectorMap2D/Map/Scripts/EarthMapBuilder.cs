using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Models.Factories;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.UI;
public class EarthMapBuilder : MonoBehaviour
{
    public static EarthMapBuilder instance;
    #region HideInInspector
    [HideInInspector]
    public uint zoom;
    [HideInInspector]
    public float initialTextSize, previousSize;
    #endregion

    #region Public
    public GameObject Factories;
    public bool isInitialMapBuild = true;
    public Slider TextSlider;
    public RectTransform SettingsPanel, ColorPicker;
    public GameObject MapBack;
    public Toggle DayNightToggle;
    public Material DayMat, NightMat, TextMat;

    public TextAsset polygonsJson;
    public TextAsset labelsJson;

    #endregion

    #region Initialization

    public void InitVariables()
    {
        isInitialMapBuild = true;
        instance = this;
        zoom = 0;
      //  initialTextSize = TextSlider.value;
        previousSize = initialTextSize;
    }
    #endregion

    public void Awake()
    {
        InitVariables();
        CreateNewLevel(0);
        SetDayNight();
        isInitialMapBuild = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region MapBuilding
    public void CreateNewLevel(int zoomLevel)
    {
        double sz = Math.Pow(2, zoomLevel) - 1;
        int szl = (int)Math.Pow(2, zoomLevel);

        for (int i = (0); i < szl; i++)
        {
            for (int j = (0); j < szl; j++)
            {
                var v = new Vector2d(i, j);
                StartCoroutine(CreateTile(v, zoomLevel));
            }
        }
    }
    /*
        protected virtual IEnumerator CreateTile(Vector2d tileTms, int level)
        {
            if (File.Exists("Assets\\VectorMap2D\\MapData\\" + level + "_" + (uint)tileTms.x + "_" + (uint)tileTms.y + ".json"))
            {
                Debug.Log("File Exists");
                ConstructTile(string.Format("Assets\\VectorMap2D\\MapData\\{0}_{1}_{2}.json", level, (uint)tileTms.x, (uint)tileTms.y));
            }

            yield return null;
        }*/

    protected virtual IEnumerator CreateTile(Vector2d tileTms, int level)
    {
        if (polygonsJson != null)
        {
            ConstructTile();
        }

        yield return null;
    }


    /*    public async void ConstructTile(string text)
        {
            JSONObject mapData = *//*await*//* ReadDataFromJson(text);

            foreach (var entity in mapData["features"].list)
            {
                EarthCountryFactory.instance.Create(entity);
            }

            JSONObject mapData2 = *//*await*//* ReadDataFromJson("Assets/VectorMap2D/MapData/countries.json");

            foreach (var entity in mapData2["features"].list)
            {
                EarthCountryFactory.instance.CreatePoints(entity);
            }

        }*/

    public async void ConstructTile()
    {
        var text_ = polygonsJson.text;
        JSONObject mapData = ReadDataFromJson(text_);

        foreach (var entity in mapData["features"].list)
        {
            EarthCountryFactory.instance.Create(entity);
        }

        if (labelsJson != null)
        {
            var text2_ = labelsJson.text;
            JSONObject mapData2 = ReadDataFromJson(text2_);

            foreach (var entity in mapData2["features"].list)
            {
                EarthCountryFactory.instance.CreatePoints(entity);
            }
        }
    }


    /*    private static *//*asyncTask<*//*JSONObject*//*>*//* ReadDataFromJson(string text)
        {
            JSONObject temp = new JSONObject();
            //await System.Threading.Tasks.Task.Run(() =>
            //{
                temp = new JSONObject(File.ReadAllText(text));
            //});

            return temp;
        }*/



    private static /*asyncTask<*/JSONObject/*>*/ ReadDataFromJson(string text)
    {
        JSONObject temp = new JSONObject();
        //await System.Threading.Tasks.Task.Run(() =>
        //{
        temp = new JSONObject(text);
        //});

        return temp;
    }

    #endregion

    public static void SetPointsVisible()
    {
        EarthCountryFactory.instance.LabelsTransform.gameObject.SetActive(!EarthCountryFactory.instance.LabelsTransform.gameObject.activeSelf);
    }

    public void SetFontSize()
    {
        for (int i = 0; i < EarthCountryFactory.instance.LabelsTransform.childCount; i++)
        {
            EarthCountryFactory.instance.LabelsTransform.GetChild(i).GetComponent<TextMesh>().fontSize += (int)TextSlider.value - (int)previousSize;
        }
        previousSize = (int)TextSlider.value;
    }

    public void SetDayNight()
    {
        /*if (DayNightToggle.isOn)
        {
            MapBack.GetComponent<MeshRenderer>().material = DayMat;
            TextMat.color = new Color32(111,111,111,255);
        }
        else
        {
            MapBack.GetComponent<MeshRenderer>().material = NightMat;
            TextMat.color = Color.white;
        }*/
    }
}
