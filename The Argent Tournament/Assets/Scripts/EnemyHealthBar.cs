using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class EnemyHealthBar : Bar
{
    private Name _enemyName;

    private void Start()
    {
        _enemyName = GetComponentInChildren<Name>();
        InitializeIndication();
        Increase(MaxAmount);
    }

    public void Refresh(float maxAmount, string newName)
    {
        this.MaxAmount = maxAmount;
        _enemyName.SetName(newName);
        Increase(MaxAmount);
    }

    public bool TryToKill(float amount)
    {
        var remainingHP = Decrease(amount);
        if (remainingHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
