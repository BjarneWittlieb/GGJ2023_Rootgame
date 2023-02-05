using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public class SplitRootAbility : BaseAbility
    {

        private NodePicker picker;

        public GameObject directionIndicator;

        public void OnEnable()
        {
            picker           = GameObject.Find("Player").GetComponent<NodePicker>();
            Cooldown         = 10;
            CooldownTimeLeft = 10;
            RequiredResource = ScriptableObject.CreateInstance<Mana>();
            ResourceCost     = 40;
        }

        public override void Execute(AbilityHolder holder)
        {
            StartCoroutine(Cast(holder));
        }

        public IEnumerator Cast(AbilityHolder holder)
        {
            var image = GameObject.Find("Split Active").GetComponent<Image>();
            image.enabled = true;
            while (true)
            {
                var currentTarget = picker.target;
            
                if (currentTarget) {

                    directionIndicator.SetActive(true);
                    directionIndicator.transform.position = currentTarget.transform.position;
                    Vector3 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouseP.z = currentTarget.transform.position.z;
                    float angle = Vector2.SignedAngle(new Vector2(0, 1), (mouseP - currentTarget.transform.position).normalized);
                    directionIndicator.transform.eulerAngles = new Vector3(0, 0, angle);
                }
                else {
                    directionIndicator.SetActive(false);
                }

                // cast on leftclick
                if (Input.GetMouseButtonDown(0) && currentTarget)
                {
                    image.enabled = false;
                    Split(currentTarget);
                    audio.Play();
                    StartCooldown();
                    break;
                }
                
                if (Input.GetMouseButtonDown(1))
                {
                    // cancel on right click
                    directionIndicator.SetActive(false);
                    image.enabled = false;
                    State = AbilityStates.Ready;
                    break;
                }
            
                picker.draw = true;
                yield return null;
            }
        }

        private void Split(GameObject currentTarget)
        {
            if (currentTarget) 
            {
                if (currentTarget.GetComponent<IsRootTip>()) {
                    currentTarget.GetComponent<IsRootTip>().IsTip                  =  true;
                    currentTarget.GetComponent<RootNode>().IsDead                  =  false;
                    currentTarget.GetComponent<RootInfluence>().TipDeadOnInfluence *= 2;
                    currentTarget.GetComponent<Growing>().branch();

                    Vector3 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouseP.z = currentTarget.transform.position.z;
                    Vector3 dir = (mouseP - currentTarget.transform.position).normalized * currentTarget.GetComponent<IsRootTip>().SplitDistance * 1.1f;
                    currentTarget.transform.position += dir;
                }
            }
            directionIndicator.SetActive(false);
        }
        
       
    }
}