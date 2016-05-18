
public class DiscardStack : Stack
{
    public static DiscardStack singleton = null;
    protected void Awake()
    {
        
        if (singleton == null)
            singleton = this;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        m_NumCards = m_Cards.Count;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        m_NumCards = m_Cards.Count;
    }
}