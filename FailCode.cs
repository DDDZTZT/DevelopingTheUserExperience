using UnityEngine;
using UnityEngine.SceneManagement;

public class FailCode : MonoBehaviour
{
    [Header("Y Position Trigger")]
    public float triggerY = -5f;

    [Header("Target Scene Index")]
    public int targetSceneIndex = 1;

    private bool isTriggered = false;

    void Update()
    {
        if (!isTriggered && transform.position.y <= triggerY)
        {
            isTriggered = true;
            SceneManager.LoadScene(targetSceneIndex);
        }
    }
}