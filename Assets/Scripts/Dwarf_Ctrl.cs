using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf_Ctrl : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController charCon;

    private CameraController cam;
    private Vector3 moveAmount;

    private float yStore;
    public float jumpForce, gravityScale;

    public float rotateSpeed;
    public Animator anim;

    public GameObject jumpEffect, landEffect;
    private bool lastGrounded;

    void Start()
    {
        cam = FindObjectOfType<CameraController>();
        lastGrounded = true;
        charCon.Move(new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f));
    }

   
    void Update()
    {
        if (LevelManager.instance.isPlaying)
        {
            yStore = moveAmount.y;

            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            moveAmount = cam.transform.forward * moveZ + cam.transform.right * moveX;
            moveAmount.y = 0;
            moveAmount = moveAmount.normalized;

            if (moveAmount.magnitude > 0.1f)
            {
                if (moveAmount != Vector3.zero)
                {
                    Quaternion newRot = Quaternion.LookRotation(moveAmount);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
                }
            }

            moveAmount.y = yStore;

            charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);

            float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;
            anim.SetFloat("speed", moveVel);
            anim.SetBool("isGrounded", charCon.isGrounded);
            anim.SetFloat("yVel", moveAmount.y);

            if (charCon.isGrounded)
            {
                jumpEffect.SetActive(false);

                if (!lastGrounded)
                {
                    landEffect.SetActive(true);
                }

                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter))
                {
                    moveAmount.y = jumpForce;
                    jumpEffect.SetActive(true);

                }
            }

            lastGrounded = charCon.isGrounded;
        }

       
    }

    private void FixedUpdate()
    {
        if (!charCon.isGrounded)
        {
            moveAmount.y = moveAmount.y + Physics.gravity.y * gravityScale * Time.deltaTime;
        }
        else
        {
            moveAmount.y = Physics.gravity.y * gravityScale * Time.deltaTime;
        }
    }
}
