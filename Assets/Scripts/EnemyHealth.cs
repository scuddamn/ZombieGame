using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int Flash = Animator.StringToHash("flash");
    private static readonly int Die1 = Animator.StringToHash("die");
    [SerializeField] float health = 100f;
    [SerializeField] private float delay = 3f;

    private EnemyController enemyController;
    public bool isDead = false;

    private void Start()
    {
        enemyController = FindFirstObjectByType<EnemyController>();
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        health -= damage;
        GetComponentInChildren<Animator>().SetTrigger(Hit);
        if (health <= 0)
        {
           Die();
        }
    }

    public void Flashed()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponentInChildren<Animator>().SetBool(Flash, true);
        Invoke(nameof(ReturnToMove), delay);
        
    }

    void ReturnToMove()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponentInChildren<Animator>().SetBool(Flash, false);
        enemyController.isProvoked = false;
    }

    void WaitForDeath()
    {
        Destroy(gameObject);
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponentInChildren<Animator>().SetTrigger(Die1);
    }
}
