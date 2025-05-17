using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [Header("Configuração da Câmera")]
    private Vector3 _cameraInitialPosition;      // Posição inicial da câmera
    public float _shakeMagnitude = 0.5f;          // Intensidade do shake
    public float _shakeTime = 0.5f;                // Duração do shake
    public Camera _mainCamera;                      // Referência à câmera principal

    public void ShakeIt()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        _cameraInitialPosition = _mainCamera.transform.position; // Salva a posição inicial da câmera
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);       // Inicia o shake repetidamente
        Invoke("StopCameraShaking", _shakeTime);                 // Para o shake após o tempo definido
    }

    void StartCameraShaking()
    {
        float offsetX = Random.value * _shakeMagnitude * 2 - _shakeMagnitude; // Valor aleatório no intervalo [-_shakeMagnitude, _shakeMagnitude]
        float offsetY = Random.value * _shakeMagnitude * 2 - _shakeMagnitude;
        Vector3 newPos = _mainCamera.transform.position;
        newPos.x = _cameraInitialPosition.x + offsetX;
        newPos.y = _cameraInitialPosition.y + offsetY;
        _mainCamera.transform.position = newPos; // Aplica o shake na câmera
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");               // Para a repetição do shake
        _mainCamera.transform.position = _cameraInitialPosition; // Restaura a posição original da câmera
    }
}
