using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : movement
{
    Rigidbody2D rb2d;
    float h, v;
    [SerializeField]
    LayerMask blockingLayer;
    enum Direction { Up, Down, Left, Right };
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        RandomDirection();
    }
    public void RandomDirection()
    {
        CancelInvoke("RandomDirection");

        List<Direction> lottery = new List<Direction>();
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(1, 0), blockingLayer))
        {
            lottery.Add(Direction.Right);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(-1, 0), blockingLayer))
        {
            lottery.Add(Direction.Left);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, 1), blockingLayer))
        {
            lottery.Add(Direction.Up);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, -1), blockingLayer))
        {
            lottery.Add(Direction.Down);
        }

        Direction selection = lottery[Random.Range(0, lottery.Count)];
        if (selection == Direction.Up)
        {
            v = 1;
            h = 0;
        }
        if (selection == Direction.Down)
        {
            v = -1;
            h = 0;
        }
        if (selection == Direction.Right)
        {
            v = 0;
            h = 1;
        }
        if (selection == Direction.Left)
        {
            v = 0;
            h = -1;
        }
        Invoke("RandomDirection", Random.Range(3, 6));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomDirection();
    }

    private void FixedUpdate()
    {
        if (v != 0 && isMoving == false) StartCoroutine(MoveVertical(v, rb2d));
        else if (h != 0 && isMoving == false) StartCoroutine(MoveHorizontal(h, rb2d));
    }

// Update is called once per frame
void Update()
    {
        
    }
}
