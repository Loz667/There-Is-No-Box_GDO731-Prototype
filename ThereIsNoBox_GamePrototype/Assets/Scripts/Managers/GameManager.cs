using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public BaseNode currentNode;

    private void Awake()
    {
        instance = this;
    }
}
