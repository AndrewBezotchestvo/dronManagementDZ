using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DronController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _hight = 10;

    private GameObject _resourse;
    private bool _active = false;
    private bool _hasResourse = false;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
    }

   
    void Update()
    {
        if (_hasResourse)
        {
            BackOnBase();
            _resourse.transform.position = transform.position + new Vector3(0, 1, 0);
            return;
        }

        if (_active)
        {
            transform.position = new Vector3(transform.position.x, _hight, transform.position.z);
            _agent.SetDestination(_resourse.transform.position);

            if (Vector3.Distance(transform.position, _resourse.transform.position) < 0.1f)
            {
                _resourse.transform.parent = transform;
                _resourse.GetComponent<Collider>().enabled = false;
                _resourse.transform.position = transform.position + new Vector3(0, 1, 0);
                _hasResourse = true;
            }
        }
        else
        {
            _resourse = null;
            _agent.ResetPath();
        }
    }
    
    public void SatTarget(GameObject resourse)
    {
        _resourse = resourse;
        _active = true;
    }

    public GameObject GetTarget()
    {
        return _resourse;
    }

    public bool GetStatus()
    {
        return _hasResourse;
    }

    public GameObject GetResourse()
    {
        _resourse.transform.parent = null;
        _active = false;
        _hasResourse =false;
        return _resourse; 
    }

    public void BackOnBase()
    {
        _agent.SetDestination(transform.parent.gameObject.transform.position);
    }
}
