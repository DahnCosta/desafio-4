using UnityEngine;

public class CenarioInfinito : MonoBehaviour
{

     [SerializeField] public float velocidade;
    private Vector3 posicaoInicial;
    private float tamanhoRealDaImagem;

    private void Awake()
    {
        this.posicaoInicial = this.transform.position;
        float tamanhoRealDaImagem = this.GetComponent<SpriteRenderer>().size.x;
        float escala = this.transform.localScale.x;
        this.tamanhoRealDaImagem = tamanhoRealDaImagem * escala;
    }
    void Update()
    {
        float deslocamento = Mathf.Repeat(this.velocidade * Time.time, this.tamanhoRealDaImagem);
        this.transform.position = this.posicaoInicial + Vector3.left * deslocamento;
    }

    

}
