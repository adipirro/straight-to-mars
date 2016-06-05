using UnityEngine;
using System.Collections;

public class QuadPlaneScript : MonoBehaviour {


	public GameObject[,] plane;
	int maxX = 2; // Divide planes into sub objects of these sizes
	int maxY = 2;

	void Start () {
	}

	public void Init(Texture2D mainTexture, Texture2D heightTexture) {
		plane = new GameObject[maxX, maxY];
		createPlanes (mainTexture, heightTexture);
	}


	void createPlanes(Texture2D mainTexture, Texture2D heightTexture) {
		int chunkX = (heightTexture.width / maxX); // Size of a plane chunk
		int chunkY = (heightTexture.height / maxY);
		int startX = 0;
		int startY = 0;
		int endX = maxX;
		int endY = maxY;

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
