
public class DiscardStack : Stack
{
    public static DiscardStack singleton = null;
    protected override void Awake()
    {
        base.Awake();
        if (singleton == null)
            singleton = this;
        NumCards = Cards.Count;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        NumCards = Cards.Count;
    }
}