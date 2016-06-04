using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebPane : MonoBehaviour {

	public Texture2D heightTexture;
	public Texture2D mainTexture;

	public int startx;
	public int starty;
	public int endx;
	public int endy;

	// Use this for initialization
	IEnumerator  Start () {
		Debug.Log ("Started Webpane");
		WWW apiHeight = new WWW ("https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/0/0/0.png");
		yield return apiHeight;
		WWW apiMainText = new WWW ("https://api.nasa.gov/mars-wmts/catalog/Mars_Viking_MDIM21_ClrMosaic_global_232m/1.0.0//default/default028mm/0/0/0.png");
		yield return apiMainText;

		heightTexture = apiHeight.texture;
		mainTexture = apiMainText.texture;
		applyHeightMap ();
	}

	// This creates an object, but runs into verice limits, we need to chunk
	// the texture before we try to create objects to render
	void applyHeightMap() {
		List<Vector3> verts = new List<Vector3>();
		List<int> tris = new List<int>();

		int horizontal_offset = 255;
		int vertical_offset = 10;

		//Bottom left section of the map, other sections are similar
		for(int i = 0; i < horizontal_offset; i++)
		{
			for(int j = 0; j < horizontal_offset; j++)
			{
				//Add each new vertex in the plane
				verts.Add(new Vector3(i, heightTexture.GetPixel(i,j).grayscale * vertical_offset, j));
				//Skip if a new square on the plane hasn't been formed
				if (i == 0 || j == 0) continue;
				//Adds the index of the three vertices in order to make up each of the two tris
				tris.Add(horizontal_offset * i + j); //Top right
				tris.Add(horizontal_offset * i + j - 1); //Bottom right
				tris.Add(horizontal_offset * (i - 1) + j - 1); //Bottom left - First triangle
				tris.Add(horizontal_offset * (i - 1) + j - 1); //Bottom left 
				tris.Add(horizontal_offset * (i - 1) + j); //Top left
				tris.Add(horizontal_offset * i + j); //Top right - Second triangle
			}
		}

		Vector2[] uvs = new Vector2[verts.Count];
		for (var i = 0; i < uvs.Length; i++) //Give UV coords X,Z world coords
			uvs[i] = new Vector2(verts[i].x, verts[i].z);

		gameObject.AddComponent<MeshFilter>();
		MeshRenderer render = gameObject.GetComponent<MeshRenderer>();
		Mesh procMesh = new Mesh();
		procMesh.vertices = verts.ToArray(); //Assign verts, uvs, and tris to the mesh
		procMesh.uv = uvs;
		procMesh.triangles = tris.ToArray();
		procMesh.RecalculateNormals(); //Determines which way the triangles are facing
		GetComponent<MeshFilter>().mesh = procMesh; //Assign Mesh object to MeshFilter	}
		render.material.SetTexture("_MainTex", mainTexture);
		render.material.SetTextureScale ("_MainTex", new Vector2 (1 / (float) horizontal_offset, 1 / (float) horizontal_offset));
	}

	// Update is called once per frame
	void Update () {
	
	}
}
