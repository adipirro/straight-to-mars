using UnityEngine;
using System.Collections;

public class QuadPlaneScript : MonoBehaviour {

	public static string mainTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_Viking_MDIM21_ClrMosaic_global_232m/1.0.0//default/default028mm/0/{TileRow}/{TileCol}.png";
	public static string heightTextCall = "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/0/{TileRow}/{TileCol}.png";

	public GameObject plane;

	void Start () {
		Debug.Log ("Started Webpane");

		StartCoroutine(loadResources (0, 0));
	}

	IEnumerator loadResources(int row, int col) {
		// Create calls
		string mCall = (string) mainTextCall.Clone();
		mCall = mCall.Replace ("{TileRow}", row.ToString());
		mCall = mCall.Replace ("{TileCol}", col.ToString());
		string hCall = (string) heightTextCall.Clone();
		hCall = hCall.Replace ("{TileRow}", row.ToString());
		hCall = hCall.Replace ("{TileCol}", col.ToString());

		// Make Calls
		WWW apiMain = new WWW(mCall);
		yield return apiMain;

		WWW apiHeight = new WWW (hCall);
		yield return apiHeight;

		// Create Planes
		createPlanes (apiMain.texture, apiHeight.texture);
	}

	void createPlanes(Texture2D mainTexture, Texture2D heightTexture) {
		plane = (GameObject) Instantiate (Resources.Load("Prefabs/Plane"));
		plane.GetComponent<PlaneScript> ().Init (mainTexture, heightTexture, 128, 128, 256, 256);
	}
}
