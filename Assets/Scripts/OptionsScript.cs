using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;

    public Button openOptions;
    public Button closeOptions;

    void Start()
    {
        CloseSettings();
        openOptions.onClick.AddListener(OpenSettings);
        closeOptions.onClick.AddListener(CloseSettings);
    }
    void OpenSettings()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }
    void CloseSettings()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }
}
