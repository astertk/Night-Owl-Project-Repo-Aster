using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.DAL.Concrete;
using DWC_NightOwlProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Castle.Components.DictionaryAdapter;
using DWC_NightOwlProject.Data;
using static OpenAI.GPT3.ObjectModels.SharedModels.IOpenAiModels;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Policy;
using OpenAI;

namespace DWC_NightOwlTests
{
    public class SongsTests
    {
        private Mock<WebAppDbContext> _mockContext;
        private Mock<DbSet<Song>> _mockSongsDbSet;
        private List<Song> _songs;
        private readonly UserManager<IdentityUser> _mockUserManager;
        WebClient webClient = new WebClient();
        

        [SetUp]
        public void Setup()
        {
            _songs = new List<Song>
            {
                new Song {
                    Id = 1,
                    UserId = "683910938473829",
                    InstrumentId = 0,
                    RateId = 0,
                    Name= "Mystery Paladin",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes."
                         + "The tone of the song is: sad"
                         + ". The song will be used for background"
                         + ". The speed of the song will be fast"
                         + ". Make it one string of notes in this format, with no " +
                         "commas or periods. Make it 32 notes or longer, but not longer than 100 notes. " +
                         "Give the song a clear progression of chorus, verse 1, chorus. Don't include the words chorus or verse 1, just the notes with" +
                         "Don't let the notes go higher than G6, but give the notes plenty of variety. Don't include sharp notes, like D4#. Let it be just notes separated by " +
                         "spaces in quotes (don't forget the ending double quote), e.g: \"C4 F5 Ab4 F5\"",

                    Completion = "  \"C4 Eb4 F4 Ab4 Bb4 B4 F5 G5 F5 Eb5 C5 Ab4 G4 Eb4 D4 C4 Bb3 Ab3 G3 F3 B3 C4 F4 Ab4 Eb4 Bb4 B4 F5 G5 C5 F5 Ab4 G4 Eb4 D4 C4 B3 Ab3 G3 F3 Eb3 D3\"",
                    PictureData = null
                },

                new Song {
                    Id = 2,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "ock Phaah",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          },

                 new Song {
                    Id = 3,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "Cloroah",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          },
                  new Song {
                    Id = 4,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "Clck Phaah",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          },
                   new Song {
                    Id = 5,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "lock Phaoah",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          },
                    new Song {
                    Id = 6,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "Cloc Ph",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          },
                     new Song {
                    Id = 7,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "Clk Pharoa",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          },
                      new Song {
                    Id = 8,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "Croah",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          },
                       new Song {
                    Id = 9,
                    UserId = "1928283764738198",
                    InstrumentId = 3,
                    RateId = 2,
                    Name= "haroah",
                    CreationDate = DateTime.Now,
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "  \"G5 Bb5 Ab5 Eb5 Bb5 C5 Eb5 E5 G5 Bb5 Eb5 D5 G5 Ab5 Bb5 Ab5 C5 D5 Eb5 G5 Bb5 D5 Eb5 F5 G5 C6 Eb5 C5 Bb5 Ab5 G5 C5 Eb5 D5 Eb5 F5 G5 C5 F5 Eb5 D5 C5 Bb5 G5 Ab5 Bb5 Eb5 D5 D5 Eb5 F5 G5 C5 Eb5 F5 Bb5 Eb5 G5 Eb5 C5 D5 Ab5 Bb5 Eb5\"",
                    PictureData = webClient.DownloadData("https://siivagunner.fandom.com/wiki/Deez_Nuts")
                          }
            };

            _mockContext = new Mock<WebAppDbContext>();
            _mockSongsDbSet = MockHelpers.GetMockDbSet(_songs.AsQueryable());
            _mockContext.Setup(ctx => ctx.Songs).Returns(_mockSongsDbSet.Object);
            _mockContext.Setup(ctx => ctx.Set<Song>()).Returns(_mockSongsDbSet.Object);
        }

        [Test]
        public void SongPromptActuallyHasContentInIt_AndIsNotEmpty()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            ISongRepository songRepo = new SongRepository(_mockContext.Object, mockUserManager);

            string expected = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!";

            //Act
            var mockSong = songRepo.GetSongByIdandMaterialId("1928283764738198",2);
            string actual = mockSong.Prompt;

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SongPromptIsExpectedToBeEmptyButActuallyHasAPromptInItAndShouldFail()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            ISongRepository songRepo = new SongRepository(_mockContext.Object, mockUserManager);

            string expected = " ";

            //Act
            var mockSong = songRepo.GetSongByIdandMaterialId("1928283764738198", 2);
            string actual = mockSong.Prompt;

            //Assert
            Assert.That(actual, Does.Not.EqualTo(expected));
        }

        [Test]
        public void SongPictureDataIsEmpty()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            ISongRepository songRepo = new SongRepository(_mockContext.Object, mockUserManager);

            //Act
            var mockSong = songRepo.GetAllSongsById("683910938473829").First();
            var actual = mockSong.PictureData;
            byte [] expected = null;

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SongPictureDataReturnsNotEmpty()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            ISongRepository songRepo = new SongRepository(_mockContext.Object, mockUserManager);

            //Act
            var mockSong = songRepo.GetAllSongsById("1928283764738198").First();
            var actual = mockSong.PictureData;

            //Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void SaveSongDoesNotSaveIfSongCountIsAbove6 ()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            ISongRepository songRepo = new SongRepository(_mockContext.Object, mockUserManager);

            //Act
            var mockSong = new Song();
            
            songRepo.Save(mockSong, "1928283764738198");
            //var mockSong = songRepo.GetAllSongsById("1928283764738198").First();

            var expected = 8;
            var actual = songRepo.GetAllSongsById("1928283764738198").Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }


    }
}
