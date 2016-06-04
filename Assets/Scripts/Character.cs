using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 10F;

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
    }
}