using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<PlayerWeaponModule> weaponModuleList;
    public PlayerWeaponModule nowUsingWeaponModule;

    public List<PlayerMovementModule> movementModuleList;
    public PlayerMovementModule  nowUsingMovementModule;

    public List<PlayerFunctionalModule> functionalModuleList;
    public PlayerWeaponModule nowUsingFunctionMudules;

    public List<PlayerPassiveModule> passiveModuleList;

    public bool isInMovingOrder, isShooting;

    public GameObject weaponModule, movementModule, functionalModule, passiveModule;

    private Vector3 lastPos, nowPos;
    public float instantSpeed;

    void Start()
    {
        isInMovingOrder = false;
        isShooting = false;

        InstallModule("movement", "AcclerationEngine");
        InstallModule("weapon", "EnergyPhotosphere");

        this.nowUsingMovementModule = movementModuleList[0];
        this.nowUsingWeaponModule = weaponModuleList[0];
    }

    void Update()
    {
        if (nowUsingMovementModule != null)
        {
            nowUsingMovementModule.Move();
        }

        if (nowUsingWeaponModule != null)
        {
            nowUsingWeaponModule.Shoot();
        }


        nowPos = transform.position;
        instantSpeed = (nowPos - lastPos).magnitude/Time.deltaTime;
        lastPos = nowPos;
    }

    void InstallModule(string moduleType, string moduleName)
    {
        GameObject mod = Instantiate(Resources.Load(moduleName)) as GameObject;

        if(moduleType == "weapon")
        {
            mod.transform.SetParent(weaponModule.transform);
            this.weaponModuleList.Add(mod.GetComponent<PlayerWeaponModule>());
        }
        else if(moduleType == "movement")
        {
            mod.transform.SetParent(movementModule.transform);
            this.movementModuleList.Add(mod.GetComponent<PlayerMovementModule>());
        }
        else if (moduleType == "functional")
        {
            mod.transform.SetParent(functionalModule.transform);
            this.functionalModuleList.Add(mod.GetComponent<PlayerFunctionalModule>());
        }
        else if (moduleType == "passive")
        {
            mod.transform.SetParent(passiveModule.transform);
            this.passiveModuleList.Add(mod.GetComponent<PlayerPassiveModule>());
        }

        mod.transform.localPosition = Vector3.zero;
        mod.transform.localEulerAngles = Vector3.zero;
    }
}