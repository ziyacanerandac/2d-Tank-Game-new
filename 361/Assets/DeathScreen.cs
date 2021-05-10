using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] enemies;
    bool bossIsDead = false;
    int enemiesDead=0, enemiesLeft;
    void Awake() 
    {
        enemies= GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
        Debug.Log(enemiesLeft);
    }
    public void IncreaseDeadCount() 
    {
        enemiesDead++;
        Debug.Log(enemiesLeft);
        Debug.Log("a+1");
    }
    public void LoadNextScene()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex+1);
    }
    public void ResetScene()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void Winner()
    {
        SceneManager.LoadScene(0);

    }
    void Update()
    {
        
        if (enemiesLeft == enemiesDead && bossIsDead==true)
        {
            LoadNextScene();
        }
      

    }
    public void IsDead()
    {
        bossIsDead = true;
        Debug.Log(bossIsDead);
    }
}
