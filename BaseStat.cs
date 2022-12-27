using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    public List<StatBonus> BaseAdditives { get; set; }
    public int BaseValue { get; set; }
    public string statName { get; set; }
    public string StatDiscription { get; set; }
    public int finalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDiscription)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.BaseValue = baseValue;
        this.statName = statName;
        this.StatDiscription = statDiscription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(statBonus);
    }

    public int GetCalculatedStatValue()
    {
        this.BaseAdditives.ForEach(x => this.finalValue += x.BonusValue);
        finalValue += BaseValue;
        return finalValue;
    }


}
