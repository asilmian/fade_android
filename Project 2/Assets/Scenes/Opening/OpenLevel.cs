using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevel : MonoBehaviour
{

    public void getLevel(GameObject button)
    {
        if (button.name.Equals("Tutorial"))
            SceneManager.LoadScene("Level1");
        else if (button.name.Equals("Level1"))
            SceneManager.LoadScene("Level2");
        else if (button.name.Equals("Level2"))
            SceneManager.LoadScene("Level3");
    }
}
