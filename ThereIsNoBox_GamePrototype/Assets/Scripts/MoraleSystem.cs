using System;
using UnityEngine;

public class MoraleSystem : MonoBehaviour
{
    public event EventHandler OnMoraleDepleted;

    [SerializeField] int morale = 6;
    int maxMorale;

    void Awake()
    {
        maxMorale = morale;
    }

    public void DamageMorale(int damageAmount)
    {
        morale -= damageAmount;

        if (morale < 0)
        {
            morale = 0;
        }

        if (morale == 0)
        {
            MoraleDepleted();
        }
    }

    public int GetMorale()
    {
        return morale;
    }

    void MoraleDepleted()
    {
        OnMoraleDepleted?.Invoke(this, EventArgs.Empty);
    }
}
