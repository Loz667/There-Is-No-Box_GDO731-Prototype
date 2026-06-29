using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform projectileSpawn;

    const string MOVE_ANIM = "IsMoving";
    const string AIM_ANIM = "IsAiming";
    const string SHOOT_ANIM = "Shoot";

    private void Awake()
    {
        if (TryGetComponent(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += OnStartMoving;
            moveAction.OnStopMoving += OnStopMoving;
        }

        if (TryGetComponent(out ShootAction shootAction))
        {
            shootAction.OnStartAim += OnStartAim;
            shootAction.OnStopAim += OnStopAim;
            shootAction.OnShoot += OnShoot;
        }
    }

    private void OnStartMoving(object sender, EventArgs e)
    {
        anim.SetBool(MOVE_ANIM, true);
    }

    private void OnStopMoving(object sender, EventArgs e)
    {
        anim.SetBool(MOVE_ANIM, false);
    }

    private void OnStartAim(object sender, EventArgs e)
    {
        anim.SetBool(AIM_ANIM, true);
    }

    private void OnStopAim(object sender, EventArgs e)
    {
        anim.SetBool(AIM_ANIM, false);
    }

    private void OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        anim.SetTrigger(SHOOT_ANIM);

        Transform projectileTransform = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        Projectile projectile = projectileTransform.GetComponent<Projectile>();

        Vector3 shootAtTargetPosition = e.targetUnit.GetWorldPosition();

        shootAtTargetPosition.y = projectileSpawn.position.y;

        projectile.Setup(shootAtTargetPosition);
    }
}
