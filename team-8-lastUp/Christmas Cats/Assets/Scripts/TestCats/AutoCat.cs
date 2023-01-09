using UnityEngine;

public class AutoCat : Cat
{
    public override void CalculateCat()
    {
        float[] receivedParametres;
        receivedParametres = catUpgrade.CatCalculator(CatUpgrades.CatType.AutoSpawned);
        damage = receivedParametres[0];
        speed = receivedParametres[1];
    }
}
