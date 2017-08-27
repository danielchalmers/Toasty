namespace Toasty.Classes
{
    public struct Credit
    {
        public Credit(
            string name,
            string website,
            string author,
            string license,
            bool isModified = false)
        {
            Name = name;
            Website = website;
            Author = author;
            License = license;
            IsModified = isModified;
        }

        public string Name { get; }
        public string Website { get; }
        public string Author { get; }
        public string License { get; }
        public bool IsModified { get; }
    }
}