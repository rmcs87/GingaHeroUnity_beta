public abstract class GameManagerState
{
    protected GameManager gm;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public GameManagerState(GameManager gm)
    {
        this.gm = gm;
    }
}