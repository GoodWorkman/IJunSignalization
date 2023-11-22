using UnityEngine;

public class CameraTestSimple : MonoBehaviour
{
   // Main Камера вложена в пустой объект, на котором висит этот скрипт, трансформ пустышки долж совпадать с трансформом
   // игрока (цели). Камеру размещаем под тем углом/положением, которое нам нужно, слегка выше-сзади игрока, и она как дочерний
   // объект следует за игроком плавно, из-за лерпа.
   // LateUpdate необходимо использовать на скриптах камеры, поскольку он срабатывает после всех апдейтов, что обеспечит
   // правильную отрисовку без задержек.
    
    [SerializeField] private Transform _target;

    private float _lerpRate = 10f;

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, Time.deltaTime * _lerpRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, _target.transform.rotation, Time.deltaTime * _lerpRate);
    }
}

