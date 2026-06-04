using UnityEngine;

public class FloatItem : MonoBehaviour
{
    Vector3 startPos;

    public float height = 0.15f;
    public float speed = 2f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position =
            startPos +
            Vector3.up *
            Mathf.Sin(Time.time * speed) *
            height;
    }

}