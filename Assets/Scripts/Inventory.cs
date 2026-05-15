using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [Header("UI 참조")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;

    [Header("획득 연출")]
    [SerializeField] private Animator slotAnimator;
    private static readonly int AcquireTrigger = Animator.StringToHash("Acquire");

    public bool HasItem { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        ClearSlot();
    }

    public void AcquireItem(string itemName, Sprite icon = null)
    {
        if (HasItem) return;
        HasItem = true;

        if (icon != null)
        {
            itemIcon.sprite = icon;
            itemIcon.gameObject.SetActive(true);
        }

        itemNameText.text = itemName;
        if (slotAnimator != null) slotAnimator.SetTrigger(AcquireTrigger);
    }

    private void ClearSlot()
    {
        HasItem = false;
        itemIcon.gameObject.SetActive(false);
        itemNameText.text = "";
    }
}