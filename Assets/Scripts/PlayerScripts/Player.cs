using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    private StarterAssetsInputs SAInputs;
    private ThirdPersonController TPController;
    public Transform AimTarget;
    [SerializeField] private CinemachineVirtualCamera aimVirCam;
    [SerializeField] private LayerMask IgnoreLayer;
    public GameObject crossHair;
    public bool aiming { get; private set; }
    private Vector3 defaultATPos;

    // Start is called before the first frame update
    void Start()
    {
        SAInputs = GetComponent<StarterAssetsInputs>();
        TPController = GetComponent<ThirdPersonController>();
        Debug.Log(AimTarget.position);
        defaultATPos = AimTarget.localPosition;
        GameManager.SAInputs = SAInputs;
        GameManager.UpdateGameState(GameManager.gameState == GameState.Playing ? GameState.Pause : GameState.Playing);
    }

    private void OnDestroy()
    {
        GameManager.SAInputs = null;
        SAInputs = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!SAInputs.isPaused)
        {
            Vector3 mouseWorldPos = Vector3.zero;
            aimVirCam.transform.gameObject.SetActive(aiming);
            crossHair.SetActive(aiming);
            if (aiming)
            {
                Debug.Log("Aiming");
                Vector2 rayOrigin = new Vector2(Screen.width / 2f, Screen.height / 2f); // center of the screen

                // actual Ray
                Ray ray = Camera.main.ScreenPointToRay(rayOrigin);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 999f, ~IgnoreLayer))
                {
                    Debug.Log("Hit " + hit.transform.gameObject.name);
                    AimTarget.transform.position = Vector3.Lerp(AimTarget.transform.position, hit.point, Time.deltaTime * 200f);
                    mouseWorldPos = hit.point;
                }
                else
                {
                    Vector3 tempFocusTarget = Camera.main.transform.position + Camera.main.transform.forward * 200f;
                    AimTarget.transform.position = Vector3.Lerp(AimTarget.transform.position, tempFocusTarget, Time.deltaTime * 200f);
                    mouseWorldPos = tempFocusTarget;
                }
                Vector3 worldAimTarget = mouseWorldPos;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDir = (worldAimTarget - transform.position).normalized;
                TPController.setRotateOnMove(false);
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
            }
            else
            {
                TPController.setRotateOnMove(true);
                AimTarget.localPosition = defaultATPos;
            }

        }
    }

    public void OnPause()
    {
        SAInputs.isPaused = !SAInputs.isPaused;
        GameManager.UpdateGameState(GameManager.gameState == GameState.Playing ? GameState.Pause : GameState.Playing);
        Debug.Log("Pause");
    }

    public void OnAim(InputValue value)
    {
        AimInput(value.isPressed);
    }

    public void AimInput(bool newAimState)
    {
        aiming = newAimState;
    }

    public void OnFire()
    {

    }
}
