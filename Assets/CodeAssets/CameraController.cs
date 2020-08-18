using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    private float speed = 5.0f;
    private float zoomSpeed = 2.0f;

    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    private Transform objectToMove;    
    private Vector3 offset;       // The object's position relative to the mouse position.

    public void ZoomToLookAt(GameObject obj)
    {
        var camera = this.GetComponent<Camera>();
        var objTransform = obj.transform.position;
        var camTransform = camera.gameObject.transform.position;
        camera.gameObject.transform.position = new Vector3(objTransform.x, objTransform.y, camTransform.z);
    }

     void Update()
    {
        var camera = this.GetComponent<Camera>();

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize += scroll * zoomSpeed;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

    }

}
