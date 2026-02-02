using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private float tempoDeEspera = 5f;
    [SerializeField]private int pontos = 0;
    [SerializeField]private int level = 1;
    [SerializeField]private int baseLevel = 100;
    [SerializeField] private int quantidadeDeInimigos = 4;
    private float esperaInimigos = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        GerarInimigo();

    }
    public void DiminuiQuantidade()
    {
        quantidadeDeInimigos--;
    }
    public void GanhaPonto(int pontos)
    {
        this.pontos += pontos;
        if(this.pontos > baseLevel * level)
        {
            level ++;
        }
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
            
            while (quantidadeDeInimigos < quantidade)
            {
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
                Instantiate(inimigoCriados, posicao, transform.rotation);

                //quantidadeDeInimigos++;
                esperaInimigos = tempoDeEspera;
            }
        }
    }
}
