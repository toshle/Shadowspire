using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    float _elapsedTime = 0f;
    [SerializeField] float _remainingTime;
    [SerializeField] bool _countdown = false;
    // Update is called once per frame
    void Update()
    {
        int minutes = 0;
        int seconds = 0;
        if(_countdown)
        {
            if (_remainingTime > 0f)
            {
                _remainingTime -= Time.deltaTime;
            }
            else if (_remainingTime < 0f)
            {
                _remainingTime = 0f;
            }
            minutes = Mathf.FloorToInt(_remainingTime / 60);
            seconds = Mathf.FloorToInt(_remainingTime % 60);
        } else
        {
            _elapsedTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(_elapsedTime / 60);
            seconds = Mathf.FloorToInt(_elapsedTime % 60);
        }

        if (_timerText != null)
        {
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
