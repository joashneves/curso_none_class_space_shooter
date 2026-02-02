
using UnityEngine;

public class Enemy01Controller : Enemy
{
    private Rigidbody2D myRigidbody;
    private bool moveOn = true;
    [SerializeField] private float yMax = 2.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.linearVelocity = Vector2.up * velocity;
        timerShoot = Random.Range(0.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Shooter();
        Moving();
    }
    private void Moving()
    {
        if(transform.position.y < yMax)
        {
            if(transform.position.x < 0f && moveOn)
            {
                myRigidbody.linearVelocity = Vector2.left * velocity;
                //moveOn = false;
            } else
            {
                myRigidbody.linearVelocity = Vector2.right * velocity;
                ///moveOn = false;
            }
            moveOn = false;
            
        }
    }
    private void Shooter()
    {
        bool sprite = GetComponentInChildren<SpriteRenderer>().isVisible;
        //Debug.LogWarning(sprite);
        timerShoot -= Time.deltaTime;
        var player = FindAnyObjectByType<PlayerController>();

        if (player)
        {
            if ((timerShoot < 0) && sprite)
            {
                timerShoot = Random.Range(1f, 2f);
                GameObject shooter = Instantiate(shoot, transform.position, transform.rotation);
                // Encontrando o player na cena
                Vector2 direcao = player.transform.position - shooter.transform.position;
                direcao.Normalize();
                shooter.GetComponent<Rigidbody2D>().linearVelocity = direcao * velocityShooter;
                // Pegar rotação
                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                shooter.transform.rotation = Quaternion.Euler(0f, 0f, angulo + 90);
            }

        }
    }
}
