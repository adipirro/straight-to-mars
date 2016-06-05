using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneScript : MonoBehaviour {

	private GameObject quadPlane;

	private Texture2D mainTexture;
	private Texture2D heightTexture;
	private int startX; // Position in the texture to start
	private int startY; 
	private int endX; // Position in the texture to end
	private int endY;

	private int sizeX;
	private int sizeY;

	private static int verticalScale = 100;

	private MeshRenderer render;
	private MeshFilter filter;

	// Init rather than a constructor call because of instantiation issues
	public void Init(GameObject quadPlane, Texture2D mainTexture, Texture2D heightTexture, int startX, int startY, int endX, int endY) {
		this.quadPlane = quadPlane;
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

		gameObject.transform.parent = quadPlane.transform;
		Debug.Log(gameObject);

		createMesh ();
		createMainTex ();
		shiftPosition ();
	}

	void createMesh() {
		List<Vector3> verts = new List<Vector3>();
		List<int> tris = new List<int>();

		for(int i = 0; i <= sizeX; i++)
		{
			for(int j = 0; j <= sizeY; j++)
			{
				// Creates bounding vertices
				verts.Add(new Vector3(i, (heightTexture.GetPixel(
					(int) Mathf.Clamp(startX + i, 0.0F, heightTexture.width - 1),
					(int) Mathf.Clamp(startY + j, 0.0F, heightTexture.height - 1)).grayscale * verticalScale), j));

				// We need an extra column to define edges of box, so we need to skip one row
				if ((i == 0) || (j == 0)) continue;

				tris.Add((sizeX + 1) * i + j); //Top right
				tris.Add((sizeX + 1) * i + j - 1); //Bottom right
				tris.Add((sizeX + 1) * (i - 1) + j - 1); //Bottom left - First triangle

				tris.Add((sizeX + 1) * (i - 1) + j - 1); //Bottom left 
				tris.Add((sizeX + 1) * (i - 1) + j); //Top left
				tris.Add((sizeX + 1) * i + j); //Top right - Second triangle


			}
		}
		Vector2[] uvs = new Vector2[verts.Count];
		for (var i = 0; i < uvs.Length; i++) //Give UV coords X,Z world coords
			uvs[i] = new Vector2(verts[i].x + startX, verts[i].z + startY);

		Mesh procMesh = new Mesh();
		procMesh.vertices = verts.ToArray(); //Assign verts, uvs, and tris to the mesh
		procMesh.uv = uvs;
		procMesh.triangles = tris.ToArray();
		procMesh.RecalculateNormals(); //Determines which way the triangles are facing
		filter.mesh = procMesh; //Assign Mesh object to MeshFilter
		gameObject.AddComponent<MeshCollider>();
	}

	void createMainTex () {
		render.material.SetTexture("_MainTex", mainTexture);
		render.material.SetTextureScale ("_MainTex", new Vector2 (1f / (float) heightTexture.width, 1f / (float) heightTexture.height));
		render.material.SetTextureOffset("_MainTex", new Vector2(startX, startY));
	}

	void shiftPosition () {
	}
}
