using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private Vector3 offset;

    public float moveSpeed = 15f;

    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    private float verticalRotstore;
    private Vector2 mouseInput;

    [HideInInspector]
    public Transform endCamPos;

    void Start()
    {
        AssignTarget();

       // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Lookaround();

        if (endCamPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, endCamPos.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, endCamPos.rotation, moveSpeed * Time.deltaTime);
        }
        else
        {

            transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime);

            if (transform.position.y < offset.y)
            {
                transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
            }
        }
    }

    private void AssignTarget()
    {
        if (target == null)
        {
            target = FindObjectOfType<Dwarf_Ctrl>().transform;

            offset = transform.position -target.position;
        }
    }

    public void SnapToTarget()
    {
        AssignTarget();

        transform.position = target.position + offset;
    }

    public void Lookaround()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")*mouseSensitivity);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

      
    }
}
