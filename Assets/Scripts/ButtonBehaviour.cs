using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    public Button Button;

    public void Update()
    {
        OnHotkey();
    }

    public void OnHotkey()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            Button.onClick.Invoke();
    }

    public void Activate()
    {
        // Debug.Log($"{Button.name} angeklickert.");
    }
}
