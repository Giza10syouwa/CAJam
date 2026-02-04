using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    public int _score { get;private set; }
    public event Action<int> OnScoreChanged;
    int _time = 0;

    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    public void PlusScore(int score)
    {
        _score += score;
        Debug.Log($"Score: {_score}");
        OnScoreChanged?.Invoke(_score);
    }

    public void MinusScore(int score)
    {
        _score -= score;
        Debug.Log($"Score: {_score}");
        OnScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    { 
        _score = 0;
        Debug.Log($"Score: {_score}");
        OnScoreChanged?.Invoke(_score); 
    }

    public void SetTime(int time)
    {
        _time = time;
        _score += _time * 2;
        Debug.Log($"Score: {_score}");
    }
}
