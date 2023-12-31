using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class AlertSoundTuner : MonoBehaviour
{
    [SerializeField] private IntruderDetector _intruderDetector;

    private AudioSource _audioSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeSpeed = 0.1f;
    private float _tolerance = 0.01f;
    private Coroutine _volumeCoroutine;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _intruderDetector.OnIntruderDetected += ActivateAlarm;
        _intruderDetector.OnIntruderLeft += DeactivateAlarm;
    }

    private void OnDisable()
    {
        _intruderDetector.OnIntruderDetected -= ActivateAlarm;
        _intruderDetector.OnIntruderLeft -= DeactivateAlarm;
    }

    private void ActivateAlarm()
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }
        
        StartVolumeChange(_maxVolume);
    }

    private void DeactivateAlarm()
    {
        StartVolumeChange(_minVolume);
    }

    private void StartVolumeChange(float targetVolume)
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeAlarmVolume(targetVolume));
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
    {
        while (Mathf.Abs(_audioSource.volume - targetVolume) > _tolerance)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changeSpeed * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume <= _tolerance)
        {
            _audioSource.Stop();
        }
    }
}
