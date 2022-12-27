using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LoadNextMap();
        }
    }
    public void LoadNextMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
