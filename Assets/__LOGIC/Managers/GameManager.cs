using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GAME_STATE
{
    START,
    INTRO,
    GAME,
    HELP
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GAME_STATE State { get { return instance._state; } }
    public static float TimeLeft { get { return instance._timeLeft; } }

    private GAME_STATE _state = GAME_STATE.GAME;
    private GAME_STATE _lateState = GAME_STATE.INTRO;

    [SerializeField]
    private float _maxTimer;
    private float _timeLeft;

    /////////////////////////////////////////////////////////

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
            Cursor.visible = false;
    }

    private void Update()
    {
        OnStateUpdate();

        if (_state != _lateState)
            OnStateChanged();

        _lateState = _state;
    }

    /////////////////////////////////////////////////////////

    private void OnStateUpdate()
    {
        switch (_state)
        {
            case GAME_STATE.START:
                break;
            case GAME_STATE.INTRO:
                break;
            case GAME_STATE.GAME:
                _timeLeft = Mathf.Clamp(_timeLeft - Time.deltaTime, 0f, _maxTimer);
                break;
            case GAME_STATE.HELP:
                break;
        }
    }

    private void OnStateChanged()
    {
        switch (_state)
        {
            case GAME_STATE.START:
                break;
            case GAME_STATE.INTRO:
                break;
            case GAME_STATE.GAME:
                _timeLeft = _maxTimer;
                break;
            case GAME_STATE.HELP:
                break;
        }
    }
}