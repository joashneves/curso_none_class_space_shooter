using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        int quantidade = FindObjectsOfType<GameManager>().Length;
        if(quantidade > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void IniciaJogo()
    {
        SceneManager.LoadScene(1);
    }
    IEnumerator PrimeiraCena()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    public void Inicio()
    {
        StartCoroutine(PrimeiraCena());
    }
    public void Sair()
    {
        Application.Quit();
    }   
}
