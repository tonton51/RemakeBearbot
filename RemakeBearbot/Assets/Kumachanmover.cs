using UnityEngine;
using UnityEngine.AI;

public class Kumachanmover : MonoBehaviour
{
    public Transform[] points; // 巡回地点を格納する配列
    private int destPoint = 0; // 現在の巡回地点
    private NavMeshAgent agent; // NavMeshAgent コンポーネント

    public GameObject target; // 追跡対象 (Player)
    public float sp; // 巡回時の速度

    public float chaseDistance = 2.0f; // 追跡を開始する距離
    public float chaseSpeed = 3.0f; // 追跡時の速度

    void Start()
    {
        // NavMeshAgent コンポーネントをキャッシュ
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        // ランダムな地点から巡回を開始
        destPoint = UnityEngine.Random.Range(0, points.Length);
        GotoNextPoint();

        // 追跡対象の設定
        target = GameObject.Find("Player");
    }

    void Update()
    {
        // ターゲットとの距離を計算
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= chaseDistance)
        {
            // 追跡モード
            ChaseTarget();
            // Debug.Log("Chase");
        }
        else
        {
            // 巡回モード
            Patrol();
            // Debug.Log("Patrol");
        }
    }

    void GotoNextPoint()
    {
        // 巡回地点が設定されていない場合は終了
        if (points.Length == 0)
            return;

        // 次の目標地点を設定
        agent.destination = points[destPoint].position;
        Debug.Log("Next Point: " + destPoint);

        // 次の地点を設定 (巡回ループ)
        destPoint = (destPoint + 1) % points.Length;
    }

    void Patrol()
    {
        // 巡回速度に設定
        agent.speed = sp;

        // 現在の目標地点に近づいたら次の地点へ移動
        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {
            GotoNextPoint();
        }
    }

    void ChaseTarget()
    {
        // 追跡速度に設定
        agent.speed = chaseSpeed;

        // ターゲットを追跡
        agent.destination = target.transform.position;
    }
}
