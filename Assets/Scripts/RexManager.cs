using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RexManager : MonoBehaviour
{
    private const int startY = -180;
    private const int acc = 1;
    private const int initVelocity = 15;

    private delegate void State();

    private State state;
    private int velocity;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        state = Idle;
        velocity = initVelocity;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        state();
    }

    void Idle()
    {
        // Do not anything....
    }

    void Jump()
    {
        Debug.Log(transform.position.y);
        if (transform.position.y + velocity >= startY)
        {
            transform.position += new Vector3(0, velocity);
            velocity -= acc;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, startY);
            velocity = initVelocity;
            state = Idle;
        }
    }

    public void InputJump()
    {
        if(state != Jump)
        {
            Debug.Log("Change idle to jump");
            state = Jump;
        }
    }
}
