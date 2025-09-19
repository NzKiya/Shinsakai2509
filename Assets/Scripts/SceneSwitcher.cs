using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void MoveToTitleScene()
    {
        //SceneManager.LoadScene("TitleScene");
        StartCoroutine(LoadSceneCoroutine("TitleScene"));
    }

    public void MoveToPuzzleScene()
    {
        StartCoroutine(LoadSceneCoroutine("PuzzleGameScene"));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;  
        }
    }
}
