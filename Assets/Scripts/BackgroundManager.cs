using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private const int velocity = -7;

    private delegate void State();

    private State state;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        state = Idle;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        state();
    }

    void Idle()
    {
        // Do not anything...
    }

    void Move()
    {
        transform.position += new Vector3(velocity, 0);
    }

    public void StartMove()
    {
        state = Move;
    }

    public void StopMove()
    {
        state = Idle;
    }
}
