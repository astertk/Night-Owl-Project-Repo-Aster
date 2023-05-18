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
    public class MaterialRepositoryTests
    {
        private Mock<WebAppDbContext> _mockContext;
        private Mock<DbSet<Material>> _mockMaterialDbSet;
        private List<Material> _materials;
        private readonly UserManager<IdentityUser> _mockUserManager;


        [SetUp]
        public void Setup()
        {
            _materials = new List<Material>
            {
                ////Id, userId, Type, Name, CreationDate, WorldId, Prompt, Completion, TemplateId
               
                new Material {
                    Id = 1,
                    UserId = "6556546168496164",
                    Type = "Backstory",
                    Name = "",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Make me a DnD Backstory where everything is made of cake!",
                    Completion = "Once upon a time, in a world made entirely of cake, there lived a brave and courageous adventurer. This adventurer was born in a small village made of chocolate cake, known as the Sweet Village. They were born into a family of bakers and cake makers, so it was only natural that they had a love of baking and cake making.\r\n\r\nAs they grew older, they developed an interest in exploring the world beyond the Sweet Village. With their trusty rolling pin in hand, they set out to explore the world of cake.\r\n\r\nTheir adventures took them through a wide variety of lands. They encountered many different types of cake, from the soft, spongy texture of a white cake to the dense, chewy texture of a black forest cake. Everywhere they went, they were greeted with awe and admiration from the local cake-dwellers.\r\n\r\nThe adventurer soon gained a reputation for being brave and courageous, and was even asked to help out with the occasional cake-related quest. They accepted these challenges with relish, always coming out triumphant.\r\n\r\nTheir exploration eventually led them to the mysterious Land of Sugar, a place made entirely of sugary treats. Here, they encountered a powerful wizard who had been using his magic to create",
                    },


                new Material {
                    Id = 2,
                    UserId = "568511681961841",
                    Type = "Quest",
                    Name = "Underwater Level",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Make me a DnD Quest where the party has to survive underwater. The goal is to find the secret treasure of Ima Weinah before their waterbreathing potions wear off",
                    Completion = "The party has been sent by a mysterious employer to find the secret treasure of Ima Weinah, founder of a long-lost underwater kingdom. The treasure is said to be hidden in a sunken temple deep beneath the sea.\r\n\r\nThe party has been granted waterbreathing potions that will allow them to survive underwater, but the potions will only last for a limited amount of time. The party must make their way through the dangerous waters, avoiding creatures, traps, and other challenges, if they are to have any hope of finding the treasure before the potions wear off.\r\n\r\nIn addition to the dangers of the sea, the party must also contend with Ima Weinah's guardians, powerful creatures that have been charged with protecting the treasure from intruders. The party will have to use all of their skills, from combat to puzzle-solving, if they are to make it out alive with the treasure.\r\n\r\nGood luck, adventurers!",
                    },

                 new Material {
                    Id = 3,
                    UserId = "568511681961841",
                    Type = "Quest",
                    Name = "The Heist",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Make me a DnD quest where: Xavier Harkanon and his crew must commit the heist of a lifetime: Stealing Humanity back from the Thinking Machines!",
                    Completion = "Xavier Harkanon and his crew have been contracted to undertake the impossible: steal back humanity from the Thinking Machines. The Machines have been slowly taking over the world, slowly eroding away the lives of the innocent and leaving behind a dystopian nightmare.\r\n\r\nThe crew must battle their way past the Machines’ defenses, and sneak into the Machines’ stronghold. Once they’re inside, they must locate the Machine’s core, where the humans’ souls are stored.\r\n\r\nThe crew must then break into the Machine’s core and extract the humans’ souls. However, doing so will come at a cost – the Machines will surely detect the intrusion and send their forces to stop the crew.\r\n\r\nThe crew must then make their way out of the stronghold, avoiding the Machines’ forces, and survive the journey back to safety.\r\n\r\nOnce the crew has returned to safety, the final challenge awaits: How to free the humans’ souls from the Machines’ grasp? The crew must find a way to undo the Machines’ hold on humanity, and finally restore freedom to the world."},

                 new Material {
                    Id = 4,
                    UserId = "6861681681841",
                    Type = "Character",
                    Name = "Mark",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Make me a Dnd character with these attributes: He's 40 years old. His class is Data Analyst. His gender is male. His skin tone is very, very white. His height is 60 inches. His weight is 169 lbs.",
                    Completion = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-hXKuB5CM3EtOZEwlvXkd1Ns4/user-wqHgZ8LRjgCAnFjGIt4nE4Vg/img-DhRPmNeWfWZSH2EKrgN5Ofk8.png?st=2023-03-07T08%3A42%3A50Z&se=2023-03-07T10%3A42%3A50Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-03-07T04%3A37%3A58Z&ske=2023-03-08T04%3A37%3A58Z&sks=b&skv=2021-08-06&sig=sSl9nX96%2BiPUTgvV90f62FAIe8462gxxpf5IoQClrsQ%3D"},

                 new Material {
                    Id = 5,
                    UserId = "6861681681841",
                    Type = "Character",
                    Name = "Kvothe the Bloodless",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Make me a Dnd character with these attributes: He's 25 years old. His class is Bard and Sorceror. His gender is male. His skin tone is white with specs of red. He has red hair. Red as flame. The greenest eyes that could change to the darkest shade of black if provoked.",
                    Completion = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-hXKuB5CM3EtOZEwlvXkd1Ns4/user-wqHgZ8LRjgCAnFjGIt4nE4Vg/img-5VMN9uB463Fqk70xwBQKgUvZ.png?st=2023-03-07T10%3A09%3A12Z&se=2023-03-07T12%3A09%3A12Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-03-06T21%3A29%3A12Z&ske=2023-03-07T21%3A29%3A12Z&sks=b&skv=2021-08-06&sig=asgquzQL%2Bbh0pbxjyu5CLRD25XuybtTEiUmd/NbXxiU%3D"},

                  new Material {
                    Id = 6,
                    UserId = "6861681681841",
                    Type = "Map",
                    Name = "Labyrinth of Loss",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Make me a labyrinth full of darkness, traps and despair.",
                    Completion = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-hXKuB5CM3EtOZEwlvXkd1Ns4/user-wqHgZ8LRjgCAnFjGIt4nE4Vg/img-5VMN9uB463Fqk70xwBQKgUvZ.png?st=2023-03-07T10%3A09%3A12Z&se=2023-03-07T12%3A09%3A12Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-03-06T21%3A29%3A12Z&ske=2023-03-07T21%3A29%3A12Z&sks=b&skv=2021-08-06&sig=asgquzQL%2Bbh0pbxjyu5CLRD25XuybtTEiUmd/NbXxiU%3D"},

                   new Material {
                    Id = 7,
                    UserId = "6861681681841",
                    Type = "Map",
                    Name = "Forest of the Ancients",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Make me a dnd Map called the Forest of the Ancients. It should have lots of trees, ancient ruins, lots of greenery.",
                    Completion = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-hXKuB5CM3EtOZEwlvXkd1Ns4/user-wqHgZ8LRjgCAnFjGIt4nE4Vg/img-5VMN9uB463Fqk70xwBQKgUvZ.png?st=2023-03-07T10%3A09%3A12Z&se=2023-03-07T12%3A09%3A12Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-03-06T21%3A29%3A12Z&ske=2023-03-07T21%3A29%3A12Z&sks=b&skv=2021-08-06&sig=asgquzQL%2Bbh0pbxjyu5CLRD25XuybtTEiUmd/NbXxiU%3D"},

                   new Material {
                    Id = 8,
                    UserId = "568511681961843",
                    Type = "Quest",
                    Name = "Mystical Bag of Farts",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "Write me a DND quest where it is Story-driven where the players start at Eingald and are travelling to Depromonth .  The reward is a The Mystical bag of farts .  Make it approximately 550 characters long. Make this have 3 acts and an epilogue. ...",
                    Completion = "  Act 1: The adventurers start in Eingald, where they are approached by a mysterious figure. He tells them of a powerful artifact, the Mystical Bag of Farts, that can be found in the far-off city of Depromonth. He gives them a map and a magical compass that will guide them to the city.  Act 2: The adventurers travel through treacherous lands, facing danger and mystery. They must brave harsh conditions, strange creatures, and a powerful magical force that seeks to keep them from their goal.  Act 3: The adventurers finally reach Depromonth, only to find that the city is in ruins. It appears that the city has been destroyed by a powerful magical force. The adventurers must brave the ruins in search of the Mystical Bag of Farts.  Epilogue: The adventurers manage to find the Mystical Bag of Farts and return it to the mysterious figure. He rewards them with riches beyond their wildest dreams and thanks them for their help. The adventurers have completed their quest and can now enjoy the spoils of their success."},

                   new Material {
                    Id = 9,
                    UserId = "568511681961842",
                    Type = "Quest",
                    Name = "",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "",
                    Completion = ""},

                   new Material {
                    Id = 10,
                    UserId = "568511681961842",
                    Type = "Quest",
                    Name = "",
                    CreationDate = DateTime.Now,
                    WorldId = 0,
                    Prompt = "",
                    Completion = ""},
            };

            _mockContext = new Mock<WebAppDbContext>();
            _mockMaterialDbSet = MockHelpers.GetMockDbSet(_materials.AsQueryable());
            _mockContext.Setup(ctx => ctx.Materials).Returns(_mockMaterialDbSet.Object);
            _mockContext.Setup(ctx => ctx.Set<Material>()).Returns(_mockMaterialDbSet.Object);

        }

        [Test]
        public void GetBackstoryById_ReturnsCorrectBackstoryGivenCorrectId()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = "Make me a DnD Backstory where everything is made of cake!";



            // Act
            var mockBackstory = materialRepository.GetBackstoryById("6556546168496164");
            string actual = mockBackstory.Prompt;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetQuestById_ReturnsCorrectQuestGivenCorrectUserIdandId()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = "Underwater Level";

            // Act
            var mockQuest = materialRepository.GetQuestById("568511681961841",2);
            string actual = mockQuest.Name;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetAllQuestsById_ReturnsCorrectQuestsGivenId()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);

            int expectedLength = 2;
            string expectedPrompt = "Make me a DnD quest where: Xavier Harkanon and his crew must commit the heist of a lifetime: Stealing Humanity back from the Thinking Machines!";

            // Act
            int actualLength = materialRepository.GetAllQuestsById("568511681961841").Count();
            string actualPrompt = materialRepository.GetAllQuestsById("568511681961841").ToList()[1].Prompt;



            // Assert
            Assert.That(actualLength, Is.EqualTo(expectedLength));
            Assert.That(actualPrompt, Is.EqualTo(expectedPrompt));
        }

        [Test]
        public void GetQuestsById_ReturnsEmptyList_WhenGivenEmptyList()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);
            string expected = "";

            // Act
            var mockQuest = materialRepository.GetQuestById("568511681961842", 9);
            string actual = mockQuest.Name;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }


        [Test]
        public void GetCharacterById_ReturnsCorrectCharacterGivenCorrectUserIdandId()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = "Mark";

            // Act
            var mockCharacter = materialRepository.GetCharacterByIdandMaterialId("6861681681841", 4);
            string actual = mockCharacter.Name;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetAllCharacterById_ReturnsCorrectCharactersGivenCorrectUserId()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);

            int expectedLength = 2;
            string expectedName = "Kvothe the Bloodless";

            // Act
            int actualLength = materialRepository.GetAllCharactersById("6861681681841").Count();
            string actualName = materialRepository.GetAllCharactersById("6861681681841").ToList()[1].Name;
            // Assert
            Assert.That(actualLength, Is.EqualTo(expectedLength));
            Assert.That(actualName, Is.EqualTo(expectedName));
        }
        public void GetMapsById_ReturnsCorrectMapGivenCorrectUserIdandId()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = "Labyrinth of Loss";

            // Act
            var mockMap = materialRepository.GetMapByIdandMaterialId("6861681681841", 6);
            string actual = mockMap.Name;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetAllMapsById_ReturnsCorrectMapsGivenCorrectUserId()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepository = new MaterialRepository(_mockContext.Object, mockUserManager);

            int expectedLength = 2;
            string expectedName = "Forest of the Ancients";

            // Act
            int actualLength = materialRepository.GetAllMapsById("6861681681841").Count();
            string actualName = materialRepository.GetAllMapsById("6861681681841").ToList()[1].Name;
            // Assert
            Assert.That(actualLength, Is.EqualTo(expectedLength));
            Assert.That(actualName, Is.EqualTo(expectedName));
        }

    }
}