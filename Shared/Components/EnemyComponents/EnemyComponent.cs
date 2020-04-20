using LudumDare46.Shared.Systems;

namespace LudumDare46.Shared.Components.EnemyComponents
{
    internal class EnemyComponent
    {
        public float HP { get; set; }
        public float Armour { get; set; }
        public float Speed { get; set; }
        public float Damage { get; set; }

        public EnemyComponent()
        {
            HP = 10;
            Armour = 0;
            Speed = 40;
            Damage = 0;
        }
    }
}