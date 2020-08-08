using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    private Animator animator;
    public GameObject projectilePrefab;
    public float minAttackTime;
    public float maxAttackTime;
    private bool attacking;
    private float timeLeft;
    private float projectileSpeed;
    private float numberOfProjectiles;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        numberOfProjectiles = 8;
        radius = 5f;
        projectileSpeed = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Ready")){
            float attackTime = Random.Range(minAttackTime, maxAttackTime);
            if(!attacking)
            {
                timeLeft -= Time.deltaTime;
                if(timeLeft < 0)
                {
                    attacking = true;
                    StartCoroutine("TurretAttack", attackTime);
                    timeLeft = attackTime;
                }
            }
        }

    }

    IEnumerator TurretAttack(float attackTime)
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1.1f);
        SpawnProjectiles();
        animator.SetBool("isAttacking", false);
        attacking  = false;
    }

    void SpawnProjectiles()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i=0; i < numberOfProjectiles; i++)
        {
            float projectileXPosition = gameObject.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileYPosition = gameObject.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2 (projectileXPosition, projectileYPosition);
            Vector2 turretPosition = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);
            Vector2 projectileMoveDirection = (projectileVector - turretPosition).normalized * projectileSpeed;
            var projectile = Instantiate(projectilePrefab, turretPosition, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            angle += angleStep;
        }
    }
}
