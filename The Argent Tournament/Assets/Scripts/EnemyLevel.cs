namespace Assets.Scripts
{
    public class EnemyLevel
    {
        public int MaxElement { get; private set; }
        public int MinElement { get; private set; }

        public EnemyLevel(int min, int max)
        {
            MaxElement = max;
            MinElement = min;
        }
    }
}
