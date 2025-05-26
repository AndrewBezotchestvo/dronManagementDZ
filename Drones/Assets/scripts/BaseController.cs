using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    private int _resoursesCount = 0;
    private List<GameObject> _drones;
    private Stack<GameObject> _dronesOnTheBase;
    private Queue<GameObject> _flags;

    [SerializeField] private Text _countText;
    [SerializeField] private GameObject _dronePrefabe;
    [SerializeField] private int _dronesCount;
    [SerializeField] private float _searchRadius = 20;
    [SerializeField] private float _getResourseRadius = 1;

    private void OnEnable()
    {
        _drones = new List<GameObject>();
        _dronesOnTheBase = new Stack<GameObject>();
        _flags = new Queue<GameObject>();
    }

    void Update()
    {
        CheckResourses();

        CheckDronesDistance();

        PlaceDrones();


        if (_flags.Count <= 0 || _drones.Count <= 1)
        {            
            GenerateNewDrone();
        }
        else
        {
            BuildNewBase();
        }

        //_countText.text = _resoursesCount.ToString();
    }

    public void GenerateDrones(int dronesCount)
    {
        for(int i = 0; i < dronesCount; i++)
        {
            GameObject newDrone = Instantiate(_dronePrefabe);
            newDrone.transform.parent = transform;
            _dronesOnTheBase.Push(newDrone);
            _drones.Add(newDrone);
        }
    }

    private void BackDronOnBase(GameObject dron)
    {
        _dronesOnTheBase.Push(dron);
    }

    private void GetResourse(GameObject Resourse)
    {
        ResoursePool._instance.ReturnResourse(Resourse);
    }

    private void CheckResourses()
    {
        Collider[] resourses = Physics.OverlapSphere(transform.position, _searchRadius);

        foreach (Collider r in resourses)
        {
            if (_dronesOnTheBase.Count > 0)
            {
                if (r.gameObject.tag != "Resourse" || !r.GetComponent<ResourseController>().GetAvailable())
                {
                    continue;
                }
                GameObject dron = _dronesOnTheBase.Pop();
                dron.GetComponent<DronController>().SatTarget(r.gameObject);
                r.GetComponent<ResourseController>().SetDron();
            }
        }
    }

    private void BuildNewBase()
    {
        if (_resoursesCount >= 5)
        {
            GameObject flag = _flags.Dequeue();
            GameObject dron = _dronesOnTheBase.Pop();
            dron.GetComponent<DronController>().SatTarget(flag);
            _drones.Remove(dron);
            _resoursesCount -= 5;
        }
    }

    private void GenerateNewDrone()
    {
        if (_resoursesCount >= 3)
        {
            GenerateDrones(1);
            _resoursesCount -= 3;
        }
    }

    private void PlaceDrones()
    {
        for (int i = 0; i < _dronesOnTheBase.Count; i++)
        {
            _dronesOnTheBase.ToArray()[i].transform.position = transform.position + new Vector3(1.5f, i, 0);
        }
    }

    private void CheckDronesDistance()
    {
        foreach (GameObject dron in _drones)
        {
            if (Vector3.Distance(dron.transform.position, transform.position) < _getResourseRadius)
            {
                if (dron.GetComponent<DronController>().GetStatus())
                {
                    GetResourse(dron.GetComponent<DronController>().GetResourse());
                    BackDronOnBase(dron);
                    _resoursesCount++;
                    _countText.text = _resoursesCount.ToString();
                }
            }
        }
    }

    public void AddFlag(GameObject flag)
    {
        _flags.Enqueue(flag);
    }

    public void NewBaseSettings()
    {
        _drones = new List<GameObject>();
        _dronesOnTheBase = new Stack<GameObject>();
        _flags = new Queue<GameObject>();
        _resoursesCount = 0;
        _dronesCount = 1;
    }
}
