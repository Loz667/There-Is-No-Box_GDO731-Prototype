using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    void Update()
    {
        HandleTestInput();
        if (InteractWithUI()) return;

        
        if (InteractWithObject()) return;

        if (MoveToCursor()) return;
        //UpdateAnimator();
        //HandleInteraction(); //TODO rename to something SELECT ?

    }
    
    private void HandleTestInput()
    {
        if (InputHandler.Controls.Player.Test.triggered)
        {
            Game.UI.ToggleMapView2();
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
                Debug.Log("Got a hit: " + hit.transform.name);
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

    private bool MoveToCursor()
    {
        if (InputHandler.Controls.Player.Interact.triggered)
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if (Game.Director.ActiveCharacter == null)
                {
                    Debug.LogError("No active character");
                    return false;
                }
                Mover movable = Game.Director.ActiveCharacter.GetComponent<Mover>();
                if (!movable.CanMoveTo(target)) return false;
                
                movable.MoveTo(target);
                return true;
            }

            
            /*
            Ray ray = InputHandler.GetMouseRay();
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                // Move the player to the hit point
                //NavMeshAgent agent = UnitActionSystem.Instance.GetSelectedUnit().GetComponent<NavMeshAgent>();
                //agent.SetDestination(hit.point);
                Mover movable = Game.Director.ActiveCharacter.GetComponent<Mover>();
                if(!movable.CanMoveTo(hit.point))
            }
            */
        }
        return false;
    }
    
    private bool RaycastNavMesh(out Vector3 target)
    {
        target = new Vector3();

        RaycastHit hit;
        bool hasHit = Physics.Raycast(InputHandler.GetMouseRay(), out hit);
        if (!hasHit) return false;

        NavMeshHit navMeshHit;
        bool hasCastToNavMesh = NavMesh.SamplePosition(
            hit.point, out navMeshHit, 1f, NavMesh.AllAreas);
        if (!hasCastToNavMesh) return false;

        target = navMeshHit.position;

        return true;
    }

    //private void UpdateAnimator()
    //{
    //    Vector3 velocity = agent.velocity;
    //    Vector3 localVelocity = transform.InverseTransformDirection(velocity);
    //    float speed = Mathf.Abs(localVelocity.z);
    //    agent.GetComponentInChildren<Animator>().SetFloat("zSpeed", speed);
    //}

}
