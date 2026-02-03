using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    [SerializeField] private GameObject myShot;
    [SerializeField] private GameObject myShot2;
    [SerializeField] private Transform posShoot;
    [SerializeField] private int life = 5;
    [SerializeField] private GameObject explosionDeath;
    [SerializeField] private float velocityShooter = 10f;
    [SerializeField] private float xLimite;
    [SerializeField] private float yLimite;
    [SerializeField] private int levelTiro = 1;
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
        // Limitando posição na tela
        // clamp - Ver um valo especifico e verifica se passa de um valor especifico e retorna true ou false
        // Valor que quero checar, valor minimo , valor maximo
        float meuX = Mathf.Clamp(transform.position.x, -xLimite, xLimite);
        float meuY = Mathf.Clamp(transform.position.y, -yLimite, yLimite);
        transform.position = new Vector3(meuX, meuY, transform.position.z);
    }
    private void Shooter()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (levelTiro)
            {
                case 1:
                    //Debug.Log("Pow");
                    CriarTiro(myShot, posShoot.position);
                    break;
                case 2:
                    CriarTiro(myShot2, new Vector3(posShoot.position.x - 0.5f,posShoot.position.y - 0.5f ));
                     CriarTiro(myShot2, new Vector3(posShoot.position.x + 0.5f,posShoot.position.y - 0.5f ));
                break;
                case 3:
                    CriarTiro(myShot2, new Vector3(posShoot.position.x - 0.5f,posShoot.position.y - 0.5f ));
                    CriarTiro(myShot, posShoot.position);
                     CriarTiro(myShot2, new Vector3(posShoot.position.x + 0.5f,posShoot.position.y - 0.5f ));
                break;
            }

        }

    }
    private void CriarTiro(GameObject tiroCriado, Vector3 pos)
    {
        GameObject shooter = Instantiate(tiroCriado, pos, transform.rotation);
        shooter.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, velocityShooter);

    }
    public void Damage(int damage)
    {
        Debug.LogWarning($"Perdeu vida {life}");
        life -= damage;
        if (life < 0)
        {
            Instantiate(explosionDeath, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("PowerUp"))
        {
            if(levelTiro < 3)
            {
                levelTiro++;
            }
            Destroy(other.gameObject);
        }
        
    }
}
