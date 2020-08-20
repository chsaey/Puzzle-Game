using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Access gm from anywhere
    public static GameManager gm;

    public string nextLevel;
    public int keys;

    //UI elements
    public Text keysRemaining;
    public Text level;
    public Text firstSolve;
    public Text fastestSolve;
    public GameObject winScreen;

    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        keys = transform.childCount;
        keysRemaining.text = "Keys: " + keys.ToString();
        scene = SceneManager.GetActiveScene();
        level.text = scene.name;
    }
    void Update()
    {
        keysRemaining.text = "Keys: " + keys.ToString();
        if (keys == 0)
        {
            Win();
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(level.text);
   
    }
    public void NextLevel()
    {

        SceneManager.LoadScene(nextLevel);
 

    }

    public void MainMenu() {

        SceneManager.LoadScene("MainMenu");


    }
    void Win()
    {
        StartCoroutine(Wait());

    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
        winScreen.SetActive(true);
    }


}
