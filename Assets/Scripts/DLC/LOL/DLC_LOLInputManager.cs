using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLC_LOLInputManager : MonoBehaviour
{
    public DLC_LOLPlayer player;

    public Transform cameraTransform;
    public float percent;
    public float speed;
    public float cameraSizeScroll;

    private int zone;


    private void Awake()
    {
        Resize();   
    }

    private void Resize()
    {
        zone = (int)(Mathf.Min(Screen.width, Screen.height) * percent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            player.OnKeyQ();
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            player.OnClick();
        }
        Vector3 camMove = new Vector3(0, 0, 0);
        if (Input.mousePosition.x < zone)
        {
            camMove.x = -speed * Time.deltaTime;
        } else if (Input.mousePosition.x > Screen.width - zone)
        {
            camMove.x = speed * Time.deltaTime;
        }
        if (Input.mousePosition.y < zone)
        {
            camMove.y = -speed * Time.deltaTime;    
        }
        else if(Input.mousePosition.y > Screen.height - zone)
        {
            camMove.y = speed * Time.deltaTime;
        }
        cameraTransform.position += camMove;
        if (Input.GetKey(KeyCode.Space))
        {
            player.OnKeySpace();
            cameraTransform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraTransform.position.z);
        }
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * cameraSizeScroll * Time.deltaTime;
    }
}
