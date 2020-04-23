
namespace Characters
{
    interface ICharacter
    {
        CharacterInputController InputController { get; set; }
        ActionController ActionController { get; set; }
        CharacterActions Actions { get; set; }
    }
}
