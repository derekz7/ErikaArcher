using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private string mapName;

    [SerializeField]
    private string description;

    public string MapName()
    {
        return mapName;
    }
    public string Description()
    {
        return description;
    }
}
