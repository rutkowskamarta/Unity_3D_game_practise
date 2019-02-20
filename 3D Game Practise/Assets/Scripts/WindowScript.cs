using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour {

    [SerializeField] private float openSpeed = 30;

    private bool isOpened = false;

    private float baseRotationY;
    private float desiredRotationY;
    private float currentRotationY;

    private bool rotating = false;

    private void Start()
    {
        baseRotationY = transform.rotation.y;
        currentRotationY = baseRotationY;
    }

    public void WindowInteraction()
    {
        currentRotationY = transform.rotation.y;
        isOpened = !isOpened;

        if (isOpened)
            desiredRotationY = baseRotationY + 90f;
        else
            desiredRotationY = baseRotationY;

        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        if (!rotating)
        {
            rotating = true;
            Quaternion rot = Quaternion.Euler(transform.eulerAngles.x, desiredRotationY, transform.eulerAngles.z);
            while (Mathf.Abs(transform.eulerAngles.y - desiredRotationY) > 1.0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, openSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            rotating = false;
        }
    }
}
