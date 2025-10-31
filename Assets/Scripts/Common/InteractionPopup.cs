using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    private const string PlayerTag = "Player";
    [SerializeField] private GameObject _interationPopup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(PlayerTag))
        {
            _interationPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            _interationPopup.SetActive(false);
        }
    }
}
