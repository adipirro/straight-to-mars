using UnityEngine;
using System.Collections;

public class DoubleClick : MonoBehaviour {

	public float clickDelta = 0.35f;  // Max between two click to be considered a double click

	private bool click = false;
	private float clickTime;

	void Update() {
		if (click && Time.time > (clickTime + clickDelta)) {
			transform.Rotate(new Vector3(0, 180, 0));  // Single click
			click = false;
		}
	}

	void OnMouseDown() {
		if (click && Time.time <= (clickTime + clickDelta)) {
			transform.Rotate (new Vector3(180,0,0)); // Double click
			click = false;
		}
		else {
			click = true;
			clickTime = Time.time;
		}
	}
}
