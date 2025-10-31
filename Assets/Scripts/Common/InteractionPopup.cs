using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    private const string PlayerTag = "Player";
    [SerializeField] private GameObject _interationPopup;
    [SerializeField] private CustomColliderTrigger _interactRadius;

    private void Awake()
    {
        _interactRadius = GetComponent<CustomColliderTrigger>();
    }

    private void OnEnable()
    {
        _interactRadius.OnTriggerEnter += ShowPopup;
        _interactRadius.OnTriggerExit += HidePopup;
    }

    private void OnDisable()
    {
        _interactRadius.OnTriggerEnter -= ShowPopup;
        _interactRadius.OnTriggerExit -= HidePopup;
    }

    private void ShowPopup(GameObject gameObject)
    {
        _interationPopup.SetActive(true);
    }

    private void HidePopup(GameObject gameObject)
    {
        _interationPopup.SetActive(false);
    }
}
