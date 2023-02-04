using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public class SplitRootAbility : BaseAbility
    {
        private NodePicker picker;
        private bool       aiming;

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
            StartCoroutine(Cast());
            Grow();
        }

        public IEnumerator Cast()
        {
            yield return DrawTarget();
        }

        public IEnumerator DrawTarget()
        {
            var target = picker.PickTarget();
            yield return null;
        }

        private void Grow()
        {
            var rootTarget = picker.PickTarget();
            
            if (rootTarget) 
            {
                if (rootTarget.GetComponent<IsRootTip>()) {
                    rootTarget.GetComponent<IsRootTip>().IsTip                  =  true;
                    rootTarget.GetComponent<RootNode>().IsDead                  =  false;
                    rootTarget.GetComponent<RootInfluence>().TipDeadOnInfluence *= 2;
                }
            }
        }
        
       
    }
}