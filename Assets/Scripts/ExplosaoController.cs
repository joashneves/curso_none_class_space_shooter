using UnityEngine;

public class ExplosaoController : MonoBehaviour
{
    [SerializeField] private AudioClip explocaoSon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.position.y < -5f && transform.position.y > 5f)
        {
            AudioSource.PlayClipAtPoint(explocaoSon, Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Explosion()
    {
        Destroy(gameObject);

    }
}
