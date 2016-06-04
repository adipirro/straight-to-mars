using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneScript : MonoBehaviour {

	private Texture2D mainTexture;
	private Texture2D heightTexture;
	private int startX; // Position in the texture to start
	private int startY; 
	private int endX; // Position in the texture to end
	private int endY;

	private int sizeX;
	private int sizeY;

	private static int verticalScale = 10;

	private MeshRenderer render;
	private MeshFilter filter;

	// Init rather than a constructor call because of instantiation issues
	public void Init(Texture2D mainTexture, Texture2D heightTexture, int startX, int startY, int endX, int endY) {
		this.mainTexture = mainTexture;
		this.heightTexture = heightTexture;
		this.startX = startX;
		this.startY = startY;
		this.endX = endX;
		this.endY = endY;

		sizeX = endX - startX;
		sizeY = endY - startY;

		render = gameObject.GetComponent<MeshRenderer>();
		filter = gameObject.GetComponent<MeshFilter>();

		createMesh ();
		createMainTex ();
	}

	void createMesh() {
		Debug.Log ("Loading textures"); 
		List<Vector3> verts = new List<Vector3>();
		List<int> tris = new List<int>();

		//Bottom left section of the map, other sections are similar
		for(int i = 0; i < sizeX; i++)
		{
			for(int j = 0; j < sizeY; j++)
			{
				//Add each new vertex in the plane
				verts.Add(new Vector3(i, heightTexture.GetPixel(startX + i, startY + j).grayscale * verticalScale, j));
				//Skip if a new square on the plane hasn't been formed
				if (i == 0 || j == 0) continue;
				//Adds the index of the three vertices in order to make up each of the two tris
				tris.Add(sizeX * i + j); //Top right
				tris.Add(sizeX * i + j - 1); //Bottom right
				tris.Add(sizeX * (i - 1) + j - 1); //Bottom left - First triangle
				tris.Add(sizeX * (i - 1) + j - 1); //Bottom left 
				tris.Add(sizeX * (i - 1) + j); //Top left
				tris.Add(sizeX * i + j); //Top right - Second triangle
			}
		}

		Vector2[] uvs = new Vector2[verts.Count];
		for (var i = 0; i < uvs.Length; i++) //Give UV coords X,Z world coords
			uvs[i] = new Vector2(verts[i].x, verts[i].z);

		Mesh procMesh = new Mesh();
		procMesh.vertices = verts.ToArray(); //Assign verts, uvs, and tris to the mesh
		procMesh.uv = uvs;
		procMesh.triangles = tris.ToArray();
		procMesh.RecalculateNormals(); //Determines which way the triangles are facing
		filter.mesh = procMesh; //Assign Mesh object to MeshFilter	}
	}

	void createMainTex () {
		render.material.SetTexture("_MainTex", mainTexture);
//		render.material.SetTextureScale ("_MainTex", new Vector2 (1 / (float) sizeX, 1 / (float) sizeY));
		render.material.SetTextureScale ("_MainTex", new Vector2 (1 / (float) heightTexture.width, 1 / (float) heightTexture.height));
		render.material.SetTextureOffset("_MainTex", new Vector2(startX, startY));
	}

}
