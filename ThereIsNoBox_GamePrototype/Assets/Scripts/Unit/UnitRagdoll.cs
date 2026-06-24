using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] Transform rootBone;

    public void Setup(Transform originalRootBone)
    {
        MatchChildTransforms(originalRootBone, rootBone);

        ApplyExplosion(rootBone, 300f, transform.position, 10f);
    }

    void MatchChildTransforms(Transform root, Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform childClone = clone.Find(child.name);
            if (childClone != null)
            {
                childClone.position = child.position;
                childClone.rotation = child.rotation;

                MatchChildTransforms(child, childClone);
            }
        }
    }

    void ApplyExplosion(Transform root, float force, Vector3 position, float range)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRb))
            {
                childRb.AddExplosionForce(force, position, range);
            }

            ApplyExplosion(child, force, position, range);
        }
    }
}
