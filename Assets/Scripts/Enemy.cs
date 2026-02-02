using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int life;
    [SerializeField] protected float velocity;
    [SerializeField] protected GameObject explosion;
    [SerializeField] protected GameObject shoot;
    [SerializeField] protected float velocityShooter;
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
        Debug.LogWarning("Perdeu vida");
        life-=damage;
        if(life <= 0)
        {
            Destroy(gameObject);    
            Instantiate(explosion, transform.position, transform.rotation);
        }
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
            Destroy(gameObject);
        }
    }
}
