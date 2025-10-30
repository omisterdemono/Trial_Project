using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    [SerializeField] private GameObject _interationPopup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _interationPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _interationPopup.SetActive(false);
        }
    }
}
