using UnityEngine;

public class WinBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Player.ToString()))
        {
            Debug.Log(other.gameObject.name + " WIN");
        }
    }
}
