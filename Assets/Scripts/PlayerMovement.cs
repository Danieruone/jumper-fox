using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;

    public bool isAlive = true;
    public float runSpeed = 30f;    
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (Input.GetMouseButton(0))
            {
                jump = true;
                GetComponent<Animator>().SetTrigger("Jump");
            }
            controller.Move(runSpeed * Time.deltaTime, false, jump);
            GetComponent<Animator>().SetBool("isRunning", true);
            jump = false;
        }
        else
        {
            controller.Move(0f, false, jump);
            GetComponent<Animator>().SetBool("isRunning", false);
        }
        
    }
}