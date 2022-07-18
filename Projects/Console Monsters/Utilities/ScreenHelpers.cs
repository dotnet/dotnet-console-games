namespace Console_Monsters.Utilities;

public static class ScreenHelpers
{
	public static StringBuilder Center(string[] render, (int Height, int Width) bufferSize, (int J, int I)? renderCenterPoint = null)
	{
		int renderWidth = render.Max(line => line is null ? 0 : line.Length);
		renderCenterPoint ??= (render.Length / 2, renderWidth / 2);
		(int J, int I) offset = ((bufferSize.Height - render.Length) / 2, (bufferSize.Width - renderWidth) / 2);
		offset = (offset.J + renderCenterPoint.Value.J - render.Length / 2, offset.I + renderCenterPoint.Value.I - renderWidth / 2);
		StringBuilder sb = new(bufferSize.Height * bufferSize.Width);
		for (int j = 0; j < bufferSize.Height; j++)
		{
			for (int i = 0; i < bufferSize.Width; i++)
			{
				var (dj, di) = (j - offset.J, i - offset.I);
				if (dj >= 0 && dj < render.Length && di >= 0 && render[dj] is not null && di < render[dj].Length)
				{
					char c = render[dj][di];
					sb.Append(c);
				}
				else
				{
					sb.Append(' ');
				}
			}
			sb.AppendLine();
		}
		return sb;
	}
}
