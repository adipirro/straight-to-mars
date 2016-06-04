using UnityEngine;
using System.Collections;

public class WebPane : MonoBehaviour {

	// Use this for initialization
	IEnumerator  Start () {
		Debug.Log ("Started Webpane");
		WWW www = new WWW ("https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/0/0/0.png");
		yield return www;

		Renderer render = GetComponent<Renderer>();
		GetComponent<Renderer>().material.mainTexture = www.texture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
