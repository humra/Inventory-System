using UnityEngine;
using UnityEngine.Analytics;

public class ColissionAnalytics : MonoBehaviour
{
    private float _timeStamp;

    [SerializeField]
    private float _timeout = 5f;

    private void Start()
    {
        _timeStamp = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Time.time + _timeout >= _timeStamp)
        {
            _timeStamp = Time.time;

            Analytics.CustomEvent("Player collided with collider");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time + _timeout >= _timeStamp)
        {
            _timeStamp = Time.time;

            Analytics.CustomEvent("Player collided with trigger collider");
        }
    }
}
