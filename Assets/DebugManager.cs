using UnityEngine;

public class DebugManager : MonoBehaviour
{

    void Start()
    {

    }


    public void ADDTEST() // 돈추가
    {

        Managers.Data.userData.gold += 1000;
    }
}
