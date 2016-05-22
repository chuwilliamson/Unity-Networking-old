using UnityEngine;
using UnityEngine.Networking;

public class SpherePlayer : NetworkBehaviour
{  
    public GameObject cam;

    void Start()
    {   
        if (!isLocalPlayer)
            cam.SetActive(false);        
    }
    // Update is called once per frame
    [ClientCallback]
    void SimpleUpdate()
    {        
        if (!isLocalPlayer)
            return;
     
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
      
        Vector3 move = new Vector3(h, 0, v);
        
        transform.Translate(move);
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }


    /// <summary>
    /// http://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    /// </summary>
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    [ClientCallback]
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        var f = Input.GetAxis("Mouse ScrollWheel") * 15.0f;
        cam.transform.Translate(new Vector3(0, 0, f));
    }
}

