using System;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    [SerializeField] private GameObject myShot;
    [SerializeField] private GameObject myShot2;
    [SerializeField] private Transform posShoot;
    [SerializeField] private GameObject escudo;
    [SerializeField] private int quantidadeDeEscudo = 3;
    [SerializeField] private int life = 5;
    [SerializeField] private GameObject explosionDeath;
    [SerializeField] private float velocityShooter = 10f;
    [SerializeField] private float xLimite;
    [SerializeField] private float yLimite;
    [SerializeField] private int levelTiro = 1;
    private Rigidbody2D myRigidbody;
    private GameObject escudoAtual;
    private float escudoTempo;
    [Header("Informações do UI")]
    [SerializeField] private Text vidaTexto;
    [SerializeField] private Text EscudoTexto;

    [Header("Sons")]
    [SerializeField]private AudioClip meuSom;
    [SerializeField]private AudioClip somDaMORTE;
    [SerializeField] private AudioClip somDoEscudo;
    [SerializeField] private AudioClip somDoEscudoAcabando;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        vidaTexto.text = life.ToString();
         EscudoTexto.text = quantidadeDeEscudo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Shooter();
        CriaEscudo();
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
    private void CriaEscudo()
    {
        if (Input.GetButtonDown("Shield") && !escudoAtual && quantidadeDeEscudo > 0)
        {
            AudioSource.PlayClipAtPoint(somDoEscudo, new Vector3(0f,0f,-10f));
            escudoAtual = Instantiate(escudo, transform.position, transform.rotation);
            quantidadeDeEscudo--;
            EscudoTexto.text = quantidadeDeEscudo.ToString();
        }
        if (escudoAtual)
        {
            escudoTempo += Time.deltaTime;
            if (escudoAtual) escudoAtual.transform.position = transform.position;
            if (escudoTempo > 6.2f)
            {
                AudioSource.PlayClipAtPoint(somDoEscudo, new Vector3(0f,0f,-10f));
                Destroy(escudoAtual);
                escudoTempo = 0f;
            }
        }

    }
    private void Shooter()
    {
        if (Input.GetButtonDown("Fire1"))
        {
             AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);
            switch (levelTiro)
            {
                case 1:
                    //Debug.Log("Pow");
                    CriarTiro(myShot, posShoot.position);
                    break;
                case 2:
                    CriarTiro(myShot2, new Vector3(posShoot.position.x - 0.5f, posShoot.position.y - 0.5f));
                    CriarTiro(myShot2, new Vector3(posShoot.position.x + 0.5f, posShoot.position.y - 0.5f));
                    break;
                case 3:
                    CriarTiro(myShot2, new Vector3(posShoot.position.x - 0.5f, posShoot.position.y - 0.5f));
                    CriarTiro(myShot, posShoot.position);
                    CriarTiro(myShot2, new Vector3(posShoot.position.x + 0.5f, posShoot.position.y - 0.5f));
                    break;
                case 4:
                    CriarTiro(myShot2, new Vector3(posShoot.position.x - 0.5f, posShoot.position.y - 0.5f));
                    CriarTiro(myShot, posShoot.position);
                    CriarTiroSecundario(myShot, new Vector3(posShoot.position.x - 0.5f, posShoot.position.y - 0.5f), -60);
                    CriarTiroSecundario(myShot, new Vector3(posShoot.position.x + 0.5f, posShoot.position.y - 0.5f), 60);
                    CriarTiro(myShot2, new Vector3(posShoot.position.x + 0.5f, posShoot.position.y - 0.5f));
                    break;
            }
        }

    }
    private void CriarTiroSecundario(GameObject tiroCriado, Vector3 pos, float rotacao)
    {
        GameObject shooter = Instantiate(tiroCriado, pos, transform.rotation); // Inicia o objsto
        shooter.transform.rotation = Quaternion.Euler(0f, 0f, rotacao); // Coloca o objeto na rotação
        int positivoOuNegativo = Math.Sign(rotacao); // Verifica se o numero é positivo ou negativo
                                                     // Tenta pegar o transforme para mudar a sprite em si
        shooter.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(velocityShooter * positivoOuNegativo, velocityShooter); // Muda a direção do player
    }
    private void CriarTiro(GameObject tiroCriado, Vector3 pos)
    {
        //Quaternion rotation = transform.rotation;
        GameObject shooter = Instantiate(tiroCriado, pos, transform.rotation);
        shooter.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, velocityShooter);

    }
    public void Damage(int damage)
    {
        Debug.LogWarning($"Perdeu vida {life}");
        life -= damage;
        if (life <= 0)
        {
            AudioSource.PlayClipAtPoint(meuSom, new Vector3(0f,0f,-10f));
            Instantiate(explosionDeath, transform.position, transform.rotation);
            Destroy(gameObject);
            var gameMananer = FindAnyObjectByType<GameManager>();
            gameMananer.Inicio();
        }
        vidaTexto.text = life.ToString();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            if (levelTiro < 4)
            {
                levelTiro++; 
            }else if (life < 5)
            {
                life++;
                vidaTexto.text = life.ToString();
                
            }else if(quantidadeDeEscudo < 5)
            {
                quantidadeDeEscudo++;
                Debug.LogWarning($"Quantidade de escudos : {quantidadeDeEscudo}");
                EscudoTexto.text = quantidadeDeEscudo.ToString();
            }
            else
            {
                Debug.LogWarning($"Ganhou pontos");
                FindAnyObjectByType<SpawnEnemy>().GanhaPonto(100);
            }
            Destroy(other.gameObject);
        }

    }
}
