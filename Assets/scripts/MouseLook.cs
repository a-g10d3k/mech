using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLookDirection();
    }

    private void UpdateLookDirection()
    {
        //Horizontal
        Vector3 playerRot = gameObject.transform.rotation.eulerAngles;
        playerRot += new Vector3(0, Input.GetAxis("Mouse X"), 0);
        gameObject.transform.rotation = Quaternion.Euler(playerRot);
        //Vertical
        Vector3 cameraRot = Camera.main.transform.rotation.eulerAngles;
        cameraRot += new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
        if (cameraRot.x > 80 && cameraRot.x < 180)
        {
            cameraRot = new Vector3(80, playerRot.y, 0);
        }
        if (cameraRot.x < 280 && cameraRot.x > 180)
        {
            cameraRot = new Vector3(280, playerRot.y, 0);
        }
        Camera.main.transform.rotation = Quaternion.Euler(cameraRot);
    }
}
