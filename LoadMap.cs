using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
    [SerializeField]
    private int indexNextMap;
    public void LoadNextMap()
    {
        SceneManager.LoadScene(indexNextMap);
    }
}
