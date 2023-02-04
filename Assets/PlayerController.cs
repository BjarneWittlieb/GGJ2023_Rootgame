using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AbilityHolder holder;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            holder.TriggerAbility();
        }
    }
}
