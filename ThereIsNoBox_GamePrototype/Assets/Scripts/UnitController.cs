using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public static InputControls Controls;
    [SerializeField] float raycastRadius = 1f;

    [SerializeField] NavMeshAgent agent;
    
    void Awake()
    {
        if (Controls != null)
        {
            Destroy(gameObject);
            return;
        }

        Controls = new InputControls();
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable() => Controls.Enable(); 

    private void OnDisable() => Controls.Disable();
        
    private void OnDestroy()
    {
        if (Controls != null)
        {
            Controls.Dispose();
            Controls = null;
        }
    }


    void Update()
    {
        HandleTestInput();
        if (InteractWithUI()) return;

        
        if (InteractWithObject()) return;

        //MoveToCursor();
        UpdateAnimator();
        //HandleInteraction(); //TODO rename to something SELECT ?

    }
    
    private void HandleTestInput()
    {
        if (Controls.Player.Test.triggered)
        {
            Debug.Log("Test key pressed");
        }
        
        //if (InputHandler.Controls.Player.Target.triggered)
        
        {
            /*
            Debug.Log("Test output:");
            foreach (var obj in GM.Grid.Interactables)
            {
                Debug.Log($"{obj.Value} is at {obj.Key.ToString()}");
            }
            */
        }
            
    }
    
    private bool InteractWithUI()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return true;
        return false;
    }
    
        
    private bool InteractWithObject()
    {
        if (Controls.Player.Interact.triggered)
        {
           Debug.Log("Interact triggered");
            Ray ray = GetMouseRay();
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.Log("Got a hit");
                // 2. Check if whatever we clicked has an IRaycastable component
                IRaycastable raycastable = hit.transform.GetComponent<IRaycastable>();
                if (raycastable != null)
                {
                    raycastable.HandleRaycast();
                    return true;
                }
            }
        }

        return false;
    }

    private void MoveToCursor()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = GetMouseRay();
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                // Move the player to the hit point
                agent.SetDestination(hit.point);
            }
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = Mathf.Abs(localVelocity.z);
        agent.GetComponentInChildren<Animator>().SetFloat("zSpeed", speed);
    }
    
    RaycastHit[] RaycastAllSorted()
    {
        RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
        float[] distances = new float[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            distances[i] = hits[i].distance;
        }
        Array.Sort(distances, hits);
        return hits;
    }
    
    public static Ray GetMouseRay() => Camera.main.ScreenPointToRay(MousePosition());
    public static Vector3 MousePosition() => Mouse.current.position.ReadValue();
    
}
