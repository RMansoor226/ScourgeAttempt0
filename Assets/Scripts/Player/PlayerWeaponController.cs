using UnityEngine;
using UnityEngine.Experimental.Audio;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask hitMask;
    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputHandler.FireHeld)
        {
            Debug.Log("Fire Held is: "+ _inputHandler.FireHeld);
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        RaycastHit hit;
        Debug.DrawRay(
            camera.transform.position, 
            camera.transform.forward, 
            Color.red,
            5.0f);

        if (Physics.Raycast(
                camera.transform.position,
                camera.transform.forward,
                out hit, 
                50f,
                hitMask))
        {
            Debug.Log("Weapon has been fired!");
            Debug.Log("Object Hit: " + hit.collider.gameObject.name);
            
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(25f);
            }
        }
    }
}
