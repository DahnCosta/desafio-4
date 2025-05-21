using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject[] _obstaculoPrefabs;
    public float _obstaculoTempo = 3f;
    public float _obstaculoVelocidade = 5f;

    [Header("Aumento de Dificuldade")]
    public float tempoParaAumentar = 3f;
    public float incrementoVelocidade = 1f;

    [Header("Limite de Velocidade")]
    public float velocidadeMaximaObstaculo = 20f;
    public float velocidadeMaximaChao = 15f;

    [Header("Pontuação e Vidas")]
    public TMP_Text pontosTexto;
    public TMP_Text vidasTexto;

    private float tempoSobrevivido = 0f;
    private int vidas = 3;

    private float _tempoDecorrido = 0f;
    public Transform _pontoDeSpawn;

    public float multiplicadorVelocidadePrimeiro = 1.5f;

    void Start()
    {
        AtualizarVidasUI();
        StartCoroutine(SpawnObstaculo());
    }

    void Update()
    {
        tempoSobrevivido += Time.deltaTime;
        int pontos = Mathf.FloorToInt(tempoSobrevivido);

        if (pontosTexto != null)
            pontosTexto.text = "Pontos: " + pontos;

        _tempoDecorrido += Time.deltaTime;
        if (_tempoDecorrido >= tempoParaAumentar)
        {
            _tempoDecorrido = 0f;

            if (_obstaculoVelocidade < velocidadeMaximaObstaculo)
                _obstaculoVelocidade = Mathf.Min(_obstaculoVelocidade + incrementoVelocidade, velocidadeMaximaObstaculo);

            if (_chaoVelocidade < velocidadeMaximaChao)
                _chaoVelocidade = Mathf.Min(_chaoVelocidade + incrementoVelocidade, velocidadeMaximaChao);

            Debug.Log($"Velocidade atualizada! Obstáculos: {_obstaculoVelocidade} | Chão: {_chaoVelocidade}");
        }
    }

    IEnumerator SpawnObstaculo()
    {
        while (true)
        {
            yield return new WaitForSeconds(_obstaculoTempo);

            int index = Random.Range(0, _obstaculoPrefabs.Length);
            GameObject prefabEscolhido = _obstaculoPrefabs[index];
            Vector3 posicaoSpawn = _pontoDeSpawn.position;

            GameObject novoObstaculo = Instantiate(prefabEscolhido, posicaoSpawn, Quaternion.identity);

            CuiaController cuia = novoObstaculo.GetComponent<CuiaController>();
            if (cuia != null)
            {
                cuia.velocidade = _obstaculoVelocidade * multiplicadorVelocidadePrimeiro;
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

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
