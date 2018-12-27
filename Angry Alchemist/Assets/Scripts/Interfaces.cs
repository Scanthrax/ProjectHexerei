using UnityEngine;

public interface IDamageable
{
    void DealDamage();
}

interface ISuckable
{
    void Suck(Vector3 pos, float power);
    void Consume();
}

public class Interfaces
{
    
}
