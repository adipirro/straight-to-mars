using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public static string mainTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_Viking_MDIM21_ClrMosaic_global_232m/1.0.0//default/default028mm/{TileZoom}/{TileRow}/{TileCol}.png";
	public static string heightTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/{TileZoom}/{TileRow}/{TileCol}.png";
	int zoom = 5;

	GameObject[,] quadPlane; // Destroy this
	Texture2D[,] mainTextures; // Keep these
	Texture2D[,] heightTextures;

	int maxTileRow = 32;
	int maxTileCol = 64;

	int tileSizeX = 256;
	int tileSizeY = 256;

	int loaded = 0;

	// Use this for initialization
	void Start () {
		quadPlane = new GameObject[maxTileRow, maxTileCol];
		mainTextures = new Texture2D[maxTileRow, maxTileCol];
		heightTextures = new Texture2D[maxTileRow, maxTileCol];
		// instantiateQuadPlane (12, 8); // Mount Olympus
		// instantiateQuadPlane (22, 44); // Martian Cocaine
		// instantiateQuadPlane (16, 24); // Canyon
		teleport(12, 8);

    }

	int counter = 0;
    	
    
	// Update is called once per frame
	void Update () {
//		counter++;
//		if (counter == 100)
//			teleport (22, 44);
	}

	public GameObject instantiateQuadPlane(int row, int col, Texture2D mainTexture, Texture2D heightTexture) {
		quadPlane[row, col] = (GameObject) Instantiate (Resources.Load("Prefabs/QuadPlane"));
		quadPlane[row, col].GetComponent<QuadPlaneScript> ().Init (this.gameObject, mainTexture, heightTexture);
		quadPlane [row, col].transform.position = new Vector3 (col * tileSizeY, 0, -1 * row * tileSizeX); // Quick fix is to multiply by -1
		return quadPlane [row, col];
	}

	public void teleport(int row, int col) {
		destroyWorld ();
		StartCoroutine(loadResources (row, col));
		StartCoroutine(loadWorld (row, col));
	}

	void destroyWorld() {
		loaded = 0;
		for (int a = 0; a < maxTileRow; a++) {
			for (int b = 0; b < maxTileCol; b++) {
				Destroy (quadPlane [a, b]);
			}
		}
	}

	IEnumerator loadWorld(int row, int col) {
		while (loaded == 0) {
			Debug.Log ("Load world status " + loaded);
			yield return new WaitForSeconds (0.5f);
		}

		for (int a = row - 1; a <= row + 1; a++) {
			for (int b = col - 1; b <= col + 1; b++) {
				GameObject quadPlane = instantiateQuadPlane(a, b, mainTextures[a,b], heightTextures[a,b]);
			}
		}

		new Vector3 (col * tileSizeY, 0, -1 * row * tileSizeX);

		var MattDamon = Instantiate (Resources.Load ("Prefabs/Character/Matt Damon")) as GameObject;
		MattDamon.transform.position = new Vector3 (col * tileSizeY, 300, -1 * row * tileSizeX);
//        var baseCamp = Instantiate(Resources.Load("Prefabs/BuildingShed")) as GameObject;
//        baseCamp.transform.position = new Vector3(150, 26, 150);

		Debug.Log ("World Loaded");
	}

	IEnumerator loadResources(int row, int col) {
		for (int a = row - 1; a <= row + 1; a++) {
			for (int b = col - 1; b <= col + 1; b++) {
				// Create calls
				string mCall = (string) mainTextCall.Clone();
				mCall = mCall.Replace("{TileZoom}", zoom.ToString());
				mCall = mCall.Replace ("{TileRow}", a.ToString());
				mCall = mCall.Replace ("{TileCol}", b.ToString());
				string hCall = (string) heightTextCall.Clone();
				hCall = hCall.Replace("{TileZoom}", zoom.ToString());
				hCall = hCall.Replace ("{TileRow}", a.ToString());
				hCall = hCall.Replace ("{TileCol}", b.ToString());

				// Make Calls
				WWW apiMain = new WWW(mCall);
				yield return apiMain;

				WWW apiHeight = new WWW (hCall);
				yield return apiHeight;

				mainTextures [a, b] = apiMain.texture;
				heightTextures [a, b] = apiHeight.texture;
				Debug.Log("Loaded Resources" + a + ", " + b);
			}
		}

		// Create Planes
		loaded = 1;
	}

}
