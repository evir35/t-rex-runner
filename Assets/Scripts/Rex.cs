using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rex : MonoBehaviour
{
    [SerializeField]
    private GameManger gameManger;

    [SerializeField]
    private int startX = -200;

    [SerializeField]
    private int startY = -140;

    [SerializeField]
    private int acc = 800;

    [SerializeField]
    private int initVelocity = 400;
    
    private enum State
    {
        Run,
        Jump
    }

    private float velocity;
    private Transform transform;
    private Coroutine jumpCoroutine;
    private State state;

    // Start is called before the first frame update
    private void Start()
    {
        velocity = initVelocity;
        transform = GetComponent<Transform>();
        jumpCoroutine = null;
        state = State.Run;
    }

    // Update is called once per frame
    private void Update()
    {
        switch(state)
        {
        case State.Jump:
            Jump();
            break;
        }
    }

    private void Jump()
    {
        if(transform.position.y + velocity * Time.deltaTime >= startY)
        {
            transform.position += new Vector3(0, velocity * Time.deltaTime);
            velocity -= acc * Time.deltaTime;
        }
        else 
        {
            transform.position = new Vector3(transform.position.x, startY);
            velocity = initVelocity;
            state = State.Run;
        }
    }

    public void InputJump()
    {
        if(state != State.Jump)
        {
            state = State.Jump;
        }
    }

    public void Initialize()
    {
        transform.position = new Vector3(startX, startY);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        gameManger.StopGame();
        state = State.Run;
    }
}
