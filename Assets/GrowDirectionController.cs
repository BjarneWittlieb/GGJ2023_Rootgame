using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrowDirectionController : MonoBehaviour
{
    private RandomWalk _randomWalk;

    public NodePicker cursor;

    public float mouseInfluenceRadius = 3f;

    public float influenceMinValue = 0.05f;
    public float influenceMaxValue = .2f;

    public float totalStamina = .5f;

    private Camera cam;

    private float _staminaLeft = 0f;

    private bool _clickedBefore = false;

    public float onChargeRadiusMultiplier = 5f;
    public float onChargeSpeedMultiplier = 2f;

    private float _currentRadiusMultiplier = 1f;
    private float _currentSpeedMultiplier = 1f;

    private float _idleWalkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _randomWalk = GetComponent<RandomWalk>();
        cam = Camera.main;

        _idleWalkSpeed = _randomWalk.walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessINputAndStamina();

        var mousePos = Vector3.Scale(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(1, 1, 0));

        if ((mousePos - transform.position).magnitude > mouseInfluenceRadius * _currentRadiusMultiplier)
        {
            _randomWalk.growInGeneralDirection = false;
            return;
        }

        _randomWalk.growInGeneralDirection = true;
        _randomWalk.generalDirection = mousePos - transform.position;
        _randomWalk.probabilityOfDirectionModifier = ((mousePos - transform.position).magnitude / mouseInfluenceRadius) * (influenceMaxValue - influenceMinValue);
    }

    void ProcessINputAndStamina()
    {
        _currentRadiusMultiplier = 1f;
        _currentSpeedMultiplier = 1f;

        if (Input.GetMouseButtonDown(0))
        {
            if (!_clickedBefore)
            {
                _clickedBefore = true;
                cursor.setNewRadius(mouseInfluenceRadius * onChargeRadiusMultiplier);
            }
            else
            {
                _staminaLeft = Mathf.Max(_staminaLeft - Time.deltaTime, -0.01f);
                if (_staminaLeft > 0)
                {
                    _currentRadiusMultiplier = onChargeRadiusMultiplier;
                    _currentSpeedMultiplier  = onChargeSpeedMultiplier;
                }
            }
        }
        else
        {
            if (_clickedBefore)
            {
                _clickedBefore = false;
                cursor?.setNewRadius(mouseInfluenceRadius);
            }
            else
            {
                _staminaLeft = Mathf.Min(_staminaLeft + Time.deltaTime / 3, totalStamina);
            }
        }

        // TODO CHARGE HERE, speed has to be adjusted :)
        _randomWalk.walkSpeed = _currentSpeedMultiplier * _idleWalkSpeed;
    }
}
