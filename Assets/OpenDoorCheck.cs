using UnityEngine;

public class OpenDoorCheck : MonoBehaviour
{
    [SerializeField] private GameObject _doorToOpen;

    public void OpenDoor()
    {
        _doorToOpen.gameObject.GetComponent<Collider2D>().enabled = false;
    }
}