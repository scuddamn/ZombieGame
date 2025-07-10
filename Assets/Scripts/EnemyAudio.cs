using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] zombieAttack;
    [SerializeField] private AudioClip[] zombieShriek;
    [SerializeField] private AudioClip[] zombieWalk;
    [SerializeField] private AudioClip[] zombieDetect;
    [SerializeField] private AudioClip[] zombieDeath;
    

    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void AttackSound()
    {
        int random = Random.Range(0, zombieAttack.Length);
        audioSource.PlayOneShot(zombieAttack[random]);
    }

    public void ShriekSound()
    {
        int random = Random.Range(0, zombieShriek.Length);
        audioSource.PlayOneShot(zombieAttack[random]);
    }

    public void WalkLoop()
    {
        int random = Random.Range(0, zombieWalk.Length);
        audioSource.PlayOneShot(zombieWalk[random]);
    }

    public void PlayerDetectedSound()
    {
        int random = Random.Range(0, zombieDetect.Length);
        audioSource.PlayOneShot(zombieDetect[random]);
    }

    public void DeathSound()
    {
        int random = Random.Range(0, zombieDeath.Length);
        audioSource.PlayOneShot(zombieDeath[random]);
    }
}
