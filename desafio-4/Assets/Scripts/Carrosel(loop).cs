using UnityEngine;

public class CenarioInfinito : MonoBehaviour
{
    [SerializeField] public float velocidade = 1f;

    private Vector3 posicaoInicial;
    private float tamanhoRealDaImagem;
    private float deslocamentoAcumulado;

    private void Awake()
    {
        // Armazena a posição inicial do objeto
        this.posicaoInicial = this.transform.position;

        // Calcula o tamanho real da imagem considerando a escala
        float tamanhoImagem = this.GetComponent<SpriteRenderer>().size.x;
        float escala = this.transform.localScale.x;
        this.tamanhoRealDaImagem = tamanhoImagem * escala;
    }

    private void Update()
    {
        // Acumula deslocamento ao longo do tempo
        deslocamentoAcumulado += velocidade * Time.deltaTime;

        // Usa Repeat para repetir o deslocamento quando exceder o tamanho da imagem
        float deslocamento = Mathf.Repeat(deslocamentoAcumulado, tamanhoRealDaImagem);

        // Atualiza a posição do objeto
        this.transform.position = this.posicaoInicial + Vector3.left * deslocamento;
    }
}
