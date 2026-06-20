using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;

    public void Show()
    {
        mesh.enabled = true;
    }

    public void Hide()
    {
        mesh.enabled = false;
    }
}
