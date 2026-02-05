using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TakaResult : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreT;

    private Score _score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject t = GameObject.Find("Score");
        if(t)
        _score = t.GetComponent<Score>();
        if(_score)
        _scoreT.text = _score.GetScore().ToString();
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

    }
}
