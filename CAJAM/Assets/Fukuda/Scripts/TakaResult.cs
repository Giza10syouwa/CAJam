using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TakaResult : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreT;

    private Score _score;

    [SerializeField]
    private TextMeshProUGUI _scoreBoard;

    [SerializeField]
    private GameObject _socrea;

    private int _size;

    [SerializeField]
    private int _currentPos;

    [SerializeField]
    private int _initialPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_socrea)
            _initialPos = (int)_socrea.transform.localPosition.y;

        _currentPos = 0;
        GameObject t = GameObject.Find("Score");
        if(t)
        _score = t.GetComponent<Score>();
        if(_score)
        _scoreT.text = "ÉXÉRÉA : " + _score.GetScore().ToString();
        if(_scoreBoard && t)
        {
            _scoreBoard.text = "ñæç◊èë\n";
            _scoreBoard.text += "--------------------------------------------------------------------------------------------------\n";

            for (int i = 0; i < _score.GetScoreNumbers().Count; i++)
            {
                _scoreBoard.text += _score.GetScoreNames()[i] + " : " + _score.GetScoreNumbers()[i].ToString() + "\n";
            }

            _size = Mathf.Max(_score.GetScoreNumbers().Count - 10, 0);

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject boss = GameObject.FindWithTag("Boss");
            if(boss)
            GameObject.Destroy(boss);
            SceneManager.LoadScene("TakaTitleScene");
        }
        if(Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if(_currentPos >= 0)
            {
                _currentPos -= (int)_scoreBoard.fontSize;
            }
            _socrea.transform.localPosition = new Vector3( _socrea.transform.localPosition.x,_initialPos + _currentPos, _socrea.transform.localPosition.z);

        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (_currentPos <= _initialPos + _size * (int)_scoreBoard.fontSize)
            {
                _currentPos += (int)_scoreBoard.fontSize;
            }
            _socrea.transform.localPosition = new Vector3(_socrea.transform.localPosition.x,_initialPos + _currentPos, _socrea.transform.localPosition.z);

        }



    }
}
