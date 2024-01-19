using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrollSate : StateMachineBehaviour
{
    float timer;
    Transform Player;
    float chaseRange = 8;
    List<Transform> WayPoints = new List<Transform>();
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 2f;
        timer = 0;
        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
        foreach (Transform t in go.transform)
            WayPoints.Add(t);
        agent.SetDestination(WayPoints[Random.Range(0, WayPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Patrol State Update");

        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(WayPoints[Random.Range(0, WayPoints.Count)].position);

        timer += Time.deltaTime;
        if (timer < 5)
            animator.SetBool("IsPatrolling", false);

        float distance = Vector3.Distance(Player.position, animator.transform.position);
        Debug.Log("Distance to Player: " + distance);

        if (distance < chaseRange)
        {
            Debug.Log("Chasing Player");
            animator.SetBool("IsChasing", true);
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
