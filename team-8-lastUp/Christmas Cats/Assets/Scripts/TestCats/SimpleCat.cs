using UnityEngine;

public class SimpleCat : Cat
{
    public override void CalculateCat()
    {
        float[] receivedParametres;
        receivedParametres = catUpgrade.CatCalculator(CatUpgrades.CatType.Simple);
        damage = receivedParametres[0];
        speed = receivedParametres[1];
    }

}
