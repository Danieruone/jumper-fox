using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController controller;
    public float runSpeed = 30f;

    float horizontalMove = 0f;
    bool jump = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            jump = true;
            GetComponent<Animator>().SetTrigger("Jump");
        }

    }

    private void FixedUpdate()
    {
        controller.Move(runSpeed * Time.deltaTime, false, jump);
        GetComponent<Animator>().SetBool("isRunning", true);
        jump = false;
    }
}
