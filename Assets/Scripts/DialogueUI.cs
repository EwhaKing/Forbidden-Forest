using UnityEngine;
using TMPro; // TextMeshPro를 쓰기 위해 꼭 필요합니다!

public class DialogueUI : MonoBehaviour
{
    // 어디서나 이 UI를 쉽게 부를 수 있도록 '싱글톤'으로 만듭니다.
    public static DialogueUI Instance { get; private set; }

    [Header("UI 셋업")]
    public GameObject dialogueBoxObject; // 껐다 켤 대사창 부모 패널
    public TMP_Text dialogueText;        // 글자가 바뀔 텍스트 컴포넌트

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
            // 씬이 바뀌어도 파괴되지 않게 하려면 아래 줄 활성화 (선택)
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        // 게임 시작할 때는 대사창을 깔끔하게 숨겨둡니다.
        CloseDialogue();
    }

    // 대사창을 켜고 텍스트를 보여주는 함수
    public void ShowDialogue(string text)
    {
        dialogueBoxObject.SetActive(true);
        dialogueText.text = text;
    }

    // 대사창을 닫는 함수
    public void CloseDialogue()
    {
        dialogueBoxObject.SetActive(false);
    }
}