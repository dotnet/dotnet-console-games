namespace Console_Monsters.Bases;

public abstract class QuestBase
{
	public string? QuestName { get; set; }

	public string? QuestDescription { get; set; }

	public int QuestId { get; set; }

	public List<CharacterBase>? QuestNPCs { get; set; }

	public bool IsStoryLineQuest { get; set; }

	public abstract void TriggerQuestStart();

	public abstract void TriggerQuestComplete();
}
