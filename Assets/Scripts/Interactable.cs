using UnityEngine;

public class Interactable : MonoBehaviour
{
    // [기능 추가 단계용 대비] 이 아이템이 가질 대사 내용을 담을 변수
    [TextArea(3, 5)]
    public string dialogueText = "여기에 아이템 대사를 입력하세요.";

    private bool isPlayerNearby = false;

    void Update()
    {
        // 플레이어가 근처에 있고, E 키를 눌렀을 때 작동
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        // 1단계 목표: 콘솔창에 로그 출력하기
        Debug.Log($"[상호작용] {gameObject.name}과 상호작용함! 대사: {dialogueText}");

        // TODO: 여기서 실제 텍스트 박스 UI를 띄우고 dialogueText를 넘겨주는 처리를 할 예정입니다.
    }

    // 플레이어가 감지 범위(Collider 2D/3D 트리거)에 들어왔을 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("플레이어 접근 - E키를 눌러 상호작용 하세요.");
        }
    }

    // 플레이어가 감지 범위를 벗어났을 때
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}