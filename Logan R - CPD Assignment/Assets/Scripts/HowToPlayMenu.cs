﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayMenu : MonoBehaviour
{
    private string[] controllers;

    public GameObject mainMenu;
    public GameObject startButton;
    public GameObject keyboardControls;
    public GameObject joystickControls;
    public GameObject touchControls;

    public GameObject switchButton;
    public GameObject controlsMenu;
    public GameObject instructionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform)
        {
            touchControls.SetActive(true);
            keyboardControls.SetActive(true);
        }
        else
        {
            controllers = Input.GetJoystickNames();

            if (controllers.Length == 0)
            {
                keyboardControls.SetActive(true);
                joystickControls.SetActive(false);
            }

            for (int i = 0; i < controllers.Length; i++)
            {
                if (!String.IsNullOrEmpty(controllers[i]))
                {
                    keyboardControls.SetActive(false);
                    joystickControls.SetActive(true);
                }
                else
                {
                    keyboardControls.SetActive(true);
                    joystickControls.SetActive(false);
                }
            }
        }
    }

    public void Switch()
    {
        if (controlsMenu.activeSelf)
        {
            switchButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONTROLS";
            instructionsMenu.SetActive(true);
            controlsMenu.SetActive(false);
        }
        else if (instructionsMenu.activeSelf)
        {
            switchButton.GetComponentInChildren<TextMeshProUGUI>().text = "INSTRUCTIONS";
            controlsMenu.SetActive(true);
            instructionsMenu.SetActive(false);
        }
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
        gameObject.SetActive(false);
    }
}
