namespace asd;


    public class ActorsDB
    {
        public int Id { get; set; }
        public HashSet<Movie>? movie { get; set; }

        public string? name { get; set; }

        // public ActorsDB {}
        public void Write()
        {
            foreach (var item in movie)
            {
                Console.WriteLine(item.Name);
            }
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            var other = obj as ActorsDB;
            return other != null && other.name == this.name;
        }
    }
