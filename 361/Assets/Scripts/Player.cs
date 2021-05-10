using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    //public DeathScreen a;
    public GameObject canvasobj;
   

    [Header("Projectile")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] float shootSoundVol = 0.3f;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileFiringPeriod = 1f;
    float xSpeed, ySpeed;

    [Header("Player")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int Health = 100;

    bool isMoving = false;
    float moveH, moveV;
    
    // Update is called once per frame
    void Awake()
    {
        canvasobj.SetActive(false);
    }
    void FixedUpdate()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

            GameObject missile = Instantiate(missilePrefab, pos, rotation) as GameObject;
            missile.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVol);
        }
    }

    private void Move()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV= Input.GetAxisRaw("Vertical");
       
        if(moveH != 0)
        {
            
            MoveHorizontal();  
        }
        else if(moveV != 0)
        {
           
            MoveVertical();
        }
        
    }
    
    public void MoveHorizontal()
    {
        //Debug.Log("Sağa Solaa");

        transform.rotation = Quaternion.Euler(0f, 0f, -moveH * 90f);

        var deltaX = moveH * Time.deltaTime * moveSpeed;

        var newXPos = transform.position.x + deltaX;

        transform.position = new Vector2(newXPos, transform.position.y);

    }
    public void MoveVertical()
    {
        Quaternion rotation;

        if (moveV < 0)
        {
            rotation = Quaternion.Euler(0, 0, moveV * 180f);
        }
        else
        {
            rotation = Quaternion.Euler(0, 0, 0);
        }
        transform.rotation = rotation;

        var deltaY = moveV * Time.deltaTime * moveSpeed;

        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(transform.position.x, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        canvasobj.SetActive(true);
        Destroy(gameObject);
       
    }
   

}
