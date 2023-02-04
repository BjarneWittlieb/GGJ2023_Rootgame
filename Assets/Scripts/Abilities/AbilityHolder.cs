using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class AbilityHolder : MonoBehaviour
    {
        public enum AbilityStates
        {
            ReadyToUse = 0,
            Cooldown   = 1
        }

        public                  AbilityStates CurrentAbilityState = AbilityStates.ReadyToUse;
        public                  UnityEvent    OnTriggerAbility;
        private                 Coroutine     handleAbilityUsage;
        
        [SerializeField] public Root          Owner;
        [SerializeField] public BaseAbility   Ability;

        public void TriggerAbility()
        {
            if (CurrentAbilityState != AbilityStates.ReadyToUse)
                return;
            
            handleAbilityUsage = StartCoroutine(HandleAbilityUsage_CO());
        }

        private IEnumerator HandleAbilityUsage_CO()
        {
            yield return new WaitForSeconds(1);
            Ability.Execute(this);
            CurrentAbilityState = AbilityStates.Cooldown;
            OnTriggerAbility?.Invoke();

            if (!Ability.IsReady())
                StartCoroutine(HandleAbiltyCheck_CO());
        }

        private IEnumerator HandleAbiltyCheck_CO()
        {
            var cooldown = Math.Ceiling(Ability.CooldownTimeLeft);
            yield return new WaitForSeconds((int)cooldown);
            
            CurrentAbilityState = AbilityStates.ReadyToUse;
        }
    }
}