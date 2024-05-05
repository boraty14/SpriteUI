using System;
using BratyUI;
using UnityEngine;

public class TryMe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        TryEventMe.OnEventTry += OnEventTry;
        // Debug.Log(Mathf.Approximately(0.00001f,0.00001f));
        // Debug.Log(Mathf.Approximately(0.00001f,0.00002f));
        // ScreenHelper.Log();
    }

    private void OnDisable()
    {
        TryEventMe.OnEventTry -= OnEventTry;
    }

    private void OnEventTry()
    {
        Debug.Log("asdsad");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
