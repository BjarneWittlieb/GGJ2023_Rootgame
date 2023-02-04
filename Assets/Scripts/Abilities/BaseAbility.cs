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
        public bool isCooldown = true;
        
        [SerializeField] public BaseRessource RequiredResource;
        [SerializeField] public int           ResourceCost;
        public Image         AbilityIcon;
        [SerializeField] public string        Name;
        [SerializeField] public KeyCode Code;

        public abstract void Execute(AbilityHolder holder);

        public void OnEnable()
        {
        }

        public void Update()
        {
            CheckCooldown();
        }

        private void CheckCooldown()
        {
            if (Input.GetKey(Code) && isCooldown == false)
            {
                isCooldown = true;
                AbilityIcon.fillAmount = 1;
            }

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
        }

        public bool IsReady()
        {
            // Ressourcen checken - Cooldown checken etc
            return !(CooldownTimeLeft > 0);
        }

        public virtual void OnAbilityUpdate(AbilityHolder holder) { }

    }
}