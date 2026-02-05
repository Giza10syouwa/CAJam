using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    [SerializeField] private List<int> _scoreNumbers = new List<int>();
    [SerializeField] private List<string> _scoreNames = new List<string>();

    public event Action<int> OnScoreChanged;
    int _time = 0;
    int count = 0;

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

    public void AddScore(int score,string name)
    {
        _scoreNumbers.Add(score);
        _scoreNames.Add(name);
        Debug.Log("âΩÇÇµÇΩÅF" + _scoreNames[count]);
        Debug.Log("éÊìæì_êîÅF" + _scoreNumbers[count]);
        int total = 0; 
        foreach (var s in _scoreNumbers) 
            total += s;
        count++;
        OnScoreChanged?.Invoke(total);
    }


    public List<int> GetScoreNumbers()
    {
        return _scoreNumbers;
    }
    public List<string> GetScoreNames()
    {
        return _scoreNames;
    }

    public void SetTime(int time)
    {
        _time = time;
        _scoreNumbers.Add(_time * 2);
    }
}
