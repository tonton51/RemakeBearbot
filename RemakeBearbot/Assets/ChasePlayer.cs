using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChasePlayer : MonoBehaviour
{
    public Transform target;
    // �ǐ՗p
    void Update()
    {
        Debug.DrawLine(transform.position, target.position, DetectTarget() ? Color.blue : Color.green);
        Debug.DrawLine(transform.position, target.position, LookDistance() ? Color.red : Color.green);

    }

    public bool DetectTarget()
    {
        Vector3 direction = target.position - transform.position;
        RaycastHit hit;

        if (Vector3.Angle(transform.forward, direction) < 5f)
        {
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.transform == target)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool LookDistance()
    {
        if (Vector3.Distance(target.position, transform.position) < 2f)
        {
            return true;
        }
        return false;
    }
}
