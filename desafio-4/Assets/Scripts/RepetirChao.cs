using UnityEngine;

public class RepetirChao : MonoBehaviour
{
    private GameController _gameController;
    public bool _chaoInstanciado = false;

    void Start()
    {
        _gameController = FindFirstObjectByType<GameController>();
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
                    0
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
        transform.Translate(Vector2.left * _gameController._chaoVelocidade * Time.deltaTime);
    }
}
