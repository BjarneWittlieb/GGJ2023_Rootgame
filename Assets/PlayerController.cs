using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{ 
    //TODO Collection von Holdern
    public AbilityHolder splitHolder;
    public AbilityHolder attackholder;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            splitHolder.TriggerAbility();
        
        if(Input.GetKeyUp(KeyCode.Alpha2))
            attackholder.TriggerAbility();
        
    }
}
