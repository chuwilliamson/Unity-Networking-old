using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour
{
    [SyncVar]
    public int damagePerShot = 20;
    [SyncVar]
    public float timeBetweenBullets = 0.15f;
    [SyncVar]
    public float range = 100f;
    public Transform GunBarrelEnd;

    [SyncVar]
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;
    float effectsDisplayTime = 0.2f;
    public Light faceLight;								 


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (!isLocalPlayer)
            return;
      

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    
    public void DisableEffects()
    {
        if (!Network.isServer)
            CmdDisableEffects();
        else
            RpcDisableEffects();
    }

    [Command]
    void CmdDisableEffects()
    {
        RpcDisableEffects();
    }

    [ClientRpc]
    void RpcDisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        faceLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0;
        if(!Network.isServer)
            CmdShoot();
        else
            RpcShoot();
    }

    [Command]
    void CmdShoot()
    {
        gunAudio.Play();
        RpcShoot();
    }

    [ClientRpc]
    void RpcShoot()
    {
        timer = 0f;

        // Play the gun shot audioclip.
        
        

        // Enable the lights.
        gunLight.enabled = true;
        faceLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, GunBarrelEnd.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = GunBarrelEnd.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            gunLine.SetPosition(1, shootHit.point);
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    }
 
}
