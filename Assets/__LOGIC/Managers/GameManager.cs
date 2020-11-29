using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GAME_STATE
{
    START,
    INTRO,
    GAME,
    PAUSE,
    FAILED,
    WON
}

public class GameManager : SingletonBehaviour<GameManager>
{
    public static GAME_STATE State { get { return Instance._state; } }
    public static float TimeLeft { get { return Instance._timeLeft; } }

    private GAME_STATE _state = GAME_STATE.INTRO;
    private GAME_STATE _lateState = GAME_STATE.START;

    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private float _maxTimer;
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _intro;
    [SerializeField] private GameObject _fail;
    [SerializeField] private GameObject _win;
    private float _timeLeft = 999f;

    /////////////////////////////////////////////////////////

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
    private void LateUpdate()
    {
        if (_timeLeft == 0 || _playerGameObject == null)
        {
            _state = GAME_STATE.FAILED;
        }
        else if (_questManager.Quests.All(x => x.Complete))
        {
            _state = GAME_STATE.WON;
        }
    }

    /////////////////////////////////////////////////////////

    private void OnStateUpdate()
    {
        switch (_state)
        {
            case GAME_STATE.START:
                break;
            case GAME_STATE.INTRO:
                if (Input.GetMouseButtonDown(0))
                    _state = GAME_STATE.GAME;
                break;
            case GAME_STATE.GAME:
                _timeLeft = Mathf.Clamp(_timeLeft - Time.deltaTime, 0f, _maxTimer);
                break;
            case GAME_STATE.PAUSE:
                break;
            case GAME_STATE.WON:
                Debug.Log("Won");
                DOTween.Clear(true);
                if (Input.GetMouseButtonDown(0))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case GAME_STATE.FAILED:
                DOTween.Clear(true);
                if (Input.GetMouseButtonDown(0))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                Time.timeScale = 0f;
                _intro.SetActive(true);
                _hud.SetActive(false);
                _win.SetActive(false);
                _fail.SetActive(false);
                break;
            case GAME_STATE.GAME:
                Time.timeScale = 1f;
                _intro.SetActive(false);
                _hud.SetActive(true);
                _win.SetActive(false);
                _fail.SetActive(false);
                _timeLeft = _maxTimer;
                break;
            case GAME_STATE.PAUSE:
                break;
            case GAME_STATE.FAILED:
                Time.timeScale = 0f;
                SoundManager.Instance.GetComponent<AudioSource>().volume = .1f;
                SoundManager.PlaySound("fail");
                _intro.SetActive(false);
                _hud.SetActive(false);
                _win.SetActive(false);
                _fail.SetActive(true);
                break;
            case GAME_STATE.WON:
                Time.timeScale = 0f;
                SoundManager.Instance.GetComponent<AudioSource>().volume = .1f;
                SoundManager.PlaySound("danke");
                _intro.SetActive(false);
                _hud.SetActive(false);
                _win.SetActive(true);
                _fail.SetActive(false);
                break;
        }
    }
}