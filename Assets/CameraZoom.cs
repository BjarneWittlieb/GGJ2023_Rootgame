using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;
    
    private float minimum;
    private float maximum = 15f;

    // Start is called before the first frame update
    void Start()
    {
        minimum = virtualCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, maximum, Time.deltaTime);
        }

        if (Input.mouseScrollDelta.y > 0 || Input.GetKey(KeyCode.E))
        {
            virtualCamera.m_Lens.OrthographicSize += 100f;
            // StopAllCoroutines();
            // StartCoroutine(Lerp(virtualCamera.m_Lens.OrthographicSize, minimum));
        }
            
        
    }

    IEnumerator Lerp(float start, float end)
    {
        var t = 0f;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        // while (virtualCamera.m_Lens.OrthographicSize != end)
        // {
        //     virtualCamera.m_Lens.OrthographicSize =  Mathf.Lerp(start, end, t);
        //     t                                     += Time.deltaTime;
        //     yield return null;
        // }

        for (int i = 0; i < 20; i++)
        {
            virtualCamera.m_Lens.OrthographicSize =  Mathf.Lerp(start, end, t);
            t                                     += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }    
}
