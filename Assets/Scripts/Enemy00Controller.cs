using UnityEngine;

public class Enemy00Controller : Enemy
{
    private Rigidbody2D myRigidbody;
    private float timerShoot = 1f;
    [SerializeField] private GameObject shoot;
    [SerializeField] private Transform posShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.linearVelocity = new Vector2(0f, -velocity);
        timerShoot = Random.Range(0.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Shooter();
    }
    private void Shooter()
    {
        bool sprite = GetComponentInChildren<SpriteRenderer>().isVisible;
        //Debug.LogWarning(sprite);
        timerShoot -= Time.deltaTime;
        if ((timerShoot < 0) && sprite)
        {
            timerShoot = Random.Range(1f, 2f);
            GameObject shooter = Instantiate(shoot, transform.position, transform.rotation);
            shooter.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, velocityShooter);
        }
    }
}
