using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Música de Fundo")]
    public AudioSource _audioSourceMusicaDeFundo;
    public AudioClip _musicaDeFundo;

    [Header("Efeitos Sonoros")]
    public AudioSource _audioSourceEfeitos;  // Fonte separada para efeitos sonoros
    public AudioClip _somPulo;               // Som do pulo
    public AudioClip _somDano;               // Som do dano (novo)

    void Start()
    {
        // Inicia a música de fundo em loop com volume reduzido
        if (_audioSourceMusicaDeFundo != null && _musicaDeFundo != null)
        {
            _audioSourceMusicaDeFundo.clip = _musicaDeFundo;
            _audioSourceMusicaDeFundo.loop = true;
            _audioSourceMusicaDeFundo.volume = 0.2f;
            _audioSourceMusicaDeFundo.Play();
        }

        if (_audioSourceEfeitos == null)
        {
            Debug.LogWarning("AudioSource de efeitos não atribuído no AudioController.");
        }
    }

    void Update()
    {
        // Teste: aperte K para tocar o som de dano
        if (Input.GetKeyDown(KeyCode.K))
        {
            TocarSomDano();
        }
    }

    // Toca o som de pulo através do AudioSource de efeitos
    public void TocarSomPulo()
    {
        if (_audioSourceEfeitos != null && _somPulo != null)
        {
            _audioSourceEfeitos.PlayOneShot(_somPulo);
        }
        else
        {
            Debug.LogWarning("Som de pulo ou AudioSource de efeitos não está atribuído.");
        }
    }

    // Toca o som de dano através do AudioSource de efeitos
    public void TocarSomDano()
    {
        if (_audioSourceEfeitos != null && _somDano != null)
        {
            _audioSourceEfeitos.PlayOneShot(_somDano);
        }
        else
        {
            Debug.LogWarning("Som de dano ou AudioSource de efeitos não está atribuído.");
        }
    }
}
