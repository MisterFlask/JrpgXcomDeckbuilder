using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies
{
    /// <summary>
    /// Orange head, yellow head, and purple head
    /// attacks a unit each time someone casts a 1 cost ability
    /// OR each time the last energy is used
    /// OR each time a card is played, period
    /// switches to other head when 50% health is dealt
    /// </summary>
    public class TwoHeadedHorror : AbstractEnemyUnit
    {
        public override List<AbstractIntent> GetNextIntents()
        {
            return new List<AbstractIntent>
            {
                new BuffSelfIntent(this, GetRandomHeadedness()),
                SingleUnitAttackIntent.AttackRandomPc(this, 5, 5)
            };

        }

        private AbstractStatusEffect GetRandomHeadedness()
        {

        }
    }
}