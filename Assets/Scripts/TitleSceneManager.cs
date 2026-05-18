using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public void OnClickStart()
    {
        Debug.Log("게임 시작! VillageScene 이동");

        SceneManager.LoadScene("VillageScene");
    }

    public void OnClickSettings()
    {
        Debug.Log("설정창 팝업 열기");
    }
}