using TMPro;
using UnityEngine;

public class ScoreLog : MonoBehaviour
{
    [SerializeField] private GameObject logItemPrefab;
    [SerializeField] private Transform content;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score.Instance.OnScoreChanged += UpdateLog;
    }

    private void OnDestroy()
    {
        if(Score.Instance != null)
        {
            Score.Instance.OnScoreChanged -= UpdateLog;
        }
    }

    // Update is called once per frame
    private void UpdateLog(int totalScore)
    {
        var names = Score.Instance.GetScoreNames();
        var scores = Score.Instance.GetScoreNumbers();

        int lastIndex = names.Count - 1;

        GameObject item = Instantiate(logItemPrefab, content);

        var text = item.GetComponentInChildren<TMP_Text>();
        text.text = $"{names[lastIndex]}:{scores[lastIndex]}“_";
    }
}
