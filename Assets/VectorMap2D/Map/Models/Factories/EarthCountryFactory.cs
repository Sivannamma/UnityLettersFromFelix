using System;
using System.Collections.Generic;
using Helpers;
using Models.Factories;
using UnityEngine;
using TriangleNet;
using TriangleNet.Geometry;
using System.Linq;

namespace Models.Factories
{
    public class EarthCountryFactory : MonoBehaviour
    {
        public static EarthCountryFactory instance;
        public static Dictionary<string, string> capitals = new Dictionary<string, string>(); // key : country, value : capital

        public  string XmlTag { get { return "Earth_countries"; } }
        public virtual Func<JSONObject, bool> Query { get; set; }

        public GameObject Earth;
        public Material Frontiers;
        public Transform GeometryTransform, LabelsTransform;
        public TextMesh MapLabel;
        public void Awake()
        {
            instance = this;
            Query = (geo) => (geo["geometry"]["type"].str == "Polygon" || geo["geometry"]["type"].str == "MultiPolygon" || geo["geometry"]["type"].str == "LineString" || geo["geometry"]["type"].str == "MultiLineString");
        }

        public void Create(JSONObject geo)
        {
            if ((geo["geometry"]["type"].str == "Polygon" || geo["geometry"]["type"].str == "MultiPolygon"))
            {
                foreach (var bb in geo["geometry"]["coordinates"].list)
                {
                    JSONObject jo = null;
                    if (bb.list == null)
                    {
                        //print("-1");
                        jo = bb;
                    }
                    else if (bb.list.Count == 0)
                    {
                        //print("-1.0");
                        jo = bb;
                    }
                    else if (bb.list[0].list == null)
                    {
                        //print("-2");
                        jo = bb;
                    }
                    else if (bb.list[0].list.Count == 0)
                    {
                        //print("-2.0");
                        jo = bb;
                    }
                    else jo = (bb.list[0].list[0].IsArray) ? bb.list[0] : bb;

                    if (jo.list == null) continue;

                    var count = jo.list.Count; //-1

                    if (count < 3)
                        continue;

                    List<Vector3> earthBoundarEnds = new List<Vector3>();
                    earthBoundarEnds.Clear();

                    bool lonSign = false;
                    int plus = 0, minus = 0;

                    for (int i = 0; i < count; i++)
                    {
                        var c = jo.list[i];
                        if (c[0].f > 0)
                        {
                            plus++;
                            //lonSign = true;
                        }
                        else
                            minus++;
                        var dotMerc = GeoConvert.LatLonToMetersForEarth(c[1].f, c[0].f);
                        dotMerc += new Vector3d(Earth.transform.position.x, Earth.transform.position.y, Earth.transform.position.z);
                        earthBoundarEnds.Add((Vector3)dotMerc);
                    }

                    var earthMeshBoundary = new GameObject("mesh").AddComponent<MeshFilter>();
                    earthMeshBoundary.gameObject.AddComponent<MeshRenderer>();
                    var earthMesh = earthMeshBoundary.mesh; //earthLineBoundary 

                    earthMesh.SetVertices(earthBoundarEnds);
                    earthMesh.SetIndices((new List<int>(System.Linq.Enumerable.Range(0, earthBoundarEnds.Count))).ToArray(), MeshTopology.LineStrip, 0);

                    if (geo["properties"]["id"] != null)
                        earthMeshBoundary.name = geo["properties"]["id"].ToString();
                    if (geo["properties"]["name"] != null)
                        earthMeshBoundary.name = geo["properties"]["name"].ToString();

                    earthMeshBoundary.GetComponent<MeshRenderer>().material = Frontiers;
                    earthMeshBoundary.transform.parent = GeometryTransform;//Earth.transform;


                    //--------------------------------------------------------------------
                    #region CountryMeshes
                    {
                        var earthMeshBoundary2 = new GameObject("mesh").AddComponent<MeshFilter>();
                        earthMeshBoundary2.gameObject.AddComponent<MeshRenderer>();
                        var earthMesh2 = earthMeshBoundary2.mesh; //earthLineBoundary 
                        var points = earthBoundarEnds;
                        var md2 = new MeshData();
                        var inp = new InputGeometry(points.Count);
                        for (int d = 0; d < points.Count; d++)
                        {
                            inp.AddPoint(points[d].x, points[d].y);
                            inp.AddSegment(d, (d + 1) % points.Count);
                        }

                        if (plus > minus)
                            lonSign = true;
                        else
                            lonSign = false;

                        if (earthMeshBoundary.transform.name.Contains("Antarctica"))
                        {
                            lonSign = true;
                        }
                        CreateMeshForPolygon(inp, md2, points, /*lonSign*/false);

                        earthMesh2.vertices = md2.Vertices.ToArray();
                        earthMesh2.triangles = md2.Indices.ToArray();
                        earthMesh2.SetUVs(0, md2.UV);
                        earthMesh2.RecalculateNormals();
                        earthMeshBoundary2.GetComponent<MeshRenderer>().material = InvisibleMat;
                        earthMeshBoundary2.transform.name = earthMeshBoundary.transform.name;
                        
                        bool containsLayer = false;
                        GameObject parentLayer = null;
                        for (int child = 0; child < GeometryForMeshes.childCount; child++)
                        {
                            if (GeometryForMeshes.GetChild(child).name.Contains(earthMeshBoundary2.transform.name + " layer"))
                            {
                                containsLayer = true;
                                parentLayer = GeometryForMeshes.GetChild(child).gameObject;
                            }
                        }

                        if (containsLayer)
                        {
                            earthMeshBoundary2.transform.parent = parentLayer.transform;
                        }
                        else
                        {
                            parentLayer = new GameObject(earthMeshBoundary2.transform.name + " layer");
                            parentLayer.transform.parent = GeometryForMeshes;
                            earthMeshBoundary2.transform.parent = parentLayer.transform;
                        }
                        
                        var info = earthMeshBoundary2.gameObject.AddComponent<CountryInfo>();
              
                        earthMeshBoundary2.gameObject.AddComponent<MeshCollider>();

                        if (geo["properties"]["capital"] != null)
                            info.Sovereignt = geo["properties"]["capital"].str;
                        if (geo["properties"]["pop_est"] != null)
                            info.Population = geo["properties"]["pop_est"].ToString();
                        if (geo["properties"]["economy"] != null)
                            info.Economy = geo["properties"]["economy"].str;
                        if (geo["properties"]["continent"] != null)
                            info.Continent = geo["properties"]["continent"].str;
                        if (geo["properties"]["region_wb"] != null)
                            info.Region = geo["properties"]["region_wb"].str;
                        if (geo["properties"]["subregion"] != null)
                            info.Subregion = geo["properties"]["subregion"].str;
                    }
                    #endregion
                }
            }
        }

        public Transform GeometryForMeshes;
        public Material InvisibleMat;

        public void CreatePoints(JSONObject geo)
        {
            if (geo["area"].f > 150000f)
            {
                List<Vector3> earthBoundarEnds = new List<Vector3>();
                earthBoundarEnds.Clear();
                var name = geo["name"]["common"].str;
                capitals.Add("\""+ name + "\"", (geo["capital"]+"").Substring(2, (geo["capital"] + "").Length-4));
                name = System.Text.RegularExpressions.Regex.Unescape(name);
                var c = geo["latlng"].list;
                var dotMerc = GeoConvert.LatLonToMetersForEarth(c[0].f, c[1].f);
                earthBoundarEnds.Add((Vector3)dotMerc);
                var label = GameObject.Instantiate(MapLabel);
                label.transform.parent = LabelsTransform;
                if (!name.Contains("Antarctica"))
                    label.transform.position = (Vector3)(dotMerc);
                else
                    label.transform.position =new Vector3((float)dotMerc.x, (float)dotMerc.y - (float)dotMerc.y/10, 0);
                label.text = name;
               // label.fontSize += (int)EarthMapBuilder.instance.TextSlider.value - (int)EarthMapBuilder.instance.previousSize;
                if (name.Contains("of the Congo") || name.Contains("Norway") || name.Contains("Ivory Coast"))
                    label.anchor = TextAnchor.UpperCenter;
            }
            else
            {
                var name = geo["name"]["common"].str;
                if(geo["capital"] + "" != "[]")
                capitals.Add("\"" + name + "\"", (geo["capital"] + "").Substring(2, (geo["capital"] + "").Length - 4));
                else
                    capitals.Add("\"" + name + "\"", "");
            }
        }

        private void CreateMeshForPolygon(InputGeometry corners, MeshData meshdata, List<Vector3> ends, bool temp)
        {
            var mesh = new TriangleNet.Mesh();
            mesh.Behavior.Algorithm = TriangulationAlgorithm.SweepLine;
            mesh.Behavior.Quality = true;
            mesh.Triangulate(corners);
            var vertsStartCount = meshdata.Vertices.Count;
            meshdata.Vertices = ends;/*AddRange(corners.Points.Select(x => new Vector3((float)x.X, (float)x.Y, 0)).ToList());*/
            foreach (var tri in mesh.Triangles)
            {
                if ((tri.P0 >= corners.Count) || (tri.P1 >= corners.Count) || (tri.P2 >= corners.Count)) continue;

                if (!temp)
                {
                    meshdata.Indices.Add(vertsStartCount + tri.P1);//1
                    meshdata.Indices.Add(vertsStartCount + tri.P0);//0
                    meshdata.Indices.Add(vertsStartCount + tri.P2);//2
                }
                else
                {
                    meshdata.Indices.Add(vertsStartCount + tri.P0);//1
                    meshdata.Indices.Add(vertsStartCount + tri.P1);//0
                    meshdata.Indices.Add(vertsStartCount + tri.P2);//2
                }
            }
        }

    }
}
