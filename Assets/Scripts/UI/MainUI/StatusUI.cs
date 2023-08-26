using System.Collections.Generic;
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

    public void SetStatuses(List<string> statuses)
    {
        foreach (string status in statuses)
        {
            SetStatus(status);
        }
    }

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

    public void RemoveAll()
    {
        foreach (Transform child in transform.Find("Status"))
        {
            Destroy(child.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
