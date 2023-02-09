﻿using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Stamina : MonoBehaviour
    {
        private float currentStamina;

        public Image     staminaSlider;
        public int        chargeRate = 3;
        public int        drainRate  = 10;
        public float      maxStamina = 100f;
        public NodePicker cursor;
        public GameObject fastGrowingAudio;

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
            staminaSlider.fillAmount = 1 - (currentStamina / maxStamina);
            // Debug.Log(currentStamina +", " + InfluenceRadius);
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

            if (Input.GetMouseButtonDown(0) && fastGrowingAudio)
                Instantiate(fastGrowingAudio);
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