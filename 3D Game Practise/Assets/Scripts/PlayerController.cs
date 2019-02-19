using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float movementSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float maxRotationX = 100f;
    [SerializeField] private float minRotationX = -100f;
    [SerializeField] private Transform mainCamera;


    private Rigidbody playerRigidbody;
    private Vector3 movementVector = Vector3.zero;

    private float yRotation;
    private float xRotation;

    private float playerYRotation;

	void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        playerYRotation = transform.rotation.y;
	}

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();

    }

    void LateUpdate () {
        CameraRotation();
	}

    private void PlayerRotation()
    {
        yRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        playerYRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        xRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
        xRotation = Mathf.Clamp(xRotation, minRotationX, maxRotationX);

        transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);


    }

    private void CameraRotation()
    {
        mainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

    }

    private void PlayerMovement()
    {
        
        movementVector = new Vector3(Input.GetAxis("Horizontal")*movementSpeed, playerRigidbody.velocity.y, Input.GetAxis("Vertical")*movementSpeed);
        movementVector = transform.TransformDirection(movementVector);
        //Debug.Log(movementVector);
        playerRigidbody.velocity = movementVector;
    }
}
