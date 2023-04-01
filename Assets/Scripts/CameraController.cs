using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform playerTransform;
	public float smoothSpeed = .5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

	void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, playerTransform.position, smoothSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerTransform.eulerAngles.y, transform.eulerAngles.z);
	}
}
