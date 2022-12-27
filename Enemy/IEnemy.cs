

public interface IEnemy 
{
    public int ID { get; set; }
    public Spawner spawner { get; set; }
    int Experience { get; set; }
    void Die();
    void TakeDamage(int damage);
    void Attack();
}
