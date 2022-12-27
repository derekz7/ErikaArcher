using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Dragon : MonoBehaviour, IEnemy
{
    [SerializeField] private string eName;
    [SerializeField] private int health;
    [SerializeField] private int currentHealth;
    [SerializeField] public int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private Canvas monsterUI;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private HealthBar heathbar;


    [Header("Patrol Settings")]
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float walkPointRange;


    private GameObject player;
    [SerializeField]
    private bool isTakeDamage = false;

    public Spawner spawner { get; set; }
    public int Experience { get; set; }
    public int ID { get; set; }

    private void Start()
    {
        this.nameText.text = eName;
        currentHealth = health;
        this.heathbar.SetMaxHealth(health);
        player = GameObject.FindWithTag("Player");
        this.monsterUI.gameObject.SetActive(false);
        ID = 1;
    }


    public Vector3 SearchWayPoint()
    {
        Vector3 walkPoint;
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        return walkPoint;
    }

    public void TakeDamage(int damage)
    {
        ShowCanvas();
        isTakeDamage = true;
        currentHealth -= damage;
        this.heathbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("takeDamage");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            TakeDamage(arrow.GetDamage());
            Chasing();
            Debug.Log("Get damage " + collision.gameObject.name + "=" + collision.gameObject.GetComponent<Arrow>().GetDamage());
        }
    }


    public void Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
    }

    public void Chasing()
    {
        if (isTakeDamage)
        {
            this.transform.LookAt(player.transform.position);
            animator.SetBool("isChasing", true);
            animator.SetBool("isPatrolling", false);
        }
    }
    public void IsTakeDamage(bool value)
    {
        isTakeDamage = value;
    }

    public string GetName()
    {
        return eName;
    }

    public void Die()
    {
        animator.SetTrigger("die");
        this.GetComponent<BoxCollider>().enabled = false;
        foreach (SphereCollider collider in GetComponents<SphereCollider>())
        {
            collider.enabled = false;
        }
        player.GetComponent<Player>().quest.goal.EnemyKilled(ID);
        this.spawner.Respawn();
        Destroy(gameObject, 5);
    }

    public void ShowCanvas()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("MonsterUI"))
        {
            gameObject.SetActive(false);
        }
        this.monsterUI.gameObject.SetActive(true);
    }


}
