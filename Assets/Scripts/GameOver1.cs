using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver1 : MonoBehaviour
{

    public void RestartGame()
    {
        
        SceneManager.LoadScene("SampleScene");
    
    }


}

