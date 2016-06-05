using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 10F;
	public float doubleClickTime = 0.34f;

	bool singleClick;
	float clickTime=0f;
    void Start()
    {
        GvrViewer.Instance.OnBackButton += Application.Quit;
    }

        void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position += Camera.main.transform.forward * moveSpeed * Time.deltaTime;
        }

		if (Input.GetMouseButtonDown (0)) {
			if ((Time.time - clickTime) < doubleClickTime) {
				Application.LoadLevel (0);
			}
			//mark time that this happened
			clickTime=Time.time;
		}
    }
}