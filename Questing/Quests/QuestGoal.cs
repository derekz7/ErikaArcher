
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredIdEnemy;
    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }
    public void EnemyKilled(int id)
    {
        if (goalType == GoalType.Kill && requiredIdEnemy == id )
        {
            currentAmount++;
        }
    }
    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
        {
            currentAmount++;
        }
    }
}

    public enum GoalType
    {
        Kill,
        Gathering
    }

