using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickableItem : MonoBehaviour
{
    [Header("아이템 정보")]
    [SerializeField] private string itemName = "날카로운 조각";
    [SerializeField] private UnityEngine.Sprite itemIcon;

    [Header("상호작용")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("연출")]
    [SerializeField] private GameObject interactPrompt; // None이어도 됨

    private bool playerNearby = false;
    private bool picked = false;

    private void Update()
    {
        if (playerNearby && !picked && Input.GetKeyDown(interactKey))
            Pickup();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerNearby = true;
        if (interactPrompt != null) interactPrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerNearby = false;
        if (interactPrompt != null) interactPrompt.SetActive(false);
    }

    public void Pickup()
    {
        if (picked) return;
        picked = true;
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (Inventory.Instance != null) Inventory.Instance.AcquireItem(itemName, itemIcon);
        Destroy(gameObject);
    }
}