using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    private delegate void State();
   
    private State state;
    private RexManager rexManager;
    private BackgroundManager backgroundManager;
 
    // Start is called before the first frame update
    void Start()
    {
        rexManager = FindObjectOfType(typeof(RexManager)) as RexManager;
        backgroundManager = FindObjectOfType(typeof(BackgroundManager)) as BackgroundManager;
        state = Ready;
    }

    // Update is called once per frame
    void Update()
    {
        state();
    }

    void Ready()
    {
        if (Input.anyKey)
        {
            Debug.Log("GameState change ReadyState to RunState");
            state = Run;
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rexManager.InputJump();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("start to move background");
            backgroundManager.StartMove();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("stop to move background");
            backgroundManager.StopMove();
        }
    }
}
