using UnityEngine;

public class SpawnBaseController : MonoBehaviour
{
    [SerializeField] private GameObject _basePrefabe;
    [SerializeField] private int _dronCount;
    void Start()
    {
        GameObject firstPrefabe = Instantiate(_basePrefabe);
        firstPrefabe.transform.position = transform.position;
        firstPrefabe.GetComponent<BaseController>().GenerateDrones(_dronCount);
    }

}
