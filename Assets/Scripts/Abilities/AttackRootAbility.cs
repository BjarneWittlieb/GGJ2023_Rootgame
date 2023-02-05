using System.Collections;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities
{
    public class AttackRootAbility : BaseAbility
    {
        private NodePicker picker;
        public void OnEnable()
        {
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
                picker = GameObject.Find("Player").GetComponent<NodePicker>();
                // cast on leftclick
                if (Input.GetMouseButtonDown(0) && picker.target is GameObject target)
                {
                    Attack(holder, target);
                    break;
                }
                
                if (Input.GetMouseButtonDown(1))
                {
                    // cancel on right click
                    break;
                }
            
                yield return null;
            }
        }
        
        private void Attack(AbilityHolder holder, GameObject rootTarget)
        {
            if (rootTarget.GetComponent<RootAttack>() is RootAttack rootAttack)
            {
                rootAttack.attack(picker.marker.transform.position);
                audio.Play();
                StartCooldown();
            }
        }
    }
}