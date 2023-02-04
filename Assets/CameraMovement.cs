using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private readonly float keyMoveSpeed;
    private readonly float scrollSpeed;
    private          float xThreshold;
    private          float yThreshold;

    public CameraMovement()
    {
        keyMoveSpeed = 8f;
        scrollSpeed  = 8f;
    }

    // Start is called before the first frame update
    private void Start() { }

    private void Update()
    {
        ProcessKeyboardMovements();

        MousePos();
    }

    // Update is called once per frame
    private void MousePos()
    {
        var screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
        var mousePos   = Input.mousePosition;

        if (!screenRect.Contains(mousePos))
            return;

        xThreshold = Screen.width * 0.05f;
        yThreshold = Screen.height * 0.05f;

        if (mousePos.x < xThreshold)
        {
            var factor = (xThreshold - mousePos.x) / xThreshold * scrollSpeed;
            Move(Vector3.left, factor);
        }

        if (mousePos.x > Screen.width - xThreshold)
        {
            var factor = (mousePos.x - Screen.width + xThreshold) / xThreshold * scrollSpeed;
            Move(Vector3.right, factor);
        }
        
        if (mousePos.y < yThreshold)
        {
            var factor = (yThreshold - mousePos.y) / yThreshold * scrollSpeed;
            Move(Vector3.down, factor);
        }
        
        if (mousePos.y > Screen.height - yThreshold)
        {
            var factor = (mousePos.y - Screen.height + yThreshold) / yThreshold * scrollSpeed;
            Move(Vector3.up, factor);
        }
    }

    private void ProcessKeyboardMovements()
    {
        if (Input.GetKey(KeyCode.W))
            Move(Vector3.up, keyMoveSpeed);

        if (Input.GetKey(KeyCode.A))
            Move(Vector3.left, keyMoveSpeed);

        if (Input.GetKey(KeyCode.S))
            Move(Vector3.down, keyMoveSpeed);

        if (Input.GetKey(KeyCode.D))
            Move(Vector3.right, keyMoveSpeed);
    }

    private void Move(Vector3 directionVector, float speed)
    {
        transform.position += directionVector * (Time.deltaTime * speed);
    }
}