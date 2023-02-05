using UnityEngine;

namespace DefaultNamespace
{
    public class MouseFollow : MonoBehaviour
    {
        private Camera camera1;

        private void Start()
        {
            camera1 = Camera.main;
        }

        private void Update()
        {
            if (!camera1)
                return;

            var mousePosition = camera1.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z    = camera1.transform.position.z + camera1.nearClipPlane;
            transform.position = mousePosition;
        }
    }
}