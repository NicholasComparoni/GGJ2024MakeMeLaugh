using UnityEngine;

public class ExclamativePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<CharacterTarget>()._xclPoint = this;
        gameObject.SetActive(false);
    }
}
