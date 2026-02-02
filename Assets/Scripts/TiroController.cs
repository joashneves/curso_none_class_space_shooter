using UnityEngine;

public class TiroController : MonoBehaviour
{
    [SerializeField] private GameObject impact;
    private Rigidbody2D myRigidbody;
    [SerializeField]private float velocity = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //myRigidbody.linearVelocity = new Vector2(0f, velocity);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy00"))
        {
            collision.GetComponent<Enemy>().Damage(1);   
        }
        
        if (collision.CompareTag("SpaceShip"))
        {
            collision.GetComponent<PlayerController>().Damage(1);   
        }
        //Debug.Log("APAGADO");
        Destroy(gameObject);

        Instantiate(impact, transform.position, transform.rotation);
    }
}
