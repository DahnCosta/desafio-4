using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string _nomeCenaJogo;

    public void IniciarJogo()
    {
        SceneManager.LoadScene(_nomeCenaJogo);
    }
   
   public void SairJogo()
    {
        Application.Quit();
        Debug.Log("Saindo do Jogo...");
    }

}
