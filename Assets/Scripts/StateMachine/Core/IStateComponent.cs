namespace Bono.StateMachine
{
    interface IStateComponent
    {
        void OnStateEnter();

        void OnStateExit();
    }
}