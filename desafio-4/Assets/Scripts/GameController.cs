using UnityEngine;
using UnityEngine.SceneManagement;  // Para reiniciar o jogo
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Configuração do Chão")]
    public GameObject _chaoPrefab;
    public float _chaoTamanho = 20f;
    public float _chaoDestruido = -25f;
    public float _chaoVelocidade = 5f;

    [Header("Configuração do Obstáculo")]
    public GameObject[] _obstaculoPrefabs;  // Array para múltiplos monstros
    public float _obstaculoTempo = 3f;
    public float _obstaculoVelocidade = 5f;

    [Header("Aumento de Dificuldade")]
    public float tempoParaAumentar = 3f;      // A cada 3 segundos aumenta a velocidade
    public float incrementoVelocidade = 1f;  // Aumenta 1 por vez

    [Header("Pontuação e Vidas")]
    public TMP_Text pontosTexto;  // Referência ao TextMeshPro da UI
    public TMP_Text vidasTexto;   // Novo texto para mostrar as vidas

    private float tempoSobrevivido = 0f;
    private int vidas = 3;

    private float _tempoDecorrido = 0f;
    public Transform _pontoDeSpawn;

    void Start()
    {
        AtualizarVidasUI();
        StartCoroutine(SpawnObstaculo());
    }

    void Update()
    {
        // Atualiza o tempo sobrevivido e a pontuação
        tempoSobrevivido += Time.deltaTime;
        int pontos = Mathf.FloorToInt(tempoSobrevivido);

        if (pontosTexto != null)
            pontosTexto.text = "Pontos: " + pontos;

        // Controla o aumento progressivo da velocidade
        _tempoDecorrido += Time.deltaTime;
        if (_tempoDecorrido >= tempoParaAumentar)
        {
            _tempoDecorrido = 0f;
            _obstaculoVelocidade += incrementoVelocidade;
            _chaoVelocidade += incrementoVelocidade;

            Debug.Log($"Velocidade aumentada! Obstáculos: {_obstaculoVelocidade} | Chão: {_chaoVelocidade}");
        }
    }

    IEnumerator SpawnObstaculo()
    {
        while (true)
        {
            yield return new WaitForSeconds(_obstaculoTempo);

            if (_obstaculoPrefabs != null && _obstaculoPrefabs.Length > 0 && _pontoDeSpawn != null)
            {
                int index = Random.Range(0, _obstaculoPrefabs.Length);
                GameObject prefabEscolhido = _obstaculoPrefabs[index];

                GameObject novoObstaculo = Instantiate(prefabEscolhido, _pontoDeSpawn.position, Quaternion.identity);

                // Define a velocidade diretamente no script CuiaController
                CuiaController cuia = novoObstaculo.GetComponent<CuiaController>();
                if (cuia != null)
                {
                    cuia.velocidade = _obstaculoVelocidade;
                }
            }
        }
    }

    public void PerderVida()
    {
        vidas--;
        AtualizarVidasUI();

        if (vidas <= 0)
        {
            ReiniciarJogo();
        }
    }

    private void AtualizarVidasUI()
    {
        if (vidasTexto != null)
            vidasTexto.text = "Vidas: " + vidas;
    }

    private void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
