using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

namespace Abilities
{
    public abstract class BaseAbility : MonoBehaviour
    {
        public float Cooldown;
        public float CooldownTimeLeft;
        
        [SerializeField] public BaseRessource RequiredResource;
        [SerializeField] public int           ResourceCost;
        [SerializeField] public Image         Icon;
        [SerializeField] public string        Name;

        public abstract void Execute(AbilityHolder holder);

        public void OnEnable()
        {
        }

        public bool IsReady()
        {
            // Ressourcen checken - Cooldown checken etc
            return !(CooldownTimeLeft > 0);
        }

        public virtual void OnAbilityUpdate(AbilityHolder holder) { }

    }
}