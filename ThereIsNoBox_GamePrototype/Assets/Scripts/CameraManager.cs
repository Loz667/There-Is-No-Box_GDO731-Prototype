using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject actionCamera;

    void Start()
    {
        BaseAction.OnAnyActionStarted += OnActionStarted;
        BaseAction.OnAnyActionCompleted += OnActionCompleted;

        HideActionCamera();
    }

    void OnActionStarted(object sender, EventArgs e)
    {
        //switch (sender)
        //{
        //    case ShootAction shootAction:
        //        Unit shootingUnit = shootAction.GetUnit();
        //        Unit targetUnit = shootAction.GetTargetUnit();

        //        Vector3 characterCamHeight = Vector3.up * 1.7f;

        //        Vector3 shootDirection = (targetUnit.GetWorldPosition() - shootingUnit.GetWorldPosition()).normalized;

        //        float shoulderOffset = 0.5f;
        //        Vector3 cameraShoulderOffset = Quaternion.Euler(0, 90, 0) * shootDirection * shoulderOffset;

        //        Vector3 actionCamPosition = shootingUnit.GetWorldPosition() + characterCamHeight + cameraShoulderOffset + (shootDirection * -1);

        //        actionCamera.transform.position = actionCamPosition;
        //        actionCamera.transform.LookAt(targetUnit.GetWorldPosition() + characterCamHeight);

        //        ShowActionCamera();
        //        break;
        //}
    }

    void OnActionCompleted(object sender, EventArgs e)
    {
        //switch (sender)
        //{
        //    case ShootAction shootAction:
        //        HideActionCamera();
        //        break;
        //}
    }

    void ShowActionCamera()
    {
        actionCamera.SetActive(true);
    }

    void HideActionCamera()
    {
        actionCamera.SetActive(false);
    }
}
