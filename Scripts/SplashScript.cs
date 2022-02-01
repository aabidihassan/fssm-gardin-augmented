using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    private bool finished = false;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(Fade());
    }


    IEnumerator Fade() {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1 ) {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        yield return finished=true;
    }


    private void LateUpdate()
    {
        if (finished)
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

}
