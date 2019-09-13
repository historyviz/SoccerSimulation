using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoccerPlayerController : MonoBehaviour
{
    [Tooltip("Parameter that controls running animation. Should be a Boolean type.")]
    public string runningParameter;

    [Tooltip("Parameter that controls tackling animation. Should be a Trigger type.")]
    public string tackleParameter;

    // Reference to the animator and agent
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    // Abstract running animation state. Using IsRunning guaruntees animation is correct.
    private bool isRunning = false;
    public bool IsRunning
    {
        get { return isRunning; }
        set
        {
            isRunning = value;
            animator.SetBool(runningParameter, isRunning);
        }
    }

    // Abstract agent destination so other classes only see SoccerPlayerComtroller.Target
    public Vector3 Target
    {
        get
        {
            return navMeshAgent.destination;
        }
        set
        {
            navMeshAgent.destination = value;
        }
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();

    }

    public void StartTackle()
    {
        animator.SetTrigger(tackleParameter);
        IsRunning = false;
    }

    public void FaceDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
