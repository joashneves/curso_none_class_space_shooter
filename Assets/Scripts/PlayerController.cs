using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    [SerializeField] private GameObject myShot;
    [SerializeField] private Transform posShoot;
    [SerializeField] private int life = 5;
    [SerializeField] private GameObject explosionDeath;
    [SerializeField] private float velocityShooter = 10f;
    private Rigidbody2D myRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Shooter();

    }
    private void Moving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 myVelocity = new Vector2(horizontal, vertical);
        myVelocity.Normalize(); // Metodo que coloca o valor em 0 a 1 
        myRigidbody.linearVelocity = myVelocity * velocity;

    }
    private void Shooter()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Pow");
            GameObject shooter = Instantiate(myShot, posShoot.position, transform.rotation);
            shooter.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, velocityShooter);
        }

    }
    public void Damage(int damage)
    {
        Debug.LogWarning($"Perdeu vida {life}");
        life -= damage;
        if (life <= 0)
        {
            Instantiate(explosionDeath, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
