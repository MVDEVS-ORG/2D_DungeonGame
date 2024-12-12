namespace MVDEV.DungeonGame.Scripts.PlayerScripts.Interface
{ 
    public interface IPlayer
    {
        float PlayerSpeed { get; }
        int PlayerHealth { get; }
        int ManaPoint { get; }

        void TakeDamage(int damageAmount);
        void UpdateHealthUI(int currentHealth);
        void Heal(int amount);
        PlaceTorch GetPlaceTorch();
    }
}