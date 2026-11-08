using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
   [SerializeField] private Button PlayButton;
   [SerializeField] private Button QuitButton;

   void Awake()
   {
      PlayButton.onClick.AddListener(() =>
      {
       Loader.Load(Loader.Scene.GameScene);
      });

      QuitButton.onClick.AddListener(() =>
      {
         Application.Quit();
      });
   }
}
