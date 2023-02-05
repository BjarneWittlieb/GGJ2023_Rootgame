using DefaultNamespace;
using UnityEngine;

public class GrowDirectionController : MonoBehaviour
{
    public  Stamina    stamina;
    public  float      influenceMinValue = 0.05f;
    public  float      influenceMaxValue = .2f;
    public  float      defaultWalkSpeed  = .5f;
    
    private RandomWalk randomWalk;
    

    // Start is called before the first frame update
    private void Start()
    {
        if (!GameObject.Find("Player"))
            return;
        
        stamina           = GameObject.Find("Player").GetComponent<Stamina>();
        randomWalk        = GetComponent<RandomWalk>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!stamina)
            return;
        var mousePos = stamina.cursor.marker.transform.position;

        if ((mousePos - transform.position).magnitude > stamina.InfluenceRadius)
        {
            randomWalk.growInGeneralDirection = false;
            randomWalk.walkSpeed              = defaultWalkSpeed;
            return;
        }

        var position     = transform.position;
        var targetVector = mousePos - position;

        randomWalk.growInGeneralDirection         = true;
        randomWalk.generalDirection               = targetVector;
        randomWalk.probabilityOfDirectionModifier = targetVector.magnitude / stamina.InfluenceRadius * (influenceMaxValue - influenceMinValue) + influenceMinValue;
        randomWalk.walkSpeed                      = stamina.SpeedMultiplier * defaultWalkSpeed;
    }
}