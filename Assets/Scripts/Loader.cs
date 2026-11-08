using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{

    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoandingScene
    }
   private static Scene targetScene ;


     public static void Load(Scene targetScene)
     {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoandingScene.ToString());
     }

     public static void CallBack()
     {
        SceneManager.LoadScene(targetScene.ToString());
     }
     
}
