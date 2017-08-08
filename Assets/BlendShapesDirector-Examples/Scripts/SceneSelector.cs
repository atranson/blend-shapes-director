using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlendShapesDirector.Examples
{
    public class SceneSelector : MonoBehaviour
    {

        public void SwitchToAvatarDemo()
        {
            SceneManager.LoadScene("Avatar", LoadSceneMode.Single);
        }

        public void SwitchToCubeDemo()
        {
            SceneManager.LoadScene("Cube", LoadSceneMode.Single);
        }

    }
}