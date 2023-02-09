using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Abilities
{
    public abstract class BaseAbility : MonoBehaviour
    {
        public float Cooldown;
        public float CooldownTimeLeft;
        
        [SerializeField] public BaseRessource RequiredResource;
        [SerializeField] public int           ResourceCost;
        public Image         AbilityIcon;
        [SerializeField] public string        Name;
        [SerializeField] public KeyCode Code;
        [SerializeField] public GameObject audio;

        public AbilityStates State { get; set; }

        public abstract void Execute(AbilityHolder holder);
        
        public void Start()
        {
            State = CooldownTimeLeft > 0 ? AbilityStates.Cooldown : AbilityStates.Ready;
            UpdateCooldown();
        }

        public void Update()
        {
            UpdateCooldown();
        }

        private void UpdateCooldown()
        { 
            if (CooldownTimeLeft > 0)
            {
                CooldownTimeLeft = Math.Max(0, CooldownTimeLeft - Time.deltaTime);
                UpdateIcon();

                if (CooldownTimeLeft == 0)
                    State = AbilityStates.Ready;
            }
        }

        private void UpdateIcon()
        {
            AbilityIcon.fillAmount = CooldownTimeLeft / Cooldown;
        }

        public void StartCooldown()
        {
            State            = AbilityStates.Cooldown;
            CooldownTimeLeft = Cooldown;
        }

        public bool IsReady()
        {
            // Ressourcen checken - Cooldown checken etc
            return !(CooldownTimeLeft > 0);
        }

        public virtual void OnAbilityUpdate(AbilityHolder holder) { }

    }
}