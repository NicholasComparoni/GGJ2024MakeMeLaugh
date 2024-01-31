using System.Transactions;
using UnityEngine;

public class ExclamativePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.TryGetComponent(out CharacterTarget target))
        {
            target._xclPoint = this;
        }

        if (transform.parent.TryGetComponent(out Chest chest))
        {
            chest._xclPoint = this;
        }
        if (transform.parent.TryGetComponent(out CleaningTable table))
        {
            table._xclPoint = this;
        }
        gameObject.SetActive(false);
    }
}
