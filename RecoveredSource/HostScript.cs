public class HostScript
{
	public virtual string pluginName => null;

	public virtual string pluginCreator => null;

	public virtual void onInit()
	{
	}

	public virtual void onDestroy()
	{
	}

	public virtual void onPlayerJoin(MCNPlayer player)
	{
	}

	public virtual void onPlayerSwitchedMachine(MCNPlayer player)
	{
	}

	public virtual void onPlayerLeave(MCNPlayer player)
	{
	}

	public virtual void onChatMessage(MCNPlayer player, string message)
	{
	}

	public virtual void onDeath(MCNPlayer player)
	{
	}
}
