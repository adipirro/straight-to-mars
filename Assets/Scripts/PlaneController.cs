using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject plane = (GameObject) Instantiate (Resources.Load("Prefabs/Plane"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
