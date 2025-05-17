using UnityEngine;

public class RepetirChao : MonoBehaviour
{
    private GameController _gameController;
    private bool _chaoInstanciado = false;

    void Start()
    {
        _gameController = FindFirstObjectByType<GameController>();
        if (_gameController == null)
        {
            Debug.LogError("GameController não encontrado na cena!");
        }
    }

    void Update()
    {
        if (!_chaoInstanciado)
        {
            if (transform.position.x <= 0)
            {
                _chaoInstanciado = true;

                GameObject novoChao = Instantiate(_gameController._chaoPrefab);
                novoChao.transform.position = new Vector3(
                    transform.position.x + _gameController._chaoTamanho,
                    transform.position.y,
                    transform.position.z
                );

                Debug.Log("O Chão foi Instanciado!");
            }
        }

        if (transform.position.x < _gameController._chaoDestruido)
        {
            _chaoInstanciado = false;
            Debug.Log("O Chão foi Destruído!");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        MoveChao();
    }

    void MoveChao()
    {
        transform.Translate(Vector2.left * _gameController._chaoVelocidade * Time.fixedDeltaTime);
    }
}
