using UnityEngine;
using System.Collections;

public class QuadPlaneScript : MonoBehaviour {

	public static string mainTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_Viking_MDIM21_ClrMosaic_global_232m/1.0.0//default/default028mm/{TileZoom}/{TileRow}/{TileCol}.png";
	public static string heightTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/{TileZoom}/{TileRow}/{TileCol}.png";

	public GameObject[,] plane;
	int maxX = 2; // Divide planes into sub objects of these sizes
	int maxY = 2;
	int zoom = 5;

	void Start () {
		plane = new GameObject[maxX, maxY];
	}

	public void Init(int row, int col) {
		Debug.Log (row);
		Debug.Log (col);
		StartCoroutine(loadResources (row, col));
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
		createPlanes (apiMain.texture, apiHeight.texture);
	}

	void createPlanes(Texture2D mainTexture, Texture2D heightTexture) {
		int chunkX = (heightTexture.width / maxX); // Size of a plane chunk
		int chunkY = (heightTexture.height / maxY);
		int startX = 0;
		int startY = 0;
		int endX = maxX;
		int endY = maxY;
//		int endX = 1;
//		int endY = 1;

		for (int x = startX; x < endX; x++) {
			for (int y = startX; y < endY; y++) {
				plane[x, y] = (GameObject)Instantiate (Resources.Load ("Prefabs/Plane"),
					new Vector3(x*chunkX, 0, y*chunkY), Quaternion.identity);
				plane[x, y].GetComponent<PlaneScript> ().Init (this.gameObject, mainTexture, heightTexture,
					chunkX*x, chunkY*y, chunkX*(x+1), chunkY*(y+1));
			}
		}
	}
}
