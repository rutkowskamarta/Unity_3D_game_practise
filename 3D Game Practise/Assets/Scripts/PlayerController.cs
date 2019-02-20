using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float movementSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float maxRotationX = 100f;
    [SerializeField] private float minRotationX = -100f;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float windowInteractionDistance = 4f;
    [SerializeField] private Image interactionSign;

    private Rigidbody playerRigidbody;
    private Vector3 movementVector = Vector3.zero;

    private float yRotation;
    private float xRotation;

    private bool isGrounded = false;
    private float playerYRotation;

    private RaycastHit raycastHit;
    private Vector3 forwardVector;

	void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        playerYRotation = transform.rotation.y;
	}

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
        PlayerJump();
        RaycastInteraction();
    }

    void LateUpdate () {
        CameraRotation();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        
    }

    private void RaycastInteraction()
    {
        forwardVector = mainCamera.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forwardVector, out raycastHit, windowInteractionDistance))
        {
            if(raycastHit.collider.gameObject.tag == "Window")
            {
                interactionSign.enabled = true;
                OpenTheWindow(raycastHit.collider.gameObject);
            } else
            {
                interactionSign.enabled = false;
            }
        }
        else
        {
            interactionSign.enabled = false;
        }
    }

    private void OpenTheWindow(GameObject currentWindow)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentWindow.transform.GetComponentInParent<WindowScript>().WindowInteraction();
        }
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
        playerRigidbody.velocity = movementVector;
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpSpeed, playerRigidbody.velocity.z); 
        }
    }
}
