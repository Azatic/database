namespace asd;


    public class TagsDb
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public HashSet<Movie>? movie { get; set; }


        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            var other = obj as TagsDb;
            return other != null && other.name == this.name;
        }

        public void writefilms()
        {
            foreach (var item in movie)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
