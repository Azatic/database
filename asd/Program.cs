using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace asd;

public class Program
{
    public static ConcurrentDictionary<string, string> MoviesID;

    public static Task<ConcurrentDictionary<string, string>> IdFFilms() //ключ-АЙДИ ФИЛЬМА, данные -НАЗВАНИЕ ФИЛЬМ 
    {
        return Task.Run(() =>
        {
            ConcurrentDictionary<string, string>
                films = new ConcurrentDictionary<string, string>(); //хотим вывести этот словарь (key - ID, value -фильм)

            // using (StreamReader readtext = new StreamReader("/Users/azat/Desktop/Projects/ml-latest/MovieCodes_IMDB.tsv"))
            //  {
            IEnumerable<string> filetext = File.ReadLines("/Users/azat/Desktop/Projects/ml-latest/MovieCodes_IMDB.tsv");

            //string line = "";
            //    while ((line = readtext.ReadLine()) != null)
            //  {
            Parallel.ForEach(filetext, line =>
            {
                var lineSpan = line.AsSpan();

                var index = lineSpan.IndexOf('\t');
                var filmId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                var filmTitle = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                var region = lineSpan.Slice(0, index).ToString();
                if (region == "RU" || region == "US" || region == "EN")
                {
                  //  lock (films)
                   // {
                        if (films.ContainsKey(filmId))
                        {
                            films.AddOrUpdate(filmId, filmTitle, (s, s1) => filmTitle);
                           // films[filmId] = filmTitle;
                        }
                        else
                        {
                            films.AddOrUpdate(filmId, filmTitle, (s, s1) => filmTitle);
                            //films.TryAdd(filmId, filmTitle);
                        }
                   // }
                }
            });

            //}
            // }

            return films;
        });
    }


    public static Task<ConcurrentDictionary<string, string>> IdFFName() //ключ-рейтинг ФИЛЬМА, данные-НАЗВАНИЕ ФИЛЬМА
    {
        return Task.Run(() =>
        {
            ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();
            // using (StreamReader readtext = new StreamReader("/Users/azat/Desktop/Projects/ml-latest/Ratings_IMDB.tsv"))
            //{
            IEnumerable<string> filetext = File.ReadLines("/Users/azat/Desktop/Projects/ml-latest/Ratings_IMDB.tsv");
            //string line = readtext.ReadLine();
            //while ((line = readtext.ReadLine()) != null)
            //{
            Parallel.ForEach(filetext, line =>
            {
                var lineSpan = line.AsSpan();

                var index = lineSpan.IndexOf('\t');
                var filmId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                var rating = lineSpan.Slice(0, index).ToString();
                if (dic.ContainsKey(filmId))
                {
                 //   lock (dic)
                  //  {
                  dic.AddOrUpdate(filmId, rating, (s, s1) => rating);
                      //  dic[filmId] = rating;
                  //  }
                }
                else
                {
                   // lock (dic)
                    //{
                    dic.AddOrUpdate(filmId, rating, (s, s1) => rating);
                      //  dic[filmId] = rating;
                    //}
                }
            });

            // }
            //}

            return dic;
        });
        // Dictionary<string, string> dic = new Dictionary<string, string>();
        // using (StreamReader readtext = new StreamReader("/Users/azat/Desktop/Projects/ml-latest/Ratings_IMDB.tsv"))
        // {
        //     string line = readtext.ReadLine();
        //     while ((line = readtext.ReadLine()) != null)
        //     {
        //         string[] words = line.Split('	');
        //         if (dic.ContainsKey(words[0]))
        //         {
        //             dic[words[0]] = words[1];
        //         }
        //         else
        //         {
        //             dic[words[0]] = words[1];
        //         }
        //     }
        // }
        //
        // return dic;
    }

    public static Task<ConcurrentDictionary<string, string>> IdFForTegIdFilms() //ключ-айди фильма для тега, данные - айди фльма 
    {
        return Task.Run(() =>
        {
            ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();
            // using (StreamReader readtext =
            //       new StreamReader("/Users/azat/Desktop/Projects/ml-latest/links_IMDB_MovieLens.csv"))
            // {
            //   string line = "";
            //  while ((line = readtext.ReadLine()) != null)
            // {
            IEnumerable<string> filetext =
                File.ReadLines("/Users/azat/Desktop/Projects/ml-latest/links_IMDB_MovieLens.csv");
            // string[] words = line.Split(',');
            Parallel.ForEach(filetext, line =>
            {
                var lineSpan = line.AsSpan();

                var index = lineSpan.IndexOf(',');
                var filmLensId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf(',');
                var filmIdImdb = lineSpan.Slice(0, index).ToString();
                if (dic.ContainsKey(filmLensId))
                {
                 //   lock (dic)
                  //  {
                  dic.AddOrUpdate(filmLensId, "tt" + filmIdImdb, (s, s1) => "tt" + filmIdImdb);
                       // dic[filmLensId] = "tt" + filmIdImdb;
                   // }
                }
                else
                {
                    //lock (dic)
                    //{
                    dic.AddOrUpdate(filmLensId, "tt" + filmIdImdb, (s, s1) => "tt" + filmIdImdb);
                       // dic[filmLensId] = "tt" + filmIdImdb;
                    //}
                }
            });

            //  }
            // }

            return dic;
        });
    }

    public static Task<ConcurrentDictionary<string, string>> GetTagsCodes() //id тега и сам тег
    {
        return Task.Run(() =>
        {
            ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();
            // using (StreamReader readtext =
            //      new StreamReader("/Users/azat/Desktop/Projects/ml-latest/TagCodes_MovieLens.csv"))
            //{
            // string line = "";
            IEnumerable<string> filetext =
                File.ReadLines("/Users/azat/Desktop/Projects/ml-latest/TagCodes_MovieLens.csv");
            //while ((line = readtext.ReadLine()) != null)
            //{
            Parallel.ForEach(filetext, line =>
            {
                var lineSpan = line.AsSpan();

                int index;
                index = lineSpan.IndexOf(',');
                var codeTag = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                var tagName = lineSpan.ToString();
                string[] words = line.Split(',');
             //   lock (dic)
              //  {
              dic.AddOrUpdate(words[0], words[1], (s, s1) => words[1]);
                   // dic[words[0]] = words[1];
              //  }
            });

            // }
            // }

            return dic;
        });
    }

    public static Task<ConcurrentDictionary<string, List<string>>> GetTagsScore() //сопоставляем айди фильма и тег
    {
        return Task.Run(() =>
        {
            var FilmsId7TagId = IdFForTegIdFilms().Result;
            var tag7Idtag = GetTagsCodes().Result;
            ConcurrentDictionary<string, List<string>> dic = new ConcurrentDictionary<string, List<string>>();
            //using (StreamReader readtext =
            //      new StreamReader("/Users/azat/Desktop/Projects/ml-latest/TagScores_MovieLens.csv"))
            // {
            //   string line = "";
            //  while ((line = readtext.ReadLine()) != null)
            // {
            IEnumerable<string> filetext =
                File.ReadLines("/Users/azat/Desktop/Projects/ml-latest/TagScores_MovieLens.csv");
            Parallel.ForEach(filetext, line =>
            {
                var lineSpan = line.AsSpan();

                var index = lineSpan.IndexOf(',');
                var movieId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf(',');
                var tagId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                var relevance = lineSpan.ToString();
                //lock (dic)
                //{
                    if (MoviesID.ContainsKey(FilmsId7TagId[movieId]) &&
                        float.Parse(relevance, CultureInfo.InvariantCulture.NumberFormat) >
                        0.5f) //существует ли фильм, которому хотим присвоить тег, а вторая на число >0.5
                    {
                        if (dic.ContainsKey(FilmsId7TagId[movieId]))
                        {
                            lock (dic)
                            {
                                dic[FilmsId7TagId[movieId]].Add(tag7Idtag[tagId]);
                            }
                            
                        }
                        else
                        {
                            if (FilmsId7TagId.ContainsKey(movieId) && tag7Idtag.ContainsKey(tagId))
                            {
                                lock (dic)
                                {
                                    dic[FilmsId7TagId[movieId]] = new List<string>();
                                    dic[FilmsId7TagId[movieId]].Add(tag7Idtag[tagId]);
                                }
                               
                            }
                        }
                  //  }
                }
                //   }
                //}
            });
            return dic;
        });
    }

    public static Task<ConcurrentDictionary<string, string>> GetAllActorsNames() //АЙДИ ЧЕЛОВЕКА И ЧЕЛОВЕК
    {
        return Task.Run(() =>
        {
            
            ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();

            var fileText = File.ReadLines("/Users/azat/Desktop/Projects/ml-latest/ActorsDirectorsNames_IMDB.txt");

            Parallel.ForEach(fileText, line =>
            {
                var lineSpan = line.AsSpan();

                var index = lineSpan.IndexOf('\t');
                var personId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                var personName = lineSpan.Slice(0, index).ToString();

               // lock (dic)
                //{
                dic.TryAdd(personId, personName);
                // dic[personId] = personName;
                //}
            });

            return dic;
        });
    }

    public static Task<ConcurrentDictionary<string, List<string>>> GetAllActorsCodes() //айди  филма - актеры(ид)
    {
        return Task.Run(() =>
        {
            var ActorsNames = GetAllActorsNames().Result;
            ConcurrentDictionary<string, List<string>> dic = new ConcurrentDictionary<string, List<string>>();
            var fileText = File.ReadAllLines("/Users/azat/Desktop/Projects/ml-latest/ActorsDirectorsCodes_IMDB.tsv");

            Parallel.ForEach(fileText, (line) =>
            {
                var lineSpan = line.AsSpan();

                int index;
                index = lineSpan.IndexOf('\t');
                var filmId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                var personId = lineSpan.Slice(0, index).ToString();
                lineSpan = lineSpan.Slice(index + 1);

                index = lineSpan.IndexOf('\t');
                var category = lineSpan.Slice(0, index).ToString();

                if (category == "actor" || category == "director") //
                {
                   // lock (dic)
                    //{
                        if (dic.ContainsKey(filmId) && ActorsNames.ContainsKey(personId))
                        {
                            lock (dic)
                            {
                                dic[filmId].Add(ActorsNames[personId]);
                            }
                          
                        }

                        else
                        {
                            if (ActorsNames.ContainsKey(personId))
                            {
                                lock (dic)
                                {
                                    dic[filmId] = new List<string>();
                                    dic[filmId].Add(ActorsNames[personId]);
                                }
                               
                            }
                        }
                  //  }
                }
            });
            return dic;
        });
    }

    public static List<Movie> GetMovies()
    {
        MoviesID = IdFFilms().Result;
        var actorsTask = GetAllActorsCodes();
        var tagsIdTask = GetTagsScore();
        var ratingTask = IdFFName();

        Task.WaitAll(actorsTask, tagsIdTask, ratingTask);

        var Actors = actorsTask.Result;
        var tagsId = tagsIdTask.Result;
        var Rating = ratingTask.Result;

        List<Movie> movies = new List<Movie>();

        foreach (var key in MoviesID.Keys)
        {
            if (MoviesID.ContainsKey(key) && Actors.ContainsKey(key) && tagsId.ContainsKey(key) &&
                Rating.ContainsKey(key))
                movies.Add(new Movie(MoviesID[key],
                    Actors[key].Select(w => new ActorsDB { name = w }).ToHashSet<ActorsDB>(),
                    tagsId[key].Select(w => new TagsDb { name = w }).ToHashSet<TagsDb>(), Rating[key]));
        }

        return movies;
    }

    /*public static Dictionary<string, List<string>> ActorFilm()
    {
        Dictionary<string, List<string>> ActorFilms = GetAllActorsCodes(); //ФИЛЬМ-Актеры
        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
        foreach (var key in ActorFilms.Keys)
        {
            if (!MoviesID.ContainsKey(key))
            {
                continue;
            }

            foreach (var value in ActorFilms[key])
            {
                if (dic.ContainsKey(value))
                {
                    dic[value].Add(MoviesID[key]);
                }
                else
                {
                    dic[value] = new List<string>();
                    dic[value].Add(MoviesID[key]);
                }
            }
        }

        return dic;
    }

    public static Dictionary<string, List<string>> TagsFilms() //АЙДИ ТЕГА - ФИЛЬМ
    {
        Dictionary<string, List<string>> tagsId = GetTagsScore(); //айди фильма и тег
        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
        foreach (var key in MoviesID.Keys)
        {
            if (!tagsId.ContainsKey(key))
            {
                continue;
            }

            foreach (var tag in tagsId[key])
            {
                if (dic.ContainsKey(tag))
                    dic[tag].Add(MoviesID[key]);
                else
                {
                    dic[tag] = new List<string>() { MoviesID[key] };
                }
            }
        }

        return dic;
    }

    public static Dictionary<string, List<string>> TagsFilmsMOV() //АЙДИ ТЕГА - ФИЛЬМ
    {
        Dictionary<string, List<string>> tagsId = GetTagsScore(); //айди фильма и тег
        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
        foreach (var key in MoviesID.Keys)
        {
            if (!tagsId.ContainsKey(key))
            {
                continue;
            }

            foreach (var tag in tagsId[key])
            {
                if (dic.ContainsKey(tag))
                    dic[tag].Add(MoviesID[key]);
                else
                {
                    dic[tag] = new List<string>() { MoviesID[key] };
                }
            }
        }

        return dic;
    }*/


// public class top10
// {
//     public int Id { get; set; }
//     
//   //  public Movie mov { get; set; }
//     public string topfilms { get; set; }
//     
//    // public HashSet<Movie>? movie { get; set; }
// }

    static void Main(string[] args)
    {
     //   // var sw = new Stopwatch();
     //   // sw.Start();
     //    var Movies = GetMovies(); 
     // //   sw.Stop();
     //  //
     //  // Console.WriteLine(sw.ElapsedMilliseconds);
     //    Dictionary<string, Movie> dic = new Dictionary<string, Movie>();
     //    Movies.ForEach(x =>
     //    {
     //        if (dic.ContainsKey(x.Name))
     //        {
     //            dic[x.Name] = x;
     //        }
     //        else
     //        {
     //            dic.Add(x.Name, x);
     //        }
     //    });
     //    ////var Actors = GetAllActorsCodes();
     //    
     //    Dictionary<string, HashSet<Movie>> dic2 = new Dictionary<string, HashSet<Movie>>(); //актер и его фильмы
     //    
     //    Movies.ForEach(x =>
     //    {
     //        foreach (var actor in x.Actors)
     //        {
     //            if (dic2.ContainsKey(actor.name))
     //            {
     //                dic2[actor.name].Add(x);
     //            }
     //            else
     //            {
     //                dic2.Add(actor.name, new HashSet<Movie>() { x });
     //            }
     //        }
     //    });
     //    Console.WriteLine("Hello, World!");
     //    Dictionary<string, HashSet<Movie>> dic3 = new Dictionary<string, HashSet<Movie>>();
     //    // HashSet<top10> topList = new();
     //    Movies.ForEach(x =>
     //    {
     //        foreach (var tag in x.Tags)
     //        {
     //            if (dic3.ContainsKey(tag.name))
     //            {
     //                dic3[tag.name].Add(x);
     //            }
     //            else
     //            {
     //                dic3.Add(tag.name, new HashSet<Movie>() { x });
     //            }
     //        }
     //    });
     //
     //    foreach (var item in dic.Values)
     //    {
     //        var candidate = new HashSet<Movie>();
     //        foreach (var tags in item.Tags)
     //        {
     //            foreach (var film in dic3[tags.name])
     //            {
     //                if (candidate.Contains(film))
     //                {
     //                }
     //                else
     //                {
     //                    candidate.Add(film);
     //                }
     //            }
     //        }
     //    
     //        foreach (var actor in item.Actors)
     //        {
     //            foreach (var film in dic2[actor.name])
     //            {
     //                if (candidate.Contains(film))
     //                {
     //                }
     //                else
     //                {
     //                    candidate.Add(film);
     //                }
     //            }
     //        }
     //    
     //    
     //        var dic4 = new Dictionary<float, HashSet<Movie>>();
     //        foreach (var item2 in candidate)
     //        {
     //            if (item == item2)
     //            {
     //                continue;
     //            }
     //    
     //            var k = item.intersection(item2);
     //            if (dic4.ContainsKey(k))
     //                dic4[k].Add(item2);
     //            else
     //            {
     //                dic4.Add(k, new HashSet<Movie>() { item2 });
     //            }
     //        }
     //    
     //        int t = 0;
     //        //top10 top = new();
     //        // var temp = new string();
     //        // string temp = "";
     //        item.top10 = new HashSet<Movie>();
     //        var semen = dic4.OrderByDescending(x => x.Key);
     //        foreach (var item1 in semen)
     //        {
     //            foreach (var film in item1.Value)
     //            {
     //                // temp += film.Name;
     //                //temp += "||";
     //                item.top10.Add(film);
     //                t++;
     //                if (t == 10)
     //                    break;
     //            }
     //    
     //            if (t == 10)
     //                break;
     //        }
     //    
     //        //top.topfilms = temp;
     //        //item.Top10s = top;
     //        //top.topfilms = item;
     //        //topList.Add(top);
     //    }

        using (ApplicationContext db = new ApplicationContext())
        {
            // создаем два объекта User/
            // User tom = new User { Name = "Tom", Age = 33 };
            // User alice = new User { Name = "Alice", Age = 26 };

            // добавляем их в бд
            // db.Users.Add(tom);
            // db.Users.Add(alice);

            //     db.MovieDBs.AddRange(dic.Values);
            //     db.ActorsDBs.AddRange(dic2.Keys.Select(w => new ActorsDB { name = w }));
            //     db.TagsDbs.AddRange(dic3.Keys.Select(w => new TagsDb { name = w }));
            // //    db.Top10s.AddRange(topList);
            //     
            //     
            //     db.SaveChanges();
            Console.WriteLine("Объекты успешно сохранены");

            // получаем объекты из бд и выводим на консоль
            // var users = db.Users.ToList();
            // Console.WriteLine("Список объектов:");
            // foreach (User u in users)
            // {
            //     Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
            // }
            //по фильму по актеру
            //по тегу
            while (true)
            {
                Console.WriteLine("Вы хотите искать по фильму, актеру или тегу");
                string? s = Console.ReadLine();
                if (s == "Актер")
                {
                    Console.WriteLine("введите имя");
                    string? t = Console.ReadLine();
                    var semen = db.MovieDBs.Include(m => m.Actors)
                        .Where(m => m.Actors.Any(n => n.name.ToLower().Contains(t)));
                    var Actorfilms = db.ActorsDBs.Include(a => a.movie).Where(m => m.name.ToLower().Contains($"{t}"))
                        .ToList();
                    foreach (var item in semen)
                    {
                        Console.WriteLine(item.Name);
                        item.Actors.ToList().ForEach(x => Console.WriteLine(x.name));
                    }
                }

                if (s == "Фильм")
                {
                    Console.WriteLine("введите название фильма");
                    string? v = Console.ReadLine();
                    var findedMovies = db.MovieDBs.Include(a => a.top10).Include(a => a.Tags).Include(a => a.Actors)
                        .Where(m => m.Name.Contains(v)).ToList();
                    foreach (var item in findedMovies)
                    {
                        Console.WriteLine(item.Name);
                        item.writeactor();
                        Console.WriteLine("ТОП 10 ФИЛЬМОВ");
                        item.top10.ToList().ForEach(x => Console.WriteLine(x.Name));
                        Console.WriteLine("ТОП 10 ФИЛЬМОВ!!!");
                        item.Tags.ToList().ForEach(x => Console.WriteLine(x.name));
                        //  item.writetags();
                        Console.WriteLine(item.Rating);
                        Console.WriteLine();
                    }
                }

                if (s == "Тег")
                {
                    Console.WriteLine("введите тег");
                    string? n = Console.ReadLine();
                    var tagsfilms = db.TagsDbs.Include(a => a.movie).Where(m => m.name == n);
                    foreach (var item in tagsfilms)
                    {
                        item.writefilms();
                    }
                }

                // if (s == "топ10")
                // {
                //     Console.WriteLine("введите название фильма");
                //     string? g = Console.ReadLine();
                //     var top = db.Top10s.Include(x => x.top).Where()
                // }


                Console.ReadKey();
                Console.Clear();
            }


            Console.WriteLine("afe");
            Console.ReadKey();
        }
    }
}