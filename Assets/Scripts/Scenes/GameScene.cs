using Cinemachine;

using UnityEditor.U2D.Animation;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Managers.UI.ShowSceneUI<UI_Scene_Game>();
        Managers.Game.player = Managers.Resource.InstantiatePrefab("Player.prefab").GetComponent<Player>();

        _virtualCamera.m_Follow = Managers.Game.player.transform;


        return true;
    }
}