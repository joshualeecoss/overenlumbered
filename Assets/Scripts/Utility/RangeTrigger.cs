using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTrigger : MonoBehaviour
{
    public enum WeaponType
    {
        Axe, Chainsaw, Default
    }

    public WeaponType weaponType;
    [SerializeField] private BoxCollider rangeTrigger;
    [SerializeField] private Chainsaw chainsaw;
    [SerializeField] private Axe axe;

    public void Update()
    {
        switch (weaponType)
        {
            case WeaponType.Axe:
                HandleSwitchToAxe();
                break;
            case WeaponType.Chainsaw:
                HandleSwitchToChainsaw();
                break;
            case WeaponType.Default:
                HandleDefault();
                break;
        }
    }

    public void SetState(WeaponType weapon)
    {
        weaponType = weapon;
    }

    private void HandleSwitchToAxe()
    {
        rangeTrigger.enabled = false;
        StartCoroutine(WeaponSwitchWait());
        GameManager.chainsawUnlocked = false;
        GameManager.attackPower = axe.GetAttackPower();
        GameManager.attackSpeed = axe.GetAttackSpeed();
        StartCoroutine(SwitchToDefault());

    }

    private void HandleSwitchToChainsaw()
    {
        rangeTrigger.enabled = false;
        StartCoroutine(WeaponSwitchWait());
        if (!GameManager.chainsawUnlocked)
        {
            GameManager.chainsawUnlocked = true;
        }
        GameManager.attackPower = chainsaw.GetAttackPower();
        GameManager.attackSpeed = chainsaw.GetAttackSpeed();
        StartCoroutine(SwitchToDefault());

    }

    private void HandleDefault()
    {
        return;
    }

    private IEnumerator WeaponSwitchWait()
    {
        yield return new WaitForSeconds(0.5f);

        rangeTrigger.enabled = true;
    }

    private IEnumerator SwitchToDefault()
    {
        yield return new WaitForSeconds(0.5f);
        weaponType = WeaponType.Default;
    }
}
