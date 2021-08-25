namespace Toasty.Classes
{
	public struct Credit
	{
		public Credit(
			string name,
			string website,
			string author)
		{
			Name = name;
			Website = website;
			Author = author;
		}

		public string Name { get; }
		public string Website { get; }
		public string Author { get; }
	}
}