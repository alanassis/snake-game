using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    Queue<Vector2> queue;

    void Awake()
    {
        queue = new Queue<Vector2>();
    }

    public void AddToQueue(Vector2 nextPos)
    {
        queue.Enqueue(nextPos);
    }

    public void SetTickDelay(int delay)
    {
        for (int i = 0; i < delay; i++)
        {
            queue.Enqueue(transform.position);
        }
    }

    public void OnTick()
    {
        Vector2 nextPos = queue.Dequeue();
        transform.position = nextPos;
    }
}