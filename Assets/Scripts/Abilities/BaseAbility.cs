using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public abstract class BaseAbility : MonoBehaviour
    {
        public float Cooldown;
        public float CooldownTimeLeft;
        public bool isCooldown;
        
        [SerializeField] public BaseRessource RequiredResource;
        [SerializeField] public int           ResourceCost;
        public Image         AbilityIcon;
        [SerializeField] public string        Name;
        [SerializeField] public KeyCode Code;
        [SerializeField] public AudioSource audio;

        public abstract void Execute(AbilityHolder holder);

        public void Update()
        {
            CheckCooldown();
        }

        private void CheckCooldown()
        { 
            if (isCooldown)
            {
                AbilityIcon.fillAmount -= 1 / Cooldown * Time.deltaTime;

                if (AbilityIcon.fillAmount <= 0)
                {
                    AbilityIcon.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }

        public void Start()
        {
            AbilityIcon.fillAmount = 1;
            isCooldown = true;
            CheckCooldown();
        }

        public bool IsReady()
        {
            // Ressourcen checken - Cooldown checken etc
            return !(CooldownTimeLeft > 0);
        }

        public virtual void OnAbilityUpdate(AbilityHolder holder) { }

    }
}