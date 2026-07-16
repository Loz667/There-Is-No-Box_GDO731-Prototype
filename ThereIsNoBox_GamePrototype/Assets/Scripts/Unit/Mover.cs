using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    
    //Source: https://github.com/UnityRPGv2/RPG/blob/master/RPG%20Project/Assets/Scripts/Movement/Mover.cs
    
    private static readonly int ZSpeed = Animator.StringToHash("zSpeed");

    private NavMeshAgent agent;
    private Animator animator;
    private Character character;

    private readonly float _maxSpeed = 6f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
        
    }

    void Update()
    {
        agent.enabled = character.Status == Character.CharacterState.Alive;
        UpdateAnimator();
    }

    public bool CanMoveTo(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
        if (!hasPath) return false;
        if (path.status != NavMeshPathStatus.PathComplete) return false;

        return true;
    }

    public void MoveTo(Vector3 destination)
    {
        _ = MoveToAsync(destination);
    }
    
    public async Task<bool> MoveToAsync(Vector3 destination, float arrivalThreshold = 1.2f)
    {
        agent.destination = destination;
        agent.speed = _maxSpeed;
        agent.isStopped = false;

        while (agent.pathPending) await Task.Yield();
        await Task.Yield();
        while (true)
        {
            if (agent == null || !agent.enabled) return false;

            float flatDistance = Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(destination.x, 0, destination.z)
            );

            if (flatDistance <= arrivalThreshold) return true;
            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
            {
                return agent.remainingDistance <= arrivalThreshold;
            }

            await Task.Yield();
        }
    }

    public void WarpToPoint(Vector3 target)
    {
        agent.ResetPath();
        agent.Warp(target);
    }

    private void UpdateAnimator()
    {
        if(animator == null) animator = GetComponentInChildren<Animator>();
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat(ZSpeed, speed);
    }
}
