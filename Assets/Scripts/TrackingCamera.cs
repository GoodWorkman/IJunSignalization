using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    [Header("Target Transform")]
    [SerializeField] private GameObject _target;

    [Header("Camera Options")] 
    [SerializeField] private float _height = 6f;
    [SerializeField] private float _rareDistance = 10f;
    [SerializeField] private float _returnSpeed = 2f;

    private void Start()
    {
        Vector3 targetPosition = _target.transform.position;
        
        transform.position = new Vector3(targetPosition.x, targetPosition.y + _height,
            targetPosition.z - _rareDistance);

        transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
    }

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPosition = _target.transform.position;
        
        Vector3 currentVector = new Vector3(targetPosition.x, targetPosition.y + _height,
            targetPosition.z - _rareDistance);

        transform.position = Vector3.Lerp(transform.position, currentVector, _returnSpeed * Time.deltaTime);
    }
}
