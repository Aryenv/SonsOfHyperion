using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float mDelta = 10.0F;
    public float mSpeed =  3.0F;
    [Space]
    public float minYValue;
    public float maxYValue;
    [Space]
    public float minXValue;
    public float maxXValue;
    [Space]
    public float wheelSensitivity;
    [Space]
    private Camera mainCamera;
    //public float dragSpeed = 2;
    public float minZoom;
    public float maxZoom;
    //public Vector3 dragOrigin;
    [Space]
    private Vector3 mRightDirection = Vector3.right;
    private Vector3 mUpDirection    = Vector3.up;

    private bool moveOn = true;
    private void Start()
    {
        mainCamera = GetComponent<Camera>();

        minYValue = 0;
        minXValue = 0;

        maxXValue = GameManager.Instance.gridHeight * 20;
        maxYValue = GameManager.Instance.gridWidth  * 20;
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            moveOn = !moveOn;
        }
        if (moveOn)
        {
            CameraLimits();
        }
        CameraWheel();
        DragCamera();
    }
    public void SetCamera()
    {
        if (GameManager.Instance.Grid != null)
        {
            transform.position = GameManager.Instance.Grid.GetCenterCell(new Vector2(GameManager.Instance.Grid.GetWidth() / 2, GameManager.Instance.Grid.GetHeight() / 2));
            transform.position = new Vector3(transform.position.x,transform.position.y,-10);
        }
    }
    public void CameraWheel()
    {
        mainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * wheelSensitivity * Time.deltaTime;

        if (mainCamera.orthographicSize < minZoom)
        {
            mainCamera.orthographicSize = minZoom;
        }
        if (mainCamera.orthographicSize > maxZoom)
        {
            mainCamera.orthographicSize = maxZoom;
        }
    }
    public void CameraLimits()
    {
        if (Input.mousePosition.x >= Screen.width - mDelta || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += mRightDirection * Time.deltaTime * mSpeed;
        }
        if (Input.mousePosition.x <= 0 + mDelta || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= mRightDirection * Time.deltaTime * mSpeed;
        }
        if (Input.mousePosition.y >= Screen.height - mDelta || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += mUpDirection * Time.deltaTime * mSpeed;
        }
        if (Input.mousePosition.y <= 0 + mDelta || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= mUpDirection * Time.deltaTime * mSpeed;
        }
        Vector3 pos = transform.position;

        pos.y = Mathf.Clamp(pos.y, minYValue, maxYValue);
        pos.x = Mathf.Clamp(pos.x, minXValue, maxXValue);

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
    public void DragCamera()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //dragOrigin = Input.mousePosition;

            //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            //Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            //transform.Translate(move, Space.World);
        }
    }
}
