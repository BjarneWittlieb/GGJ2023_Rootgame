using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class AbilityHolder : MonoBehaviour
    {
        public                  UnityEvent    OnTriggerAbility;
        private                 Coroutine     handleAbilityUsage;
        
        [SerializeField] public Root          Owner;
        [SerializeField] public BaseAbility   Ability;

        public void TriggerAbility()
        {
            if (Ability.State != AbilityStates.Ready)
                return;
            
            handleAbilityUsage = StartCoroutine(HandleAbilityUsage_CO());
        }

        private IEnumerator HandleAbilityUsage_CO()
        {
            yield return null;
            Ability.Execute(this);
            Ability.State = AbilityStates.Cooldown;
            OnTriggerAbility?.Invoke();

            if (!Ability.IsReady())
                StartCoroutine(HandleAbiltyCheck_CO());
        }

        private IEnumerator HandleAbiltyCheck_CO()
        {
            var cooldown = Math.Ceiling(Ability.Cooldown);
            yield return new WaitForSeconds((int)cooldown);
            
            Ability.State = AbilityStates.Ready;
        }
    }
}