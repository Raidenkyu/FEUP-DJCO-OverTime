using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int horizontalRes;
    private int verticalRes;
    private FullScreenMode mode;

    void Start()
    {
        horizontalRes = 1920;
        verticalRes = 1080;
        mode = FullScreenMode.FullScreenWindow;
    }

    public void changeRes(int index){
        switch(index){
            case 0:
            horizontalRes = 1920;
            verticalRes = 1080;
            break;
            case 1:
            horizontalRes = 1600;
            verticalRes = 900;
            break;
            case 2:
            horizontalRes = 1440;
            verticalRes = 900;
            break;
            case 3:
            horizontalRes = 1366;
            verticalRes = 765;
            break;
            case 4:
            horizontalRes = 1280;
            verticalRes = 1024;
            break;
            case 5:
            horizontalRes = 1280;
            verticalRes = 720;
            break;
            case 6:
            horizontalRes = 800;
            verticalRes = 640;
            break;
            case 7:
            horizontalRes = 640;
            verticalRes = 480;
            break;
        }

        applyChanges();

    }

    public void changeScreenMode(int index){
        switch(index){
            case 0:
            mode = FullScreenMode.FullScreenWindow;
            break;
            case 1:
            mode = FullScreenMode.MaximizedWindow;
            break;
            case 2:
            mode = FullScreenMode.ExclusiveFullScreen;
            break;
            case 3:
            mode = FullScreenMode.Windowed;
            break;
        }
      
        applyChanges();
    }
    private void applyChanges(){
        Screen.SetResolution(horizontalRes, verticalRes,mode, 60);
    }
}
