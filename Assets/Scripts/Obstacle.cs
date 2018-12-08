using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private int poolSize = 20;

    [SerializeField]
    private int cactusWidth = 25;

    [SerializeField]
    private int startY = -140;

    [SerializeField]
    private int startX = 500;

    [SerializeField]
    private int endX = -500;

    [SerializeField]
    private int velocity = -200;

    [SerializeField]
    private Sprite[] cactusSprites = new Sprite[3];

    private Pool[] cactusPools = new Pool[3];

    private void Start()
    {
        CreateCactusPool();
    }

    private void Update()
    {
        
    }

    private void CreateCactusPool()
    {
        GameObject temp;
        for(int i=0;i<3;i++)
        {
            temp = new GameObject("cactus");
            temp.tag = "cactus";
            temp.AddComponent<SpriteRenderer>();
            temp.GetComponent<SpriteRenderer>().sprite = cactusSprites[i];
            temp.AddComponent<BoxCollider2D>();
            temp.GetComponent<BoxCollider2D>().isTrigger = true;
            temp.AddComponent<Rigidbody2D>();
            temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            temp.AddComponent<Cactus>();
            temp.GetComponent<Cactus>().kind = i;
            temp.transform.localScale = new Vector3(100, 100);
            cactusPools[i] = new Pool(temp, poolSize);
        }
    }

    public void GenerateCactus(int count)
    {
        int kind;
        GameObject cactus;
        for(int i=0;i<count;i++)
        {
            kind = Random.Range(0, 3);
            cactus = cactusPools[kind].Pop();
            cactus.SetActive(true);
            cactus.transform.position = new Vector3(startX + i * cactusWidth, startY);
            StartCoroutine(MoveCactus(cactus, kind));
        }
    }

    public void Initialize()
    {
        foreach(GameObject cactus in GameObject.FindGameObjectsWithTag("cactus"))
        {
            cactusPools[cactus.GetComponent<Cactus>().kind].Push(cactus);
        }
    }

    public void StopCactuses()
    {
        StopAllCoroutines();
    }

    private IEnumerator MoveCactus(GameObject cactus, int kind)
    {
        while(cactus.transform.position.x > endX)
        {
            cactus.transform.position += new Vector3(velocity * Time.deltaTime, 0);
            yield return null;
        }

        cactusPools[kind].Push(cactus);
    }
}