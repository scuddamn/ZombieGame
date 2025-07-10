using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;
    [SerializeField] private float turnSpeed = 5f;
    
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    public bool isProvoked = false;
    private EnemyHealth health;
    
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<EnemyHealth>();
        }
    
        // Update is called once per frame
        void Update()
        {
            if (health.isDead)
            {
                enabled = false;
                navMeshAgent.enabled = false;
            }
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }
        }
    
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    
        void EngageTarget()
        {
            FaceTarget();
            
            if (distanceToTarget >= navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            }
    
            if (distanceToTarget <= navMeshAgent.stoppingDistance)
            {
                AttackTarget();
            }
        }
    
        void ChaseTarget()
        {
            GetComponentInChildren<Animator>().SetBool("attack", false);
            GetComponentInChildren<Animator>().SetTrigger("move");
            navMeshAgent.SetDestination(target.position);
        }

        void AttackTarget()
        {
            GetComponentInChildren<Animator>().SetBool("attack", true);
            Debug.Log("attacking player");
        }

        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        public void OnDamageTaken()
        {
            isProvoked = true;
        }

    

}

