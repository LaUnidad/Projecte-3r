using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Pada1.BBCore; // Code attributes
using Pada1.BBCore.Tasks; // TaskStatus
using BBUnity.Actions; // actions with "gameobject"

[Action("MyActions/FindPatrolPoint")] // name of the action for BB engine
[Help("Returns the nearest patrol point")]

public class FindPatrolPoint : GOAction
{

    [InParam("patrolPoint1")]
    public Transform m_PatrolPosition1;
    [InParam("patrolPoint2")]
    public Transform m_PatrolPosition2;
    [InParam("patrolPoint3")]
    public Transform m_PatrolPosition3;
    [InParam("patrolPoint4")]
    public Transform m_PatrolPosition4;

    [InParam("patrolPointsNumber")]
    public int m_PatrolPointsNumber;

    private List<Transform> m_PatrolPositions;
    private NavMeshAgent m_NavMeshAgent;
    int m_CurrentPatrolPositionId = -1;

    // BB engine OnStart method (equivalent to Start of MonoBehaviours)
    public override void OnStart()
    {
        base.OnStart();
        m_NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        m_PatrolPositions.Add(m_PatrolPosition1);
        m_PatrolPositions.Add(m_PatrolPosition2);
        m_PatrolPositions.Add(m_PatrolPosition3);
        m_PatrolPositions.Add(m_PatrolPosition4);
    }

    // BB engine OnUpdate method
    public override TaskStatus OnUpdate()
    {
        if (!m_NavMeshAgent.hasPath && m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
            MoveToNextPatrolPosition();

        return TaskStatus.RUNNING;

    }

    void MoveToNextPatrolPosition()
    {
        ++m_CurrentPatrolPositionId;
        if (m_CurrentPatrolPositionId >= m_PatrolPositions.Count)
            m_CurrentPatrolPositionId = 0;
        m_NavMeshAgent.SetDestination(m_PatrolPositions[m_CurrentPatrolPositionId].position);
    }

    // what to do if task is aborted while running
    public override void OnAbort()
    {
        base.OnAbort();
    }


}
