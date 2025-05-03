public interface IGetDamageable 
{
    public void GetDamage(float atk, float power);

    public static float CalculateTrueDamage(float atk, float power, float def)
    {
        return (atk / def) * power;
    }
}
