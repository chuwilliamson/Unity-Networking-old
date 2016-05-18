
public class DiscardStack : Stack
{


    public static DiscardStack singleton = null;
    protected override void Awake()
    {
        base.Awake();
        if (singleton == null)
            singleton = this;
        numCards = cards.Count;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        numCards = cards.Count;
    }
}