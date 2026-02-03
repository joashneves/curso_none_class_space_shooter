using System;
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
                
                FindAnyObjectByType<SpawnEnemy>().GanhaPonto(pontos);
            }
        }
    }
    private void OnDestroy()
    {
        Debug.Log("Inimigo se destruiu");
        FindAnyObjectByType<SpawnEnemy>().DiminuiQuantidade();  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destruidor"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("SpaceShip"))
        {
            other.GetComponent<PlayerController>().Damage(1);   

            GameObject pU = Instantiate(powerUp, transform.position, transform.rotation);
            Destroy(pU, 3f);
            Destroy(gameObject);

            pU.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up;
        }
    }
}
