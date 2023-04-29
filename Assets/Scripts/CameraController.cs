using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private float moveSpeed = 0.02f;
    private float scrollSpeed = 10f;

    public int speed;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position -= scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            var currEulerAngles = transform.eulerAngles;
            currEulerAngles.y -= speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(currEulerAngles);
            currEulerAngles.y = Mathf.Clamp(70, currEulerAngles.y, -70);
        }

        if (Input.GetKey(KeyCode.E))
        {
            var currEulerAngles = transform.eulerAngles;
            currEulerAngles.y += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(currEulerAngles);
            currEulerAngles.y = Mathf.Clamp(70, currEulerAngles.y, -70);
        }
    }

}