using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonPicker : MonoBehaviour
{
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldButtomY = -6f;
    public float energyShieldRadius = 1.5f;
    public List<GameObject> basketList;
    private bool canRemoveShield = true; // Cooldown flag for shield removal

    void Start()
    {
        basketList = new List<GameObject>();
        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tBasketGo = Instantiate<GameObject>(energyShieldPrefab);
            tBasketGo.transform.position = new Vector3(0, energyShieldButtomY, 0);
            tBasketGo.transform.localScale = new Vector3(1 * i, 1 * i, 1 * i);
            basketList.Add(tBasketGo);
        }
    }

    void Update()
    {
    }

    public void DragonEggDestroyed()
    {
        if (!canRemoveShield) return; // Skip if cooldown is active

        StartCoroutine(RemoveShieldWithCooldown());
    }

    private IEnumerator RemoveShieldWithCooldown()
    {
        canRemoveShield = false; // Disable further shield removal temporarily

        Debug.Log("Egg Destroyed");
        int basketIndex = basketList.Count - 1;
        Debug.Log(basketList.Count);
        if (basketIndex >= 0)
        {
            GameObject tBasketGo = basketList[basketIndex];
            basketList.RemoveAt(basketIndex);
            Destroy(tBasketGo);

            if (basketList.Count == 0)
            {
                SceneManager.LoadScene("0Scene");
            }
        }

        yield return new WaitForSeconds(2f); // Cooldown duration
        canRemoveShield = true; // Re-enable shield removal
    }
}
