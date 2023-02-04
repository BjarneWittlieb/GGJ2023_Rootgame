using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public class SplitRootAbility : BaseAbility
    {

        private NodePicker picker;

        public void OnEnable()
        {
            picker           = GameObject.Find("Player").GetComponent<NodePicker>();
            Cooldown         = 10;
            CooldownTimeLeft = 10;
            RequiredResource = ScriptableObject.CreateInstance<Mana>();
            ResourceCost     = 40;
        }

        public override void Execute(AbilityHolder holder)
        {
            StartCoroutine(Cast(holder));
        }

        public IEnumerator Cast(AbilityHolder holder)
        {
            while (true)
            {
                var currentTarget = picker.target;
            
                // cast on leftclick
                if (Input.GetMouseButtonDown(0) && currentTarget)
                {
                    Split(currentTarget);
                    audio.Play();
                    holder.Ability.isCooldown = true;
                    holder.Ability.AbilityIcon.fillAmount = 1;
                    break;
                }
                
                if (Input.GetMouseButtonDown(1))
                {
                    // cancel on right click
                    break;
                }
            
                picker.draw = true;
                yield return null;
            }
        }

        // public IEnumerator DrawTarget()
        // {
        //     picker.DrawTargetIndicator();
        //     yield return null;
        // }

        private void Split(GameObject currentTarget)
        {
            if (currentTarget) 
            {
                if (currentTarget.GetComponent<IsRootTip>()) {
                    currentTarget.GetComponent<IsRootTip>().IsTip                  =  true;
                    currentTarget.GetComponent<RootNode>().IsDead                  =  false;
                    currentTarget.GetComponent<RootInfluence>().TipDeadOnInfluence *= 2;
                }
            }
        }
        
       
    }
}