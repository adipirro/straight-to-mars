using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	int i;
	// Use this for initialization
	void Start () {
		i = 0;
		Debug.Log ("Hello");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(1,0,0) * 0);
	}
}
