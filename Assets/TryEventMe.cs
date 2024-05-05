using System;
using UnityEngine;

public class TryEventMe : MonoBehaviour
{
    public static Action OnEventTry;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnEventTry?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
