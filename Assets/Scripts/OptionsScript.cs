using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsScript : MonoBehaviour
{
    [SerializeField] private RectTransform options;
    [SerializeField] private Button openoptions;
    [SerializeField] private Button closeoptions;

    void Start()
    {
        CloseSettings();
        openoptions.onClick.AddListener(OpenSettings);
        closeoptions.onClick.AddListener(CloseSettings);
    }
    void OpenSettings()
    {
        options.gameObject.SetActive(true);
    }
    void CloseSettings()
    {
        options.gameObject.SetActive(false);
    }
}
