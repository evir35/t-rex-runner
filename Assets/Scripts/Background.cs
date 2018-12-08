using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private int velocity = -70;

    [SerializeField]
    private int startY = -160;

    [SerializeField]
    private int backgroundWidth = 1196;

    [SerializeField]
    private int backgroundCount = 2;

    [SerializeField]
    private Sprite backgroundSprite;

    private enum State
    {
        IDLE,
        MOVE
    }

    private State state;
    private List<GameObject> backgroundObjects;

    // Start is called before the first frame update
    void Start()
    {
        state = State.IDLE;
        backgroundObjects = new List<GameObject>();
        createBackgrounds(backgroundCount);
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.MOVE:
                move();
                break;
        }
    }

    private void createBackgrounds(int count)
    {
        GameObject temp;
        for(int i=0;i<count;i++)
        {
            temp = new GameObject("background");
            temp.AddComponent<SpriteRenderer>();
            temp.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
            temp.transform.localScale = new Vector3(100, 100);
            temp.transform.position = new Vector3(i * backgroundWidth, startY);
            backgroundObjects.Add(temp);
        }
    }

    private void move()
    {
        foreach(GameObject background in backgroundObjects)
        {
            background.transform.position += new Vector3(velocity * Time.deltaTime, 0);
            
            if(background.transform.position.x < -backgroundWidth)
            {
                background.transform.position += new Vector3(backgroundWidth * 2, 0);
            }
        }
    }

    public void StartMove()
    {
        state = State.MOVE;
    }

    public void StopMove()
    {
        state = State.IDLE;
    }
}
