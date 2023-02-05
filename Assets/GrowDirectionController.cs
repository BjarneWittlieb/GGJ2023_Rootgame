using DefaultNamespace;
using UnityEngine;

public class GrowDirectionController : MonoBehaviour
{
    public  Stamina    stamina;
    public  float      influenceMinValue = 0.05f;
    public  float      influenceMaxValue = .2f;
    
    private RandomWalk randomWalk;
    private float      originalWalkSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        stamina           = GameObject.Find("Player").GetComponent<Stamina>();
        randomWalk        = GetComponent<RandomWalk>();
        originalWalkSpeed = randomWalk.walkSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        var mousePos = stamina.cursor.marker.transform.position;

        if ((mousePos - transform.position).magnitude > stamina.InfluenceRadius)
        {
            randomWalk.growInGeneralDirection = false;
            return;
        }

        var position     = transform.position;
        var targetVector = mousePos - position;

        randomWalk.growInGeneralDirection         = true;
        randomWalk.generalDirection               = targetVector;
        randomWalk.probabilityOfDirectionModifier = targetVector.magnitude / stamina.InfluenceRadius * (influenceMaxValue - influenceMinValue) + influenceMinValue;
        randomWalk.walkSpeed                      = stamina.SpeedMultiplier * originalWalkSpeed;
    }
}