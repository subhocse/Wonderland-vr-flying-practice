using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    private float yawSpeed = 0.05f;
    private float pitchSpeed = 0.05f;
    private float maxSpeed = 10.0f;
    private float minSpeed = 1.0f;
    private float accSpeed;

    public float moveSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        accSpeed = 1.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (accSpeed < maxSpeed && Input.GetMouseButtonDown(0))
        {
            accSpeed += 1.0f;
        }
        else if (accSpeed > minSpeed && Input.GetMouseButtonDown(1))
        {
            accSpeed -= 1.0f;
        }
        

        Vector3 moveVector = transform.forward * moveSpeed * accSpeed;

        Vector3 yaw = Input.GetAxis("Horizontal") * transform.right * yawSpeed * accSpeed;
        Vector3 pitch = Input.GetAxis("Vertical") * transform.up * pitchSpeed * accSpeed;
        Vector3 dir = yaw + pitch;

        // Limit Pitch
        float maxPitch = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;
        //float maxYaw = Quaternion.LookRotation(moveVector + dir).eulerAngles.y;

        if (maxPitch < 90 && maxPitch > 70 || maxPitch > 270 && maxPitch < 290 )
            //|| maxYaw < 90 && maxYaw > 70 || maxYaw > 270 && maxYaw < 290)
        {
            // do not add dir
        }
        else
        {
            // add dir
            moveVector += dir;
            transform.rotation = Quaternion.LookRotation(moveVector);
        }

        // Final Move
        controller.Move(moveVector * Time.deltaTime);

    }
}


// Reference:https://www.youtube.com/watch?v=u7xxxwDCxC8