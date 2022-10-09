using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class UIHandle : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Title;
    public TMP_InputField playerName;
    private void Awake()
    {
        
    }
    void Start()
    {
        //Debug.Log(PlayerInfo.Instance.highestScore);
        Title.text = Title.text + " : " + PlayerInfo.Instance.highestPlayer + " : " + PlayerInfo.Instance.highestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {        
        PlayerInfo.Instance.playerName = playerName.text;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}
