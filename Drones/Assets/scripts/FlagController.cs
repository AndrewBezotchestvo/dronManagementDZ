using UnityEngine;
using UnityEngine.InputSystem;

public class FlagController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _basePrefabe;
    private bool _isActive = false;
    private bool _canStand = false;
    private bool _isCreated = false;
    private GameObject _base;

    private Ray ray;
    private Ray downRay;

    private void OnEnable()
    {
        _isActive = false;
        _canStand = false;
        _isCreated = false;
    }

    void Update()
    {
        if (_isCreated)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag != "Dron") {return;}

                if (collider.gameObject.GetComponent<DronController>().GetTarget() == gameObject)
                {
                    GameObject newBase = Instantiate(_basePrefabe);
                    newBase.transform.position = transform.position;
                    newBase.GetComponent<BaseController>().NewBaseSettings();
                    newBase.GetComponent<BaseController>().GenerateDrones(1);
                    DestroyFlag();
                    Destroy(collider.gameObject);
                }
            }
        }

        if (!_isActive || _isCreated) return;

        SetMousePosition();

        CheckGround();

        if (Input.GetMouseButtonDown(0) && _canStand)
        {
            _canStand = false;
            _isActive = false;
            _isCreated = true;
        }
    }

    public void CreateFlag()
    {
        _isActive = true;
    }

    public void DisableFlag()
    {
        gameObject.SetActive(false);
        _isActive = false;
    }

    public void DestroyFlag()
    {
        Destroy(gameObject);
    }

    private void SetRedColor()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
        transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.red;
    }
    private void SetNormalColor()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
        transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
    }
    
    private void CheckGround()
    {
        downRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(downRay, out hit, 1))
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                _canStand = true;
                SetNormalColor();
            }
            else
            {
                _canStand = false;
                SetRedColor();
            }
        }
    }

    private void SetMousePosition()
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, 10 * Time.deltaTime);
        }
    }

    private void CreateBase()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<DronController>().GetTarget() == gameObject)
            {
                GameObject newBase = Instantiate(_basePrefabe);
                newBase.transform.position = transform.position;
                DestroyFlag();
            }
        }
    }

}

