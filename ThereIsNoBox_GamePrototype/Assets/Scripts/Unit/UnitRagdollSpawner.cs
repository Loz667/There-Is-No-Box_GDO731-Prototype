using System;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] Transform ragdollPrefab;
    [SerializeField] Transform originalRootBone;

    HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnHealthDepleted += OnHealthDepleted;
    }

    void OnHealthDepleted(object sender, EventArgs e)
    {
        Transform ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        UnitRagdoll ragdoll = ragdollTransform.GetComponent<UnitRagdoll>();
        ragdoll.Setup(originalRootBone);
    }
}
