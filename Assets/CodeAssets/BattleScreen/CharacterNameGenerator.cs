using CNG;

public class CharacterNameGenerator
{
    public static CharacterName GenerateCharacterName()
    {
        var character = NameGenerator.GetCharacter();

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