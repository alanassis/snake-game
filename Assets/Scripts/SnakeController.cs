using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public SnakeBody piecePrefab;
    public Vector2 dir;

    private List<SnakeBody> pieces = new List<SnakeBody>();
    private bool isDead = false;

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (dir != Vector2.down)
            {
                dir = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (dir != Vector2.left)
            {
                dir = Vector2.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (dir != Vector2.up)
            {
                dir = Vector2.down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (dir != Vector2.right)
            {
                dir = Vector2.left;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        MoveController();

        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].AddToQueue(transform.position);
            pieces[i].OnTick();
        }
    }

    void MoveController()
    {
        float nextX = transform.position.x + dir.x;
        float nextY = transform.position.y + dir.y;

        for (int i = 0; i < pieces.Count; i++)
        {
            if (nextX == pieces[i].transform.position.x)
            {
                if (nextY == pieces[i].transform.position.y)
                {
                    StartCoroutine(AnimDeath());
                }
            }
        }

        if (nextX == -11)
        {
            transform.position = new Vector3(10f, transform.position.y, 0f);
        }
        else if (nextX == 11)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0f);
        }
        else if (nextY == 11)
        {
            transform.position = new Vector3(transform.position.x, -10f, 0f);
        }
        else if (nextY == -11)
        {
            transform.position = new Vector3(transform.position.x, 10f, 0f);
        }
        else
        {
            transform.Translate(dir);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        SnakeBody newBody = Instantiate<SnakeBody>(piecePrefab, transform.position, Quaternion.identity);
        newBody.SetTickDelay(pieces.Count + 1);

        pieces.Add(newBody);

        Fruit._instance.OnPick();
    }

    void Respawn()
    {
        isDead = false;

        transform.position = Vector2.zero;
        transform.rotation = Quaternion.identity;

        dir = Vector2.zero;

        for (int j = 0; j < pieces.Count; j++)
        {
            Destroy(pieces[j].gameObject);
        }
        pieces.Clear();

        Fruit._instance.OnDeath();
    }

    IEnumerator AnimDeath()
    {
        isDead = true;

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.1f);

            for (int j = 0; j < pieces.Count; j++)
            {
                pieces[j].transform.Rotate(Vector3.forward * 4f);
            }

            transform.Rotate(Vector3.forward * 4f);
        }

        Respawn();
    }
}