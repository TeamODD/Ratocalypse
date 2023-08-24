using UnityEngine;
using UnityEngine.UI;


public class StatusUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _statusPrefab;
    [SerializeField]
    private Sprite _poisonSprite;
    [SerializeField]
    private Sprite _etcStatusSprite;

    public void SetStatus(string status)
    {
        GameObject statusUi = Instantiate(_statusPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        statusUi.name = status;

        if (status == "Poison")
        {
            statusUi.GetComponent<Image>().sprite = _poisonSprite;
        }
        else if (status == "Etc")
        {
            statusUi.GetComponent<Image>().sprite = _etcStatusSprite;
        }
        else
        {
            Debug.Log("wrong status name");
            return;
        }

        statusUi.AddComponent<BoxCollider2D>();
        statusUi.GetComponent<BoxCollider2D>().size = new Vector2(20, 20);
        statusUi.GetComponent<Transform>().SetParent(transform.Find("Status"), false);
    }

    public void RemoveStatus(string status)
    {
        Destroy(transform.Find("Status/" + status).gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetStatus("Poison");
        SetStatus("Poison");
        SetStatus("Poison");
        SetStatus("Etc");
        RemoveStatus("Etc");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
