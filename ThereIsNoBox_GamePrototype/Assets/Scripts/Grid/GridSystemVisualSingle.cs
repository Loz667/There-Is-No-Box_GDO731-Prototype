using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRend;

    public void Show(Material material)
    {
        meshRend.enabled = true;
        meshRend.material = material;
    }

    public void Hide()
    {
        meshRend.enabled = false;
    }
}
