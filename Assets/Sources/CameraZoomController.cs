using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoomController : MonoBehaviour
{

    public float zoomSpeed = 0.5f;
    public int scrollWheelMinDistance = -2;
    public int scrollWheelMaxDistance = 6;
    public int scrollCount = 3; // Closer position = _scrollWheelMinDistance | Farthest position = scrollWheelMaxDistance

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            if(scrollCount > scrollWheelMinDistance)
            {
                transform.Translate(new Vector3(0, 0, zoomSpeed));
                scrollCount--;
            }
        } else if(Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if(scrollCount < scrollWheelMaxDistance)
            {
                transform.Translate(new Vector3(0, 0, -1 * zoomSpeed));
                scrollCount++;
            }
        }
    }
}
