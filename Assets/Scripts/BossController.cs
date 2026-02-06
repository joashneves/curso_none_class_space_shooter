using UnityEngine;
using UnityEngine.UI;


public class BossController : Enemy
{
    [Header("info-manções")]
    [SerializeField]private int lifeMax = 100;
    [SerializeField] private string estado = "estado1";
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float limitHorizontal = 6f;
    [SerializeField] private bool direita = true;
    [SerializeField] private string[] listaDeEstado;
    [SerializeField] private float esperaEstado = 10f;
    [Header("informacoes dos tiros")]
    [SerializeField] private Transform posTiroUm;
    [SerializeField] private Transform posTiroDois;
    [SerializeField] private Transform posTiroTres;
    [SerializeField] private GameObject tiroUm;
    [SerializeField] private GameObject tiroDois;
    [SerializeField] private float delayShoot = 2f;
    [SerializeField] private float timerShoot01 = 4;
    [SerializeField] private float delayShoot01 = 1f;
     [Header("Informações do UI")]
    [SerializeField] private Image barraDeVidaDoBoss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = lifeMax;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case "estado1":
                Estado01();
                break;
            case "estado2":
                Estado00();
                break;
            case "estado3":
                Estado02();
                break;
            case "parado":
                Debug.Log("Fazendo o negocio la");
                break;
        }
        TrocaDeEstado();
        //Vida
        barraDeVidaDoBoss.fillAmount = ((float)life / (float)lifeMax);
        barraDeVidaDoBoss.color = new Color32(132, (byte) (barraDeVidaDoBoss.fillAmount * 255), 90, 100);
    }
    private void Estado00()
    {   
        Movimento00();
        if (timerShoot <= 0f)
        {
            Tiro01();
            timerShoot = delayShoot / 2;
        }
        else
        {
            timerShoot -= Time.deltaTime;
        }
    }
    private void Estado01()
    {
        if (timerShoot <= 0f)
        {
            Tiro00();
            timerShoot = delayShoot;
        }
        else
        {
            timerShoot -= Time.deltaTime;
        }


    }
    private void Estado02()
    {
        Movimento00();
        if (timerShoot <= 0f)
        {
            Tiro01();

            timerShoot = delayShoot;
        }
        else
        {
            timerShoot -= Time.deltaTime;
        }
        if (timerShoot01 <= 0f)
        {
            Tiro00();
            timerShoot01 = delayShoot01 / 2;
        }
        else
        {
            timerShoot01 -= Time.deltaTime;
        }
    }
    private void Tiro00()
    {
        GameObject tiro00 = Instantiate(tiroUm, posTiroTres.position, transform.rotation);
        tiro00.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, -velocityShooter);

        tiro00 = Instantiate(tiroUm, posTiroUm.position, transform.rotation);
        tiro00.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, -velocityShooter);

    }
    private void Tiro01()
    {
        var player = FindAnyObjectByType<PlayerController>();

        if (player)
        {
            GameObject shooter = Instantiate(tiroDois, posTiroDois.position, transform.rotation);
            // Encontrando o player na cena
            Vector2 direcao = player.transform.position - shooter.transform.position;
            direcao.Normalize();
            shooter.GetComponent<Rigidbody2D>().linearVelocity = direcao * velocityShooter;
            // Pegar rotação
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            shooter.transform.rotation = Quaternion.Euler(0f, 0f, angulo + 90);
        }
    }
    private void Movimento00()
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
            direita = false;
        }
        else if (transform.position.x <= -limitHorizontal)
        {
            direita = true;
        }
    }
    private void TrocaDeEstado()
    {
        if(esperaEstado <= 0f)
        {
            int indiceDeIndex = Random.Range(0, listaDeEstado.Length);
            estado = listaDeEstado[indiceDeIndex];
            esperaEstado = Random.Range(2f, 4f);
            Debug.Log($"Estado atual : {estado}");
        }
        else
        {
            esperaEstado -= Time.deltaTime;
        }
    }
    private void AumentaDificuldade()
    {
        if(life <= lifeMax / 2)
        {
            delayShoot = 0.5f;
        }
    }
}
