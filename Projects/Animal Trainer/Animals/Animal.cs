using System;
using System.Collections.Generic;
namespace Animal_Trainer.Animals;

public abstract class Animal
{
	public string? Name { get; set; }

	public int Level { get; set; }

	public int ExperiencePoints { get; set; }

	public int CurrentHP { get; set; }

	public int MaximumHP { get; set; }

	public int CurrentEnergy { get; set; }

	public int MaximumEnergy { get; set; }

	//public AnimalType Type { get; set; }

	//public string? Description { get; set; }

	//public string? StatusCondition { get; set; }
}
