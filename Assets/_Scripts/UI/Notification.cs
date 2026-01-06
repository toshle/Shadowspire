using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] float _time = 10f;
    float _elapsedTime = 0f;
    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if( _elapsedTime > _time )
        {
            DestroyImmediate(gameObject);
        }
    }
}
