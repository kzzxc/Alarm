using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public class Alarm : MonoBehaviour
{
    private static readonly int AlarmTrigger = Animator.StringToHash("Alarm");
    private static readonly int IdleTrigger = Animator.StringToHash("Idle");
    
    [SerializeField] private House _house;
    [SerializeField] private float _speedOfChanging;

    private Animator _animator;
    private AudioSource _alarmSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _alarmSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _house.PlayerEntred += OnPlayerEntred;
    }

    private void OnDisable()
    {
        _house.PlayerEntred -= OnPlayerEntred;
    }

    private void OnPlayerEntred(bool isEntred)
    {
        StopCoroutine();

        if (isEntred)
        {
            PlayAlarm();
            StartAnimation(AlarmTrigger);
            _currentCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
        }
        else
        {
            StartAnimation(IdleTrigger);
            _currentCoroutine = StartCoroutine(ChangeVolume(_minVolume));
        }
    }

    private void StopCoroutine()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
    }

    private void PlayAlarm()
    {
        if (!_alarmSource.isPlaying)
        {
            _alarmSource.Play();
        }
    }

    private void StartAnimation(int animationName)
    {
        _animator.SetTrigger(animationName);
    }

    private IEnumerator ChangeVolume(float endVolume)
    {
        while (!Mathf.Approximately(endVolume, _alarmSource.volume))
        {
            _alarmSource.volume = Mathf.MoveTowards(_alarmSource.volume, endVolume, _speedOfChanging * Time.deltaTime);
            yield return null;
        }

        if (Mathf.Approximately(_alarmSource.volume, _minVolume))
        {
            _alarmSource.Stop();
        }
    }
}
