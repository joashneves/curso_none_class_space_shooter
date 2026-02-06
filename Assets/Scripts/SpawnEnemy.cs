using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private float tempoDeEspera = 5f;
    [SerializeField]private int pontos = 0;
    [SerializeField]private int level = 1;
    [SerializeField]private int baseLevel = 100;
    [SerializeField] private int quantidadeDeInimigos = 0;
    [SerializeField] private GameObject bossAnimation;
    private bool criouBoss = false;
    private float esperaInimigos = 0f;
    [SerializeField] private AudioClip musicaBoss;
    [SerializeField] private AudioSource musica;
    [Header("Informações do UI")]
    [SerializeField] private Text pontosTexto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pontosTexto.text = pontos.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if(level < 10)
        {
        GerarInimigo();
        }
        else
        {
            GerarBoss();
        }

    }
    private void GerarBoss()
    {
        if(quantidadeDeInimigos <= 0 && tempoDeEspera > 0)
        {
            tempoDeEspera -= Time.deltaTime;
        }
        if (!criouBoss && tempoDeEspera <= 0)
        {
            musica.clip = musicaBoss;
            musica.Play();
            GameObject animacaoBossObjeto = Instantiate(bossAnimation, Vector3.zero, transform.rotation);
            Destroy(animacaoBossObjeto, 6.2f);
            criouBoss = true;
        }
    }
    public void DiminuiQuantidade()
    {
        quantidadeDeInimigos--;
    }
    public void GanhaPonto(int pontos)
    {
        this.pontos += pontos * level;
        pontosTexto.text = this.pontos.ToString();
        if(this.pontos > baseLevel * level)
        {
            level ++;
            baseLevel *= 2;
        }
    }
    private bool ChecarInimigo(Vector3 posicao, Vector3 size)
    {
        // checando se a posicao tem alguem
        Collider2D hit = Physics2D.OverlapBox(posicao, size, 0f);
        if(hit != null) return true;
        return false;
    }
    private void GerarInimigo()
    {
        if (esperaInimigos > 0)
        {
            esperaInimigos -= Time.deltaTime;
        }
        if (esperaInimigos <= 0f && quantidadeDeInimigos <= 0)
        {

            int quantidade = level * 3;
            //int quantidadeDeInimigos = 0;
            int tentativas = 0;
            
            while (quantidadeDeInimigos < quantidade)
            {
                // aumentando as tentativas
                tentativas++;
                if(tentativas > 200) break;
                GameObject inimigoCriados;

                float chance = Random.Range(0f, level);

                if (chance > 2f)
                {
                    inimigoCriados = enemys[1];
                }
                else
                {
                    inimigoCriados = enemys[0];
                }

                Vector3 posicao = new Vector3(Random.Range(-8f, 8f), Random.Range(5.99f, 11f), 0f);
                if( ChecarInimigo(posicao, inimigoCriados.transform.localScale )) continue;
                Instantiate(inimigoCriados, posicao, transform.rotation);

                quantidadeDeInimigos++;
                esperaInimigos = tempoDeEspera;
            }
        }
    }
}
