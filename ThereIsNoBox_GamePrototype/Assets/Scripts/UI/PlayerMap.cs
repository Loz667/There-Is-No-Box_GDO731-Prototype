using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMap : MonoBehaviour
{
    
    public static InputControls Controls;
    
    [SerializeField] private GameObject mapScreen;
    
    void Awake()
    {
        if (Controls != null)
        {
            Destroy(gameObject);
            return;
        }

        Controls = new InputControls();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Controls.Player.Map.triggered)
        {
            Debug.Log("Map triggered");
            ToggleMap();
        }
    }

    private void ToggleMap()
    {
        mapScreen.SetActive(!mapScreen.activeSelf);
    }
    
    
}
