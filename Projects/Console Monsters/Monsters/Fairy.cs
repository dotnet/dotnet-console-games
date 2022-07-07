﻿namespace Console_Monsters.Monsters;

public class Fairy : MonsterBase
{
	public Fairy()
	{
		Name = "Fairy";
	}

	public override string[] Sprite => (
		@"     ╭╭┬──┬╮╮  " + '\n' +
		@"     │«ⱺ╲╱ⱺ»│  " + '\n' +
		@"     ╯╯╰┬┬╯╰╰  " + '\n' +
		@"      ╭╭╮╭╮╮   " + '\n' +
		@"     ╱╱ ├▼││   " + '\n' +
		@"    ╰╯  ││╰╯   " + '\n' +
		@"      ˏ╱  ╲    " + '\n' +
		@"    ˏ╱  │╲ ╲   " + '\n' +
		@"  ˏ╱  ╱ │ ╲ ╲  " + '\n' +
		@" ╱  ╱ ╱ ├──╲ ╲ " + '\n' +
		@"╰───────╯   ╰─╯").Split('\n');
}
