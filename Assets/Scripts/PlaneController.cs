using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject quadPlane = (GameObject) Instantiate (Resources.Load("Prefabs/QuadPlane"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
