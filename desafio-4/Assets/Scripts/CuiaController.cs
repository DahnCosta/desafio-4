using UnityEngine;

public class CuiaController : MonoBehaviour
{
    private GameController _gameController;
    private CameraShaker _cameraShaker;
    private AudioController _audioController;  // Referência ao AudioController

    public float velocidade = 5f; // Velocidade do movimento para a esquerda

    void Start()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _cameraShaker = GameObject.FindObjectOfType<CameraShaker>();
        _audioController = GameObject.FindObjectOfType<AudioController>();  // Pega o AudioController
    }

    void FixedUpdate()
    {
        MoveObjeto();
    }

    void MoveObjeto()
    {
        transform.Translate(Vector2.left * velocidade * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Tocou no Obstáculo");

            // Shake da câmera
            _cameraShaker?.ShakeIt();

            // Tocar som de dano
            if (_audioController != null)
            {
                _audioController.TocarSomDano();
            }
            else
            {
                Debug.LogWarning("AudioController não encontrado no CuiaController.");
            }

            // Perder vida
            _gameController?.PerderVida();
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        Debug.Log("O Obstáculo foi Destruído!");
    }
}
