using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 _movement;
    private Vector3 _newPosition;

    [SerializeField] private float _maxX;
    [SerializeField] private float _maxZ;
    [SerializeField] private float _minX;
    [SerializeField] private float _minZ;

    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _rotationSpeed;

    private float _angle;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";

    void Start()
    {
        
    }

    private void Update()
    {
        _movement.x = Input.GetAxis(Horizontal);
        _movement.z = Input.GetAxis(Vertical);

        _movement = transform.forward * _movement.z + transform.right * _movement.x;

        if (Input.GetMouseButton(1))
        {
            _angle += Input.GetAxis(MouseX) * _rotationSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _newPosition = transform.position + _movement;
        _newPosition.x = Mathf.Clamp(_newPosition.x, _minX, _maxX);
        _newPosition.z = Mathf.Clamp(_newPosition.z, _minZ, _maxZ);

        transform.position = _newPosition;
        transform.rotation = Quaternion.Euler(0, _angle * Time.fixedDeltaTime , 0);
    }
}
