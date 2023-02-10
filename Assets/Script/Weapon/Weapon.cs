using Script.Player;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerController firePoint;

    void LateUpdate()
    {
        if (firePoint._targetLook.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180f, 0);
        }
    }
}