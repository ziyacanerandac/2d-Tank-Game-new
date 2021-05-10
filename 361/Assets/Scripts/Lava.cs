using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    public GameObject canvasobj;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            canvasobj.SetActive(true);
            Destroy(other.gameObject);
        }
        else
        {
            FindObjectOfType<DeathScreen>().IncreaseDeadCount();
            Destroy(other.gameObject);
        }
    }
}
