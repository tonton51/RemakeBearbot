using UnityEngine;
using UnityEngine.AI;

public class Kumachanmover : MonoBehaviour
{
    public Transform[] points; // ����n�_���i�[����z��
    private int destPoint = 0; // ���݂̏���n�_
    private NavMeshAgent agent; // NavMeshAgent �R���|�[�l���g

    public GameObject target; // �ǐՑΏ� (Player)
    public float sp; // ���񎞂̑��x

    public float chaseDistance = 2.0f; // �ǐՂ��J�n���鋗��
    public float chaseSpeed = 3.0f; // �ǐՎ��̑��x

    void Start()
    {
        // NavMeshAgent �R���|�[�l���g���L���b�V��
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        // �����_���Ȓn�_���珄����J�n
        destPoint = UnityEngine.Random.Range(0, points.Length);
        GotoNextPoint();

        // �ǐՑΏۂ̐ݒ�
        target = GameObject.Find("Player");
    }

    void Update()
    {
        // �^�[�Q�b�g�Ƃ̋������v�Z
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= chaseDistance)
        {
            // �ǐՃ��[�h
            ChaseTarget();
            // Debug.Log("Chase");
        }
        else
        {
            // ���񃂁[�h
            Patrol();
            // Debug.Log("Patrol");
        }
    }

    void GotoNextPoint()
    {
        // ����n�_���ݒ肳��Ă��Ȃ��ꍇ�͏I��
        if (points.Length == 0)
            return;

        // ���̖ڕW�n�_��ݒ�
        agent.destination = points[destPoint].position;
        Debug.Log("Next Point: " + destPoint);

        // ���̒n�_��ݒ� (���񃋁[�v)
        destPoint = (destPoint + 1) % points.Length;
    }

    void Patrol()
    {
        // ���񑬓x�ɐݒ�
        agent.speed = sp;

        // ���݂̖ڕW�n�_�ɋ߂Â����玟�̒n�_�ֈړ�
        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {
            GotoNextPoint();
        }
    }

    void ChaseTarget()
    {
        // �ǐՑ��x�ɐݒ�
        agent.speed = chaseSpeed;

        // �^�[�Q�b�g��ǐ�
        agent.destination = target.transform.position;
    }
}
