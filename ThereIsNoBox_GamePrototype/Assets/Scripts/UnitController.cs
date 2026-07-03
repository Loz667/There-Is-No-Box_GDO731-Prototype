using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    
    [SerializeField] NavMeshAgent agent;

    void Update()
    {
        HandleTestInput();
        if (InteractWithUI()) return;

        
        if (InteractWithObject()) return;

        MoveToCursor();
        //UpdateAnimator();
        //HandleInteraction(); //TODO rename to something SELECT ?

    }
    
    private void HandleTestInput()
    {
        if (InputHandler.Controls.Player.Test.triggered)
        {
            Debug.Log("Test key pressed");
            Game.UI.TogglePuzzleView();
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
        if (InputHandler.Controls.Player.Map.triggered)
        {
            Game.UI.ToggleMapView();
        }

        if (EventSystem.current.IsPointerOverGameObject()) return true;
        return false;
    }
    
        
    private bool InteractWithObject()
    {
        if (InputHandler.Controls.Player.Interact.triggered)
        {
           Debug.Log("Interact triggered");
            Ray ray = InputHandler.GetMouseRay();
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.Log("Got a hit");
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
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        if (InputHandler.Controls.Player.Interact.triggered)
        {
            Ray ray = InputHandler.GetMouseRay();
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
    
}
