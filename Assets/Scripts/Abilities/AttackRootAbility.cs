using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public class AttackRootAbility : BaseAbility
    {
        private NodePicker picker;
        public void OnEnable()
        {
            picker           = GameObject.Find("Player").GetComponent<NodePicker>();
            Cooldown         = 1;
            CooldownTimeLeft = 1;
            RequiredResource = ScriptableObject.CreateInstance<Mana>();
            ResourceCost     = 40;
        }
        
        public override void Execute(AbilityHolder holder)
        {
            Attack();
        }
        
        private void Attack()
        {
            var rootTarget = picker.PickTarget();
            
            if (rootTarget)
            {
                if (rootTarget.GetComponent<RootAttack>())
                    rootTarget.GetComponent<RootAttack>().attack(picker.marker.transform.position);
            }
        }
    }
}