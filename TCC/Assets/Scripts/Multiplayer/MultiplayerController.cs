using UnityEngine;
using Photon.Pun;

public class MultiplayerController : MonoBehaviour
{
    public PhotonView m_PhotonView;
    public GameObject cameraPrefab;
    public Transform camSpawnPoint;
    public Animator animator;
    private PlayerController m_CharacterController;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = PlayerController.instance;
        cameraPrefab.GetComponent<Camera3rdPerson>().targetCamera = this.transform;
        GameObject _cam = Instantiate(cameraPrefab, camSpawnPoint.position, Quaternion.identity);
        m_CharacterController.movement.cam = _cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
