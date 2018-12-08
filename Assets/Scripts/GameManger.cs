using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private Rex rex;

    [SerializeField]
    private Background background;

    [SerializeField]
    private Obstacle obstacle;

    [SerializeField]
    private float cactusDefaultGenTime = 1;

    [SerializeField]
    private float cactusAdditionalGenTime = 3;

    private Coroutine generateCactusCoroutine;

    private enum State
    {
        READY,
        RUN,
        END
    }
    
    private State state;
   
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        state = State.READY;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.READY:
                Ready();
                break;
            case State.RUN:
                Run();
                break;
            case State.END:
                End();
                break;
        }
    }

    void Ready()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            state = State.RUN;
            generateCactusCoroutine = StartCoroutine(generateCactus());
            background.StartMove();
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rex.InputJump();
        }
    }

    void End()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            state = State.READY;
            rex.Initialize();
            obstacle.Initialize();
        }
    }

    private IEnumerator generateCactus()
    {
        while(state == State.RUN)
        {
            obstacle.GenerateCactus(Random.Range(0, 4));
            yield return new WaitForSeconds(cactusDefaultGenTime + Random.Range(0.0f, cactusAdditionalGenTime));
        }
    }

    public void StopGame()
    {
        state = State.END;
        background.StopMove();
        obstacle.StopCactuses();
        StopCoroutine(generateCactusCoroutine);
    }
}
