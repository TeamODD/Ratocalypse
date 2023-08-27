using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private List<string> _loops;

    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;
        _text.text = _loops[(int)(_timer / 0.5f) % _loops.Count];

        if(_timer>5)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
