using UnityEngine;

public class CuiaController : MonoBehaviour
{
    private GameController _gameController;
    private CameraShaker _cameraShaker;

    public float velocidade = 5f; // Agora a velocidade pode ser atualizada ao instanciar

    void Start()
    {
        _gameController = GameObject.FindFirstObjectByType<GameController>();
        _cameraShaker = GameObject.FindFirstObjectByType<CameraShaker>();
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
            _cameraShaker.ShakeIt();

            if (_gameController != null)
            {
                _gameController.PerderVida();
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        Debug.Log("O Obstáculo foi Destruído!");
    }
}
