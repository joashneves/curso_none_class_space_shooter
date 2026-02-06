using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int life;
    [SerializeField] protected float velocity;
    [SerializeField] protected GameObject explosion;
    [SerializeField] protected GameObject shoot;
    [SerializeField] protected GameObject powerUp;
    [SerializeField] protected float velocityShooter;
    [SerializeField] protected int pontos;
    [SerializeField] protected float itemRate;
    protected float timerShoot = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Damage(int damage)
    {
        bool sprite = GetComponentInChildren<SpriteRenderer>().isVisible;
        if (sprite)
        {
            Debug.LogWarning($"Inimigo Perdeu vida : {life}");
            life-=damage;
            if(life <= 0)
            {
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
                if(powerUp)DropaItem();
                FindAnyObjectByType<SpawnEnemy>().GanhaPonto(pontos);
            }
        }
    }
    private void OnDestroy()
    {
        Debug.Log("Inimigo se destruiu");
        var gerador = FindAnyObjectByType<SpawnEnemy>();
        if(gerador) gerador.DiminuiQuantidade();  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destruidor"))
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (other.CompareTag("SpaceShip"))
        {
            other.GetComponent<PlayerController>().Damage(1);   
            //DropaItem();
        }
    }
    public void DropaItem()
    {   
        float chance = Random.Range(0f, 1f);
        if (chance > itemRate)
        {
            GameObject pU = Instantiate(powerUp, transform.position, transform.rotation);
            Destroy(pU, 3f);
            Destroy(gameObject);

            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            pU.GetComponent<Rigidbody2D>().linearVelocity = dir;
        }
    }
}
