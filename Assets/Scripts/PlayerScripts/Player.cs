using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEditor.PackageManager;

public class Player : MonoBehaviour
{
    private StarterAssetsInputs SAInputs;
    public Transform AimTarget;
    private Ray RayOrigin;
    private RaycastHit HitInfo;

    // Start is called before the first frame update
    void Start()
    {
        SAInputs = GetComponent<StarterAssetsInputs>();
        GameManager.SAInputs = SAInputs;
        GameManager.UpdateGameState(GameManager.gameState == GameState.Playing ? GameState.Pause : GameState.Playing);
    }

    private void OnDestroy()
    {
        GameManager.SAInputs = null;
        SAInputs = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause()
    {
        SAInputs.isPaused = !SAInputs.isPaused;
        GameManager.UpdateGameState(GameManager.gameState == GameState.Playing ? GameState.Pause : GameState.Playing);
        Debug.Log("Pause");
    }

    public void OnAim()
    {
        Debug.Log("Aiming");
        Transform cameraTransform = Camera.main.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 100.0f))
            Debug.Log(HitInfo.transform.gameObject.name);
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);
    }
}
