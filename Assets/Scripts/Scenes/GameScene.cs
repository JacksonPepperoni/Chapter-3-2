using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Managers.UI.ShowSceneUI<UI_Scene_Game>();
        Managers.Game.player = Managers.Resource.InstantiatePrefab("Player.prefab").GetComponent<Player>();

        return true;
    }
}