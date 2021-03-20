using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PerkRoulette
{
    public static List<SoldierPerk> PossiblePerks => new List<SoldierPerk>
    {
        new PowerfulPerk(),
        new ToughPerk(),
        new KindPerk(),
        new ViciousPerk(),
        new SadisticPerk(), 
        new ResilientPerk()
    };

    public static SoldierPerk GetRandomPerk()
    {
        return PossiblePerks.PickRandom();
    }
}


public class PowerfulPerk : SoldierPerk
{
    public override string Description()
    {
        return "This soldier starts combat with 2 Power.";
    }

    public override string Name()
    {
        return "Strong";
    }
}

public class ToughPerk : SoldierPerk
{
    public override string Description()
    {
        return "This soldier starts combat with 6 Armor.";
    }

    public override string Name()
    {
        return "Tough";
    }
}

public class KindPerk: SoldierPerk
{
    public override string Description()
    {
        return "Each turn in combat, a different ally gains +3 Power until end of turn.";
    }

    public override string Name()
    {
        return "Kind";
    }
}

public class ViciousPerk: SoldierPerk
{
    public override string Description()
    {
        return "When this soldier deals damage, remove 2 Stress.";
    }

    public override string Name()
    {
        return "Vicious";
    }
}
public class SadisticPerk : SoldierPerk
{
    public override string Description()
    {
        return "When an enemy dies, this soldier gains +2 Power.";
    }

    public override string Name()
    {
        return "Sadistic";
    }
}

public class ResilientPerk : SoldierPerk
{
    public override string Description()
    {
        return "This soldier recovers an extra 5 hit points per day.";
    }

    public override string Name()
    {
        return "Resilient";
    }
}


public class ResourcefulPerk : SoldierPerk
{
    public override string Description()
    {
        return "This soldier gains an extra X gold at the end of combat, where X is its level.";
    }

    public override string Name()
    {
        return "Resourceful";
    }
}
public class IngenuityPerk : SoldierPerk
{
    public override string Description()
    {
        return "This soldier gets a 25% discount on card upgrades.";
    }

    public override string Name()
    {
        return "Ingenious";
    }
}

public class SturdyPerk: SoldierPerk
{
    public override string Description()
    {
        return "This soldier gets +20 HP.";
    }

    public override string Name()
    {
        return "Sturdy";
    }
}


public class CaringPerk: SoldierPerk
{
    public override string Description()
    {
        return "After combat, everyone ELSE on the mission recovers five hit points.";
    }

    public override string Name()
    {

        return "Caring";
    }
}


public class TinkerPerk: SoldierPerk
{
    public override string Description()
    {
        return "At the start of combat, a random card in this soldier's deck gets mega-upgraded.";
    }

    public override string Name()
    {
        return "Tinker";
    }
}

public class AgilePerk: SoldierPerk
{
    public override string Description()
    {
        return "The first time each turn this soldier plays a cost 0 card, draw.";
    }

    public override string Name()
    {
        return "Agile";
    }
}


public class CheerfulPerk : SoldierPerk
{
    public override string Description()
    {
        return "Loses an extra 10 stress per day.";
    }

    public override string Name()
    {
        return "Cheerful";
    }
}

public class NeuroticPerk : SoldierPerk
{
    public override string Description()
    {
        throw new System.NotImplementedException();
    }

    public override string Name()
    {
        throw new System.NotImplementedException();
    }
}