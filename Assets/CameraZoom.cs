using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private readonly         float                    maximumZoom = 15f;
    private readonly         float                    minimumZoom = 3.5f;
    private                  float                    zoomSpeed   = 0.4f;

    // Start is called before the first frame update
    private void Start() { }

    // Update is called once per frame
    private void Update()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            var diff = Math.Min(zoomSpeed, maximumZoom - virtualCamera.m_Lens.OrthographicSize);
            virtualCamera.m_Lens.OrthographicSize += diff;
        }

        if (Input.mouseScrollDelta.y > 0 )
        {
            var diff = Math.Min(zoomSpeed, virtualCamera.m_Lens.OrthographicSize - minimumZoom);
            virtualCamera.m_Lens.OrthographicSize -= diff;
        }
    }

    private IEnumerator Lerp(float start, float end)
    {
        var t = 0f;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        // while (virtualCamera.m_Lens.OrthographicSize != end)
        // {
        //     virtualCamera.m_Lens.OrthographicSize =  Mathf.Lerp(start, end, t);
        //     t                                     += Time.deltaTime;
        //     yield return null;
        // }

        for (var i = 0; i < 20; i++)
        {
            virtualCamera.m_Lens.OrthographicSize =  Mathf.Lerp(start, end, t);
            t                                     += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}