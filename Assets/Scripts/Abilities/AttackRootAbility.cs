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
            Attack(holder);
        }
        
        private void Attack(AbilityHolder holder)
        {
            picker = GameObject.Find("Player").GetComponent<NodePicker>();
            var rootTarget = picker.target;

            if (!rootTarget) 
                return;

            if (rootTarget.GetComponent<RootAttack>() is RootAttack rootAttack)
            {
                rootAttack.attack(picker.marker.transform.position);
                audio.Play();
                holder.Ability.isCooldown = true;
                holder.Ability.AbilityIcon.fillAmount = 1;
            }
        }
    }
}