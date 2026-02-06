using UnityEngine;

public class AnimacaoBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CriarBoss()
    {
        Instantiate(boss, transform.position, transform.rotation);
        var meuPai = transform.parent.gameObject;
        Destroy(gameObject);
    }
}
