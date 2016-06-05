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

	int counter = 0;
	// Update is called once per frame
	void Update () {
//		counter++;
//		if (counter == 100)
//			teleport (22, 44);
	}

	public void instantiateQuadPlane(int row, int col) {
		if (quadPlane[row,col] == null) {
			quadPlane[row, col] = (GameObject) Instantiate (Resources.Load("Prefabs/QuadPlane"));
			quadPlane[row, col].GetComponent<QuadPlaneScript> ().Init (row, col);
		}
	}

	public void teleport(int row, int col) {
		destroyWorld ();
		instantiateQuadPlane (row, col);
	}

	void destroyWorld() {
		for (int a = 0; a < maxTileRow; a++) {
			for (int b = 0; b < maxTileCol; b++) {
				Destroy (quadPlane [a, b]);
			}
		}

	}
}
