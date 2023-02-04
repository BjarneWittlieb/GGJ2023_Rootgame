using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

namespace Abilities
{
    public abstract class BaseAbility : ScriptableObject
    {
        [SerializeField]
        public AbilityCooldown Cooldown { get; set; } = new();
        [SerializeField]
        public BaseRessource RequiredResource { get; set; }
        [SerializeField]
        public int ResourceCost { get; set; }
        public Image  Icon { get;      set; }
        public string Name { get;      set; }

        public abstract void Execute(AbilityHolder holder);

        public bool IsReady()
        {
            // Ressourcen checken - Cooldown checken etc
            return !(Cooldown.CooldownTimeLeft > 0);
        }

        public virtual void OnAbilityUpdate(AbilityHolder holder) { }

    }
}