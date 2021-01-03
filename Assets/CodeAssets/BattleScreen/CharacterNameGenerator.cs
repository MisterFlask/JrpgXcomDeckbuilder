using Assets.CodeAssets.CharacterNames;
using CNG;
using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterNameGenerator
{

    public static string GetRandomFirstName()
    {
        var namesTaken = GameState.Instance.PersistentCharacterRoster.Select(item => item.CharacterFullName);
        var firstName = new List<string>
        {
            CommonFemaleNames.FemaleNames.PickRandomWhere(firstNameCandidate => !namesTaken.Any(nameTaken => nameTaken.Contains(firstNameCandidate))),
            CommonMaleNames.MaleNames.PickRandomWhere(firstNameCandidate => !namesTaken.Any(nameTaken => nameTaken.Contains(firstNameCandidate)))
        }.PickRandom();

        return firstName;
    }

    public static CharacterName GenerateCharacterName()
    {
        var character = NameGenerator.GetCharacter(Origin.SubcategoryType.NORTHERN_AMERICAN);

        return new CharacterName
        {
            FirstName = GetRandomFirstName(),
            LastName = character.FamilyName,
            Nickname = character.Nickname
        };
    }


}


public static class CharacterNameExtensions
{

    public static CharacterName ToCharacterName(this Character character)
    {
        return new CharacterName
        {
            FirstName = character.GivenName,
            LastName = character.FamilyName,
            Nickname = character.Nickname
        };
    }
}

public class CharacterName
{
    public string FirstName;
    public string LastName;
    public string Nickname;
}