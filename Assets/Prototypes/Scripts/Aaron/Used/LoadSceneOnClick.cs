using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public void LoadSceneNum(int num)
    {
        if(num<0||num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Cant Load scene num" + num + ", SceneManager only has" + SceneManager.sceneCountInBuildSettings + " scenes in BuildSettings");
            return;
        }

        LoadingScreenManager.LoadScene(num);
    }   
}