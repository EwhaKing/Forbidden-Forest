using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("상호작용")]
    [SerializeField] private float interactRadius = 1.2f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    // 컴포넌트
    private Rigidbody2D rb;
    private Animator anim;

    // 상태
    private Vector2 moveInput;

    // 애니메이터 파라미터 (나중에 Animator에 같은 이름으로 추가)
    private static readonly int AnimSpeed  = Animator.StringToHash("Speed");
    private static readonly int AnimMoveX  = Animator.StringToHash("MoveX");
    private static readonly int AnimMoveY  = Animator.StringToHash("MoveY");

    private void Awake()
    {
        rb   = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // ── 입력 ──────────────────────────────────────
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // ── 애니메이터 ────────────────────────────────
        anim.SetFloat(AnimSpeed,  moveInput.magnitude);
        anim.SetFloat(AnimMoveX,  moveInput.x);
        anim.SetFloat(AnimMoveY,  moveInput.y);

        // ── 상호작용 ──────────────────────────────────
        if (Input.GetKeyDown(interactKey))
            TryInteract();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    // ── 상호작용 감지 ─────────────────────────────────
    // 나중에 NPC, 오브젝트 조사 등 IInteractable 붙이면 여기서 호출됩니다
    private void TryInteract()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactLayer);

        float minDist = float.MaxValue;
        PickableItem nearestItem = null;

        foreach (var hit in hits)
        {
            PickableItem item = hit.GetComponent<PickableItem>();
            if (item == null) continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist) { minDist = dist; nearestItem = item; }
        }

        // 아이템 획득
        if (nearestItem != null)
        {
            nearestItem.Pickup();
            return;
        }

        // TODO: 여기에 NPC 대화, 오브젝트 조사 등 추가
        // IInteractable interactable = ...
        // interactable?.Interact();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
