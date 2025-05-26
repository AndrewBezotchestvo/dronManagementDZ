
using System.Collections.Generic;
using UnityEngine;

public class ResoursePool : MonoBehaviour
{
    public static ResoursePool _instance;

    [SerializeField] private List<GameObject> _resourscesPrefabes;

    [SerializeField] private int _poolSize = 10;

    private Queue<GameObject> _resourses = new Queue<GameObject>();

    private void Awake()
    {
        _instance = this;
        InitializePool(_poolSize);
    }

    private void InitializePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject resourse = Instantiate(_resourscesPrefabes[Random.Range(0, _resourscesPrefabes.Count - 1)]);
            resourse.SetActive(false);
            _resourses.Enqueue(resourse);
        }
    }

    public GameObject GetResourse()
    {
        if (_resourses.Count <= 0)
        {
            InitializePool(1);
        }

        GameObject newResourse = _resourses.Dequeue();
        newResourse.SetActive(true);
        return newResourse;
    }

    public void ReturnResourse(GameObject resourse)
    {
        resourse.SetActive(false);
        resourse.transform.localScale = Vector3.one;
        resourse.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        resourse.GetComponent<Collider>().enabled = true;
        _resourses.Enqueue(resourse);
    }
}
