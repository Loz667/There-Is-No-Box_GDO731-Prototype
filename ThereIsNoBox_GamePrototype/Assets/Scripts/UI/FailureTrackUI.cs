using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class FailureTrackUI: UIPanel
{

    [SerializeField] private Transform lockPrefab;
    [SerializeField] private Transform lockContainer;
    List<ContainmentLockUI> lockList = new List<ContainmentLockUI>();

    public void Initialize(int length)
    {
        for (int i = 0; i < length; i++)
        {
            Transform lockObj = Instantiate(lockPrefab, lockContainer);
            ContainmentLockUI lockUI = lockObj.GetComponent<ContainmentLockUI>();
            lockList.Add(lockUI);
        }
    }

    public void Redraw(int failed)
    {
        for (int i = 0; i < failed; i++)
        {
            lockList[i].TurnOff();
        }
    }
    
}
