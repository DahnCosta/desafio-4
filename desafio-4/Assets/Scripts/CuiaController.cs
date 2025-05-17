using UnityEngine;

public class CuiaController : MonoBehaviour
{
    private GameController _gameController;
    private CameraShaker _cameraShaker;

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
        transform.Translate(Vector2.left * 5f * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Tocou no Obstáculo");
            _cameraShaker.ShakeIt();
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        Debug.Log("O Obstáculo foi Destruído!");
    }
}
