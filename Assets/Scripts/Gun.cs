using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Gun : NetworkBehaviour
{
    [SerializeField] private GunSO[] guns;
    
    [SerializeField] 
    private NetworkVariable<int> m_SelectedGunId = new(0);

    public NetworkVariable<int> SelectedGunId => m_SelectedGunId;

    [SerializeField] private Vector3 projectileSpawnOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (guns.Length == 0) return;
        Debug.Log("Guns length: " + guns.Length);
        GetComponent<SpriteRenderer>().sprite = guns[SelectedGunId.Value].GetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // TODO: Make fire in any direction
                FireServerRpc(SelectedGunId.Value, new Vector2(5, 0));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateGunServerRpc();
            }
        }
    }

    void OnEnable()
    {
        SelectedGunId.OnValueChanged += OnSelectedGunIdChanged;
    }

    void OnDisable()
    {
        SelectedGunId.OnValueChanged -= OnSelectedGunIdChanged;
    }

    void OnSelectedGunIdChanged(int oldValue, int newValue)
    {
        if (!IsClient) return;
        
        GetComponent<SpriteRenderer>().sprite = guns[SelectedGunId.Value].GetSprite();
    }

    [ServerRpc]
    void UpdateGunServerRpc()
    {
        int newId = (SelectedGunId.Value + 1) % guns.Length;
        Debug.Log("Gun ID: " + newId);
        SelectedGunId.Value = newId;
    }

    [ServerRpc]
    void FireServerRpc(int gunID, Vector2 direction)
    {
        // TODO: Set rotation based on direction
        
        // Instantiate the projectile
        GameObject projectileObj = Instantiate(guns[gunID].GetProjectile(), transform.position + projectileSpawnOffset, Quaternion.identity);
        projectileObj.GetComponent<Projectile>().SetShooterPlayerID(OwnerClientId);
        // Spawn on network
        projectileObj.GetComponent<NetworkObject>().Spawn();
        // Send it on its merry way
        projectileObj.GetComponent<Rigidbody2D>().velocity = direction;
    }
}
