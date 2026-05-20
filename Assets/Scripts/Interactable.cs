using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("대사 설정 (인펙터에서 여러 줄 입력 가능)")]
    [TextArea(3, 5)]
    public string[] dialogues; // 1줄이 아니라 여러 줄을 담는 배열로 변경!

    private bool isPlayerNearby = false;
    private bool isInteracting = false; // 현재 대화 중인지 체크
    private int currentDialogueIndex = 0; // 현재 몇 번째 대사를 보여주고 있는지

    void Update()
    {
        // 플레이어가 근처에 있고, E 키를 눌렀을 때
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // 아직 대화 시작 전이라면 첫 대사 출력
            if (!isInteracting)
            {
                StartDialogue();
            }
            // 이미 대화 중이라면 다음 대사로 넘어가기
            else
            {
                NextDialogue();
            }
        }
    }

    // 대화 시작
    void StartDialogue()
    {
        if (dialogues.Length == 0) return; // 적힌 대사가 없으면 리턴

        isInteracting = true;
        currentDialogueIndex = 0;

        // DialogueUI에게 첫 번째 대사를 띄우라고 명령함
        DialogueUI.Instance.ShowDialogue(dialogues[currentDialogueIndex]);
    }

    // 다음 대사 출력 또는 닫기
    void NextDialogue()
    {
        currentDialogueIndex++;

        // 아직 보여줄 대사가 더 남아있다면 다음 줄 출력
        if (currentDialogueIndex < dialogues.Length)
        {
            DialogueUI.Instance.ShowDialogue(dialogues[currentDialogueIndex]);
        }
        // 모든 대사를 다 보여줬다면 대사창 닫기
        else
        {
            EndDialogue();
        }
    }

    // 대화 종료
    void EndDialogue()
    {
        isInteracting = false;
        DialogueUI.Instance.CloseDialogue();
        Debug.Log($"{gameObject.name}과의 대화 종료!");

        // TODO: 여기서 기획서에 있던 "모든 대사가 떴을 때 발동할 효과"들을 연결할 예정입니다.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("플레이어 접근 - E키를 눌러 상호작용 하세요.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            // 대화 중에 플레이어가 도망치면 대사창을 닫아버림
            if (isInteracting)
            {
                EndDialogue();
            }
        }
    }
}