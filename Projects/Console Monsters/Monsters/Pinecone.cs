namespace Console_Monsters.Monsters;

public class Pinecone : MonsterBase
{
	public Pinecone()
	{
		Name = "Pinecone";
	}

	public override string[] Sprite => (
		@" ┌┌┐┌┐┐ " + "\n" +
		@"[ ^╶╴^ ]" + "\n" +
		@"└└┘└┘└┘┘" + "\n" +
		@" └└┘└┘┘ " + "\n" +
		@"  └└┘┘  ").Split('\n');
}
