using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Configuração do Chão")]
    public GameObject _chaoPrefab;
    public float _chaoTamanho = 20f;
    public float _chaoDestruido = -25f;
    public float _chaoVelocidade = 5f;

    [Header("Configuração do Obstáculo")]
    public GameObject[] _obstaculoPrefabs;  // Agora é um array para múltiplos monstros
    public float _obstaculoTempo = 3f;
    public float _obstaculoVelocidade = 5f;

    public Transform _pontoDeSpawn;

    void Start()
    {
        StartCoroutine(SpawnObstaculo());
    }

    IEnumerator SpawnObstaculo()
    {
        while (true)
        {
            yield return new WaitForSeconds(_obstaculoTempo);

            if (_obstaculoPrefabs != null && _obstaculoPrefabs.Length > 0 && _pontoDeSpawn != null)
            {
                // Escolhe aleatoriamente um prefab do array
                int index = Random.Range(0, _obstaculoPrefabs.Length);
                GameObject prefabEscolhido = _obstaculoPrefabs[index];

                GameObject novoObstaculo = Instantiate(prefabEscolhido, _pontoDeSpawn.position, Quaternion.identity);

                Rigidbody2D rb = novoObstaculo.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.linearVelocity = new Vector2(-_obstaculoVelocidade, 0);
            }
        }
    }
}
