using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/*Level loader boss ölünce çalışacak*/

public class Boss : movement
{
    Rigidbody2D rb2d;
    float h, v;
    [SerializeField]
    LayerMask blockingLayer;
    enum Direction { Up, Down, Left, Right };
    // Start is called before the first frame update

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float projectileFiringPeriod = 1f;
    [SerializeField] int waitingTime = 8;
    [SerializeField] int health = 5;
    float timer = 0f;
    float xSpeed, ySpeed;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        health--;
        if(health <= 0)
        {
            FindObjectOfType<DeathScreen>().IsDead();

            // var currentIndex = SceneManager.GetActiveScene().buildIndex;
            //  SceneManager.LoadScene(currentIndex + 1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    private void Fire()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        float z = transform.rotation.z;

        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        if (z == 0f) //up
        {
            pos.y += 5f;
            xSpeed = 0f;
            ySpeed = projectileSpeed;
            rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (z == 1) //down
        {
            pos.y += -5f;
            xSpeed = 0f;
            ySpeed = -projectileSpeed;
            rotation = Quaternion.Euler(0, 0, -180f);
        }

        else if (-0.6 >= z && z >= -0.8f) //right
        {
            pos.x += 5f;
            xSpeed = projectileSpeed;
            ySpeed = 0f;
            rotation = Quaternion.Euler(0, 0, -90f);
        }

        else if (0.6 <= z && z <= 0.8) //left
        {
            pos.x += -5f;
            xSpeed = -projectileSpeed;
            ySpeed = 0f;
            rotation = Quaternion.Euler(0, 0, 90f);
        }

        GameObject missile = Instantiate(laserPrefab, pos, rotation) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitingTime)
        {
            Fire();
            timer = 0;
        }
    }
}
