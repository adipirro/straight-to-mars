using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {

	GameObject[,] quadPlane;

	int maxTileRow = 64;
	int maxTileCol = 32;

	// Use this for initialization
	void Start () {
		quadPlane = new GameObject[maxTileRow, maxTileCol];
		instantiateQuadPlane (10, 20);
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
