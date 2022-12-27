using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public int id;
    public bool isActive;
    public bool isCompleted;
    public string title;
    public string description;
    public int experieneReward;
    public int goldReward;

    public QuestGoal goal;

    public void Complete()
    {
        isCompleted = true;
        isActive = false;
    }
}
