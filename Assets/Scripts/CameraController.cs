using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform targetTransform;
    public Camera frontCamera;
    public Camera backCamera;
	public float smoothSpeed = .5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

	void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, targetTransform.position, smoothSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetTransform.eulerAngles.y, transform.eulerAngles.z);
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            backCamera.gameObject.SetActive(true);
            frontCamera.gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            backCamera.gameObject.SetActive(false);
            frontCamera.gameObject.SetActive(true);
        }
    }
}
