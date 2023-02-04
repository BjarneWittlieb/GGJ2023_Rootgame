using UnityEngine;

namespace Abilities
{

[CreateAssetMenu(menuName = "Abilities/Split", fileName = "Split Ability")]    
    public class SplitRoot : BaseAbility
    {
        public SplitRoot()
        {
            Cooldown.Cooldown         = 60;
            Cooldown.CooldownTimeLeft = 10;
        }

        public override void Execute(AbilityHolder holder)
        {
            Debug.Log("prewprpwrp");
        }
    }
}