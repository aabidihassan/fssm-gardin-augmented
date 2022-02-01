using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private bool isPause = true;

    
    public bool IsPause
    { 
        get { return isPause; }

        set { isPause = value; }
            }

    public void pause() {
        isPause = true;
    }

    public void play() {

        isPause = false;
            }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
