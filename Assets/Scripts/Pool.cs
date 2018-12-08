using UnityEngine;
using System.Collections.Generic;

class Pool
{  
    private Stack<GameObject> pool;

    private int count;

    private int max;
    
    public Pool(GameObject ori, int max)
    {
        ori.SetActive(false);
        pool = new Stack<GameObject>();
        pool.Push( ori );

        for ( int i = 1 ; i < max ; i++ )
        {
            pool.Push( GameObject.Instantiate( ori ) );
        }

        this.max = max;
        count = max;
    }

    public GameObject Pop()
    {
        if(count == 1)
        {
            max++;
            GameObject temp = pool.Pop();
            pool.Push( GameObject.Instantiate( temp ) );
            return temp;
        }

        count--;

        return pool.Pop();
    }

    public void Push(GameObject ret)
    {
        count++;
        ret.SetActive( false );
        ret.transform.position = Vector3.zero;
        pool.Push( ret );
    }
}