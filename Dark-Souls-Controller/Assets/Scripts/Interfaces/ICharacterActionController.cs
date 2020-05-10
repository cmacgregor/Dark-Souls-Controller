namespace Characters
{
    public interface ICharacterActionController
    {
        void HandleInputs();

        void SetInputController(CharacterInputController brain);

        void SetCharacterActions(CharacterActions body);
    }
}
