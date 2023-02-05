using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Stamina : MonoBehaviour
    {
        private float currentStamina;
        
        public int        chargeRate = 3;
        public int        drainRate  = 10;
        public float      maxStamina = 100f;
        public NodePicker cursor;

        [FormerlySerializedAs("mouseInfluenceRadius")] 
        public float baseInfluenceRadius = 3f;
        public float onChargeRadiusMultiplier = 5f;
        public float onChargeSpeedMultiplier  = 2f;
      
        public float SpeedMultiplier { get; private set; }
        public float InfluenceRadius { get; private set; }

        public void Update()
        {
            ChargeStamina();
            ProcessInputs();
        }

        private void ChargeStamina()
        {
            var newStamina = currentStamina + Time.deltaTime * chargeRate;
            currentStamina = Math.Min(maxStamina, newStamina);
        }

        private bool TryDrain(float rate)
        {
            var drain         = rate * Time.deltaTime;
            var enoughStamina = currentStamina >= drain;

            if (!enoughStamina)
                return false;
            
            currentStamina -= drain;
            return true;
        }
        
        void ProcessInputs()
        {
            InfluenceRadius = baseInfluenceRadius;
            SpeedMultiplier = 1f;

            if (Input.GetMouseButton(0))
            {
                if (TryDrain(drainRate))
                {
                    InfluenceRadius *= onChargeRadiusMultiplier;
                    SpeedMultiplier *= onChargeSpeedMultiplier;
                }
            }

            cursor.SetRadius(InfluenceRadius);
        }
    }
}