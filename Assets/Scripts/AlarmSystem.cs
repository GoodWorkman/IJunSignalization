using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    public event Action OnIntruderDetected;
    public event Action OnIntruderLeft;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IPlayer>() != null)
        {
            OnIntruderDetected?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IPlayer>() != null)
        {
            OnIntruderLeft?.Invoke();
        }
    }
}
