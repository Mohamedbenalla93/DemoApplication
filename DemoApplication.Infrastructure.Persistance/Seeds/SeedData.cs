using DemoAppliaction.Core.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Core.Domain.Entities;
using AutoMapper.Execution;
using MediatR;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using static System.Collections.Specialized.BitVector32;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;

namespace DemoApplication.Infrastructure.Persistance.Seeds
{
    public static class SeedData
    {
        private static IUnitOfWork UnitOfWork { get; set; }

        public static async Task SeedAsync(this IServiceProvider serviceProvider)
        {
            UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await SeedGenres();
            await SeedAuthors();
            await SeedBooks();
            await SeedBookGenres();

        }

        public static async Task SeedGenres()
        {
            if (await UnitOfWork.GenreRepository.AnyAsync())
                return;

            var listOfGenres = new List<string>
            {
                "Fantasy",
                "Adventure",
                "Romance",
                "Contemporary",
                "Dystopian",
                "Mystery",
                "Horror",
                "Thriller",
                "Paranormal",
                "Historical fiction",
                "Science Fiction"
            };
            foreach (string genre in listOfGenres)
            {
                var genreEntity = new Genre() { Name = genre };
                await UnitOfWork.GenreRepository.AddAsync(genreEntity);
            }

            await UnitOfWork.CompleteAsync();

        }

        public static async Task SeedAuthors()
        {
            if (await UnitOfWork.AuthorRepository.AnyAsync())
                return;

            var author1 = new Author();
            author1.FirstName = "Evelyn";
            author1.LastName = " Waugh";

            var author2 = new Author();
            author2.FirstName = "Flannery ";
            author2.LastName = " Connor";

            var author3 = new Author();
            author3.FirstName = " Colin";
            author3.LastName = " Forbes";

            var author4 = new Author();
            author4.FirstName = "John";
            author4.LastName = " Neal";

            await UnitOfWork.AuthorRepository.AddAsync(author1);
            await UnitOfWork.AuthorRepository.AddAsync(author2);
            await UnitOfWork.AuthorRepository.AddAsync(author3);
            await UnitOfWork.AuthorRepository.AddAsync(author4);
            await UnitOfWork.CompleteAsync();

        }

        public static async Task SeedBooks()
        {
            if (await UnitOfWork.BookRepository.AnyAsync())
                return;


            var titles = new List<string>()
            {
                "An Acceptable Time",
                "Antic Hay",
                "Arms and the Man",
                "As I Lay Dying",
                "Beautiful World", "Where Are You",
                "Behold the Man",
                "Beneath the Bleeding",
                "Beyond the Mexique Bay",
                "Blithe Spirit",
                "Blood's a Rover",
                "Blue Remembered Earth",
                "Blue Remembered Hills",
                "Bonjour Tristesse",
                "Brandy of the Damned",
                "Bury My Heart at Wounded Knee",
                "Butter In a Lordly Dish",
                "By Grand Central Station I Sat Down and Wept",
                "Cabbages and Kings",
                "Captains Courageous",
                "Carrion Comfort",
                "A Catskill Eagle",
                "The Children of Men",
                "Clouds of Witness",
                "A Confederacy of Dunces",
                "Consider Phlebas",
                "Consider the Lilies",
                "Cover Her Face",
                "The Cricket on the Hearth",
                "The Curious Incident of the Dog in the Night-Time",
                "The Daffodil Sky",
                "Dance Dance Dance",
                "A Darkling Plain",
                "Darkness Visible",
                "Death Be Not Proud",
                "The Doors of Perception",
                "Down to a Sunless Sea",
                "Drive Your Plow Over the Bones of the Dead",
                "Dulce et Decorum est",
                "Dying of the Light",
                "East of Eden",
                "Ego Dominus Tuus",
                "Endless Night",
                "Everything is Illuminated",
                "Except the Lord",
                "Eyeless in Gaza",
                "An Evil Cradling",
                "Fair Stood the Wind for France",
                "Fame Is the Spur",
                "A Fanatic Heart",
                "The Far - Distant Oxus",
                "A Farewell to Arms",
                "Far From the Madding Crowd",
                "Fear and Trembling",
                "For a Breath I Tarry",
                "For Whom the Bell Tolls",
                "Frequent Hearses",
                "From Here to Eternity",
                "The Getting of Wisdom",
                "A Glass of Blessings",
                "The Glory and the Dream",
                "The Gods Themselves",
                "The Golden Apples of the Sun",
                "The Golden Bowl",
                "Gone with the Wind",
                "The Grapes of Wrath",
                "Great Work of Time",
                "The Green Bay Tree",
                "A Handful of Dust",
                "Have His Carcase",
                "The Heart Is a Lonely Hunter",
                "The Heart Is Deceitful Above All Things",
                "His Dark Materials",
                "Horseman, Pass By",
                "The House of Mirth",
                "How Sleep the Brave",
                "Human Voices",
                "I Know Why the Caged Bird Sings",
                "I Sing the Body Electric!",
                "I Will Fear No Evil",
                "If I Forget Thee Jerusalem",
                "If Not Now, When ?",
                "In a Dry Season",
                "In a Glass Darkly",
                "In Death Ground",
                "In Dubious Battle",
                "An Instant in the Wind",
                "It's a Battlefield",
                "Jacob Have I Loved",
                "O Jerusalem!",
                "Jesting Pilate",
                "The Kingdom of God Is Within You",
                "The Last Enemy",
                "The Last Temptation",
                "The Lathe of Heaven",
                "Let Us Now Praise Famous Men",
                "Lilies of the Field",
                "This Lime Tree Bower",
                "The Line of Beauty",
                "The Little Foxes",
                "Little Hands Clapping",
                "A Little Learning",
                "Look Homeward, Angel",
                "Look to Windward",
                "The Magic Mountain",
                "The Man Within",
                "Many Waters",
                "A Many - Splendoured Thing",
                "The Mermaids Singing",
                "The Millstone                       ",
                "The Mirror Crack'd from Side to Side",
                "Moab Is My Washpot                  ",
                "The Monkey's Raincoat               ",
                "Monstrous Regiment                  ",
                "A Monstrous Regiment of Women       ",
                "The Moon by Night                   ",
                "Mother Night                        ",
                "The Moving Finger                   ",
                "The Moving Toyshop                  ",
                "Mr Standfast                        ",
                "Nectar in a Sieve                   ",
                "The Needle's Eye                    ",
                "Nine Coaches Waiting                ",
                "No Country for Old Men              ",
                "No Highway                          ",
                "Noli Me Tangere                     ",
                "No Longer at Ease                   ",
                "Not Honour More                     ",
                "Now Sleeps the Crimson Petal        ",
                "Number the Stars                    ",
                "Of Human Bondage                    ",
                "Of Mice and Men                     ",
                "Oh! To be in England  ",
                "'Oh, Whistle, and I'll Come to You, My Lad'",
                "The Other Side of Silence ",
                "Out of Africa             ",
                "The Painted Veil          ",
                "Pale Kings and Princes    ",
                "The Parliament of Man     ",
                "Paths of Glory            ",
                "A Passage to India        ",
                "O Pioneers!               ",
                "Postern of Fate           ",
                "Precious Bane             ",
                "The Proper Study          ",
                "Quo Vadis                 ",
                "Recalled to Life          ",
                "Recalled to Life          ",
                "Ring of Bright Water      ",
                "The Road Less Traveled    ",
                "A Scanner Darkly          ",
                "Shall not Perish          ",
                "The Skull Beneath the Skin",
                "The Soldier's Art         ",
                "Some Buried Caesar        ",
                "Specimen Days             ",
                "The Stars' Tennis Balls   ",
                "Stranger in a Strange Land",
                "Such, Such Were the Joys  ",
                "A Summer Bird - Cage      ",
                "The Sun Also Rises        ",
                "Surprised by Joy          ",
                "A Swiftly Tilting Planet  ",
                "Taming a Sea Horse        ",
                "Tender Is the Night       ",
                "Terrible Swift Sword      ",
                "That Good Night           ",
                "That Hideous Strength     ",
                "Things Fall Apart         ",
                "This Side of Paradise     ",
                "Those Barren Leaves       ",
                "Thrones, Dominations      ",
                "Tiger! Tiger!(alternative title of The Stars My Destination)",
                "Tiger! Tiger! ",
                "A Time of Gifts",
                "Time of our Darkness ",
                "A Time to Kill ",
                "Time To Murder And Create  ",
                "Tirra Lirra by the River   ",
                "To a God Unknown           ",
                "To Sail Beyond the Sunset  ",
                "To Say Nothing of the Dog  ",
                "To Your Scattered Bodies Go",
                "The Torment of Others      ",
                "Unweaving the Rainbow",
                "Vanity Fair   ",
                "Vile Bodies  ",
                "The Violent Bear It Away",
                "The Vorpal Blade  ",
                "Waiting for the Barbarians ",
                "Wandering Recollections of a Somewhat Busy Life",
                "The Waste Land",
                "The Way of All Flesh",
                "The Way Through the Woods ",
                "The Wealth of Nations     ",
                "What's Become of Waring   ",
                "When the Green Woods Laugh",
                "Where Angels Fear to Tread",
                "The Widening Gyre ",
                "Wildfire at Midnight",
                "The Wind's Twelve Quarters",
                "The Wings of the Dove",
                "The Wives of Bath",
                "The World, the Flesh and the Devil",
                "The Yellow Meads of Asphodel "
            };

            var books = new List<Book>();
            foreach (string title in titles)
            {
                var book = new Book();
                var authors = await UnitOfWork.AuthorRepository.GetAllAsync();
                var Random = System.Random.Shared.Next(0, 3);
                book.AuthorId = authors.ToList().ElementAt(Random).Id;
                book.Title = title.TrimEnd();

                var randomTest = new Random();

                TimeSpan timeSpan = DateTime.Now - Convert.ToDateTime("05/05/2000");
                TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                DateTime newDate = Convert.ToDateTime("05/05/2000") + newSpan;

                book.PublishedAt = newDate;
                book.NumberOfPages = randomTest.Next(100, 450);

                books.Add(book);
            }

            await UnitOfWork.BookRepository.AddRangeAsync(books);
            await UnitOfWork.CompleteAsync();
        }

        public static async Task SeedBookGenres()
        {
            if (await UnitOfWork.BookGenresRepository.AnyAsync())
                return;

            var _books = await UnitOfWork.BookRepository.GetAllAsync();
            var _genres = await UnitOfWork.GenreRepository.GetAllAsync();
            var randomTest = new Random();
            var booksGenres = new List<BookGenres>();
            foreach (var book in _books)
            {
                Guid? previousGenreId = null;

                for (int i = 0; i < 3; i++)
                {
                    var index = randomTest.Next(0, _genres.Count - 1);
                    var genre = _genres.ElementAt(index);
                    while (genre.Id == previousGenreId)
                    {
                        index = randomTest.Next(0, _genres.Count - 1);
                        genre = _genres.ElementAt(index);
                    }

                    previousGenreId = genre.Id;
                    var bookgenre = new BookGenres()
                    {
                        BookId = book.Id,
                        GenreId = genre.Id
                    };
                    booksGenres.Add(bookgenre);
                }
            }

            await UnitOfWork.BookGenresRepository.AddRangeAsync(booksGenres);
            await UnitOfWork.CompleteAsync();
        }
    }
}
