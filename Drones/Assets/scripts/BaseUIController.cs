using UnityEngine;
using UnityEngine.UI;

public class BaseUIController : MonoBehaviour
{
    [SerializeField] private GameObject _flagPrefabe;
    [SerializeField] private Text _countText;
    [SerializeField] private BaseController _baseController;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void CreateFlag()
    {
        GameObject _newFlag = Instantiate(_flagPrefabe);
        _newFlag.transform.position = transform.position;
        _newFlag.GetComponent<FlagController>().CreateFlag();
        _baseController.AddFlag(_newFlag);
    }
}
