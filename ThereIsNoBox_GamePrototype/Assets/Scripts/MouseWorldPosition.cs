using UnityEngine;
using UnityEngine.Rendering;

public class MouseWorldPosition : MonoBehaviour
{
    private static MouseWorldPosition instance;

    [SerializeField] LayerMask floorLayer;

    void Awake()
    {
        instance = this;
    }

    public static Vector3 GetCurrentPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, float.MaxValue, instance.floorLayer);
        return hit.point;
    }
}
