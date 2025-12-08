using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _bar;
    [SerializeField] private TextMeshProUGUI _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }

    public void SetHealth(float current, float max)
    {
        float barScale = current / max;
        string text = current + "/" + max;
        _bar.transform.localScale = new Vector3(barScale, 1, 1);
        _text.text = text;
    }
}
