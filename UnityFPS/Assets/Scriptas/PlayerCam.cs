using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float SensibilityX;
    public float SensibilityY;
    public Transform Orientation;
    public float RotationX;
    public float RotationY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensibilityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensibilityY;

        RotationY += mouseX; 
        RotationX -= mouseY;

        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(RotationX, RotationY, 0);
        Orientation.rotation = Quaternion.Euler(0, RotationY, 0);
    }
}
