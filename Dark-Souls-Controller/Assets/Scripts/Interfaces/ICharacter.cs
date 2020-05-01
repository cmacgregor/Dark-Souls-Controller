
namespace Characters
{
    interface ICharacter
    {
        CharacterInputController InputController { get; set; }
        CharacterActionController ActionController { get; set; }
        CharacterActions Actions { get; set; }
    }
}
