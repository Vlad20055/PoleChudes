namespace Domain.Interfaces;

public interface ISectorHandler
{
    Task<State> Handle();

    enum State
    {
        Completed_Change,
        Completed_NoChange,
        Incompleted,
    };
}
