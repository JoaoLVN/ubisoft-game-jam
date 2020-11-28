using System.Linq;
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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GAME_STATE State { get { return Instance._state; } }
    public static float TimeLeft { get { return Instance._timeLeft; } }

    private GAME_STATE _state = GAME_STATE.GAME;
    private GAME_STATE _lateState = GAME_STATE.INTRO;

    [SerializeField] private QuestManager _questManager;
    [SerializeField] private float _maxTimer;
    private float _timeLeft;

    /////////////////////////////////////////////////////////

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            GameObject.DestroyImmediate(this);
            return;
        }

        Instance = this;
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
    private void LateUpdate()
    {
        if (_timeLeft == 0)
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
                break;
            case GAME_STATE.GAME:
                _timeLeft = Mathf.Clamp(_timeLeft - Time.deltaTime, 0f, _maxTimer);
                break;
            case GAME_STATE.PAUSE:
                break;
            case GAME_STATE.WON:
                Debug.Log("Won");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case GAME_STATE.FAILED:
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
                break;
            case GAME_STATE.GAME:
                _timeLeft = _maxTimer;
                break;
            case GAME_STATE.PAUSE:
                break;
        }
    }
}