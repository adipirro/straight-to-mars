using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {

	GameObject[,] quadPlane;

	 int maxTileRow = 32;
	 int maxTileCol = 64;

	// Use this for initialization
	void Start () {
		quadPlane = new GameObject[maxTileRow, maxTileCol];
		 instantiateQuadPlane (12, 8); // Mount Olympus
		// instantiateQuadPlane (22, 44); // Martian Cocaine
		// instantiateQuadPlane (16, 24); // Canyon

		var MattDamon = Instantiate (Resources.Load ("Prefabs/Character/Matt Damon")) as GameObject;
		MattDamon.transform.position = new Vector3 (128, 128, 128);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void instantiateQuadPlane(int row, int col) {
		if (quadPlane[row,col] == null) {
			quadPlane[row, col] = (GameObject) Instantiate (Resources.Load("Prefabs/QuadPlane"));
			quadPlane[row, col].GetComponent<QuadPlaneScript> ().Init (row, col);
		}
	}
}
