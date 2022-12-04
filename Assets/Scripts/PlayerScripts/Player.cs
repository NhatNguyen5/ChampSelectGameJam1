using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Player : MonoBehaviour
{
    private StarterAssetsInputs SAInputs;
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
}
