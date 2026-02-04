using UnityEngine;

public class BossController : Enemy
{
    [SerializeField] private string estado = "parado";
    [SerializeField] private float velocity = 5;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float limitHorizontal = 6f;
    private bool direita = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case "estado":
                Estado01();
                break;
            case "parado":
                Debug.Log("Fazendo o negocio la");
                break;
        }
    }
    private void Estado01()
    {


        if (direita)
        {
            myRigidbody.linearVelocity = new Vector2(velocity, 0f);
        }
        else
        {
            myRigidbody.linearVelocity = new Vector2(-velocity, 0f);
        }

        if (transform.position.x >= limitHorizontal)
        {
            direita = true;
        }
        else if (transform.position.x <= -limitHorizontal)
        {
            direita = false;
        }
    }
}
