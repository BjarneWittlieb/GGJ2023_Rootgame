using DefaultNamespace;
using UnityEngine;

public class GrowDirectionController : MonoBehaviour
{
    public  Stamina    stamina;
    public  float      influenceMinValue = 0.05f;
    public  float      influenceMaxValue = .2f;
    public  float      defaultWalkSpeed  = .5f;
    public GameObject influenceMarker;

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

        float dist = (mousePos - transform.position).magnitude;

        if (influenceMarker && (dist > stamina.InfluenceRadius || !GetComponent<IsRootTip>().IsTip))
            influenceMarker.SetActive(false);


        if (dist > stamina.InfluenceRadius)
        {
            randomWalk.growInGeneralDirection = false;
            randomWalk.walkSpeed              = defaultWalkSpeed;
            return;
        }

        if (influenceMarker && GetComponent<IsRootTip>().IsTip) {
            influenceMarker.SetActive(true);
            Color clr = influenceMarker.GetComponent<SpriteRenderer>().color;
            float perc = (1-(dist / stamina.InfluenceRadius)) * 0.2f;
            influenceMarker.GetComponent<SpriteRenderer>().color = new Color(clr.r, clr.g, clr.b,perc);
        }
        var position     = transform.position;
        var targetVector = mousePos - position;

        randomWalk.growInGeneralDirection         = true;
        randomWalk.generalDirection               = targetVector;
        randomWalk.probabilityOfDirectionModifier = targetVector.magnitude / stamina.InfluenceRadius * (influenceMaxValue - influenceMinValue) + influenceMinValue;
        randomWalk.walkSpeed                      = stamina.SpeedMultiplier * defaultWalkSpeed;
    }
}