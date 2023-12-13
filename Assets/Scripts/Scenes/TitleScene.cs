using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

   
     UI = Managers.UI.ShowSceneUI<UI_Scene_Title>();


        return true;
    }
}