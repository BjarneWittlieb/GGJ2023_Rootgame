using DefaultNamespace;
using UnityEngine;

namespace Abilities
{
    public class AttackRootAbility : BaseAbility
    {
        private NodePicker picker;
        public void OnEnable()
        {
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
            picker = GameObject.Find("Player").GetComponent<NodePicker>();
            var rootTarget = picker.target;

            if (!rootTarget) 
                return;
            
            if (rootTarget.GetComponent<RootAttack>() is RootAttack rootAttack)
                rootAttack.attack(picker.marker.transform.position);
        }
    }
}