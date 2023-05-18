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

namespace DWC_NightOwlTests
{
    public class SongsTests
    {
        private Mock<WebAppDbContext> _mockContext;
        private Mock<DbSet<Song>> _mockSongsDbSet;
        private List<Song> _songs;
        private readonly UserManager<IdentityUser> _mockUserManager;

        [SetUp]
        public void Setup()
        {
            _songs = new List<Song>
            {
                new Song {
                    Id = 1,
                    UserId = "683910938473829",
                    Name= "",
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song slow, and romantic sounding. Fantasy like.",
                    Completion = "Verse 1:\r\n\r\nC4 - F5 - Ab4 - F5 - Bb4 - Eb5 - Bb4 - F5\r\n\r\nC4 - F5 - Ab4 - F5 - Bb4 - Eb5 - Bb4 - C5\r\n\r\nChorus:\r\n\r\nBb4 - Eb5 - Ab4 - F5 - Bb4 - Eb5 - Ab4 - F5 \r\n\r\nBb4 - Eb5 - Ab4 - F5 - Eb4 - Db5 - C5 - Bb4\r\n\r\nVerse 2:\r\n\r\nC4 - F5 - Ab4 - F5 - Bb4 - Eb5 - Bb4 - F5\r\n\r\nC4 - F5 - Ab4 - F5 - Bb4 - Eb5 - Bb4 - C5\r\n\r\nChorus:\r\n\r\nBb4 - Eb5 - Ab4 - F5 - Bb4 - Eb5 - Ab4 - F5 \r\n\r\nBb4 - Eb5 - Ab4 - F5 - Eb4 - Db5 - C5 - Bb4\r\n\r\nBridge:\r\n\r\nEb4 - Bb4 - F5 - C5 - Eb4 - Bb4 - F5 - C5\r\n\r\nEb4 - Bb4 - F5 - C5 - Eb4 - Bb4 - F5 - C5\r\n\r\nOutro:\r\n\r\nC4 - F5 - Ab4 - F5 - Bb4 - Eb5 - Bb4 - F5\r\n\r\nC4 - F5 - Ab4 - F5 - Bb4 - Eb5 - Bb4 - C5"
                    
                },

                new Song {
                    Id = 2,
                    UserId = "1928283764738198",
                    Name= "",
                    WorldId= 0,
                    Prompt = "Write a song that will work with tone.js, meaning only instrumental and only notes. Make the song fast and exciting! This is music during combat and fighting in DnD!",
                    Completion = "Verse 1:\r\n\r\nA Gb A B Bb A\r\nA Gb A B Bb A\r\nE Eb D Db C B\r\nE Eb D Db C B\r\n\r\nChorus 1:\r\nA Gb F D Db A\r\nA Gb F D Db A\r\nE Eb Db B A Gb\r\nE Eb Db B A Gb\r\n\r\nVerse 2:\r\nA Gb A B Bb A\r\nA Gb A B Bb A\r\nE Eb D Db C B\r\nE Eb D Db C B\r\n\r\nChorus 2:\r\nA Gb F D Db A\r\nA Gb F D Db A\r\nE Eb Db B A Gb\r\nE Eb Db B A Gb\r\n\r\nBridge:\r\nF Eb D Bb A Gb\r\nF Eb D Bb A Gb\r\nA Gb F D Db A\r\nA Gb F D Db A\r\n\r\nOutro:\r\nA Gb A B Bb A\r\nA Gb A B Bb A\r\nE Eb D Db C B\r\nE Eb D Db C B"
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

     
    }
}
