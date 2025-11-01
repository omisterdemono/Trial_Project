using System;
using UnityEngine;

public class CustomColliderTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _mainGameObject;

    public GameObject MainGameObject { get => _mainGameObject; }

    public event Action<GameObject> OnTriggerEnter;
    public event Action<GameObject> OnTriggerExit;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision.GetComponent<CustomColliderTrigger>().MainGameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(collision.GetComponent<CustomColliderTrigger>().MainGameObject);
    }
}
