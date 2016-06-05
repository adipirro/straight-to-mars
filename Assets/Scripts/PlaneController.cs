using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public static string mainTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_Viking_MDIM21_ClrMosaic_global_232m/1.0.0//default/default028mm/{TileZoom}/{TileRow}/{TileCol}.png";
	public static string heightTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/{TileZoom}/{TileRow}/{TileCol}.png";
	int zoom = 5;

	GameObject[,] quadPlane;

	 int maxTileRow = 32;
	 int maxTileCol = 64;

	// Use this for initialization
	void Start () {
		quadPlane = new GameObject[maxTileRow, maxTileCol];
		// instantiateQuadPlane (12, 8); // Mount Olympus
		// instantiateQuadPlane (22, 44); // Martian Cocaine
		// instantiateQuadPlane (16, 24); // Canyon
		teleport(12, 8);

		var MattDamon = Instantiate (Resources.Load ("Prefabs/Character/Matt Damon")) as GameObject;
        MattDamon.transform.position = new Vector3(128, 130, 128);
        var baseCamp = Instantiate(Resources.Load("Prefabs/BuildingShed")) as GameObject;
        baseCamp.transform.position = new Vector3(150, 26, 150);
    }

	int counter = 0;
    	
    
	// Update is called once per frame
	void Update () {
//		counter++;
//		if (counter == 100)
//			teleport (22, 44);
	}

	public void instantiateQuadPlane(int row, int col, Texture2D mainTexture, Texture2D heightTexture) {
		if (quadPlane[row,col] == null) {
			quadPlane[row, col] = (GameObject) Instantiate (Resources.Load("Prefabs/QuadPlane"));
			quadPlane[row, col].GetComponent<QuadPlaneScript> ().Init (mainTexture, heightTexture);
		}
	}

	public void teleport(int row, int col) {
		destroyWorld ();
		StartCoroutine(loadResources (row, col));
	}

	void destroyWorld() {
		for (int a = 0; a < maxTileRow; a++) {
			for (int b = 0; b < maxTileCol; b++) {
				Destroy (quadPlane [a, b]);
			}
		}

	}

	IEnumerator loadResources(int row, int col) {
		// Create calls
		string mCall = (string) mainTextCall.Clone();
		mCall = mCall.Replace("{TileZoom}", zoom.ToString());
		mCall = mCall.Replace ("{TileRow}", row.ToString());
		mCall = mCall.Replace ("{TileCol}", col.ToString());
		string hCall = (string) heightTextCall.Clone();
		hCall = hCall.Replace("{TileZoom}", zoom.ToString());
		hCall = hCall.Replace ("{TileRow}", row.ToString());
		hCall = hCall.Replace ("{TileCol}", col.ToString());

		Debug.Log ("Hey" + hCall);
		// Make Calls
		WWW apiMain = new WWW(mCall);
		yield return apiMain;

		WWW apiHeight = new WWW (hCall);
		yield return apiHeight;

		// Create Planes
		instantiateQuadPlane(row, col, apiMain.texture, apiHeight.texture);
	}

}
