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
    public class BackstoryTests
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
                new Material {
                    Id = 1,
                    UserId = "683910938473829",
                    Type = "Backstory",
                    Name= "",
                    WorldId= 0,
                    Prompt = "Create a DnD Backstory where everyone has turned into snowmen!",
                    Completion = "The kingdom of Arandor was once a prosperous land ruled by a benevolent queen, but all that changed when a powerful wizard named Zoltar arrived in town. Zoltar was obsessed with the idea of eternal winter, and he used his dark magic to conjure a massive snowstorm that lasted for days.\r\n\r\nThe people of Arandor tried to fight back, but Zoltar's magic was too strong. One by one, they succumbed to the cold, until every living creature in the kingdom had turned into a snowman. The queen was among the last to fall, but before she was fully transformed, she managed to cast a powerful spell that would preserve her memories and consciousness in the form of a magical snowflake.\r\n\r\nCenturies passed, and Arandor became a forgotten kingdom, buried under snow and ice. The snowmen that once were the people of Arandor remained frozen in time, unaware of their former lives and the tragedy that befell them.\r\n\r\nOne day, a group of adventurers stumbled upon Arandor, drawn by rumors of a powerful artifact hidden somewhere in the frozen wasteland. As they made their way through the snow-covered ruins, they encountered the snowmen, who were unable to communicate with them due to their frozen state.\r\n\r\nBut the queen's snowflake, still imbued with her magic, sensed the presence of the adventurers and called out to them. Using her magic, she imbued the adventurers with the power to communicate with the snowmen and tasked them with finding the artifact that could reverse the curse and restore Arandor to its former glory.\r\n\r\nThe adventurers set out on their quest, braving treacherous blizzards and facing off against Zoltar's minions, who were still lurking in the shadows. Along the way, they discovered clues about the artifact's location, and eventually found it in a hidden chamber deep beneath the ice.\r\n\r\nWith the artifact in hand, the adventurers returned to the snowmen and used it to break the curse. The snowmen began to transform back into their former selves, and the adventurers watched in amazement as the ruins of Arandor came back to life.\r\n\r\nThe queen, now free from her snowflake, emerged from her icy prison and thanked the adventurers for their bravery. She rewarded them with magical items and bestowed upon them the title of \"Heroes of Arandor.\" The adventurers basked in their newfound glory, knowing that they had saved a kingdom from eternal winter and restored hope to its people."
                },

                new Material {
                    Id = 2,
                    UserId = "1928283764738198",
                    Type = "Backstory",
                    Name= "",
                    WorldId= 0,
                    Prompt = "Create a DnD Backstory where all the land has become lava",
                    Completion = "The world of Eorath was once a beautiful place, filled with rolling hills, sparkling rivers, and dense forests. But one day, the ground began to rumble, and a massive earthquake shook the land. As the dust settled, the people of Eorath looked out to see that the earth had been transformed. Everywhere they looked, the land had turned into a sea of lava.\\r\\n\\r\\nThe people of Eorath were devastated. Their homes, their crops, and their livestock were all destroyed in the cataclysmic event. Many perished in the flames, and those who survived were left to eke out a meager existence in a world that had been turned upside down.\r\n\r\nAs the years passed, the people of Eorath adapted to their new reality. They built homes and settlements on the few patches of solid ground that remained, using boats and other watercraft to navigate the lava seas. They learned to cultivate crops that could withstand the heat, and they discovered new ways to hunt and fish in the harsh environment.\r\n\r\nBut the people of Eorath were not alone in this new world. The cataclysm had unleashed ancient and terrible creatures from the depths of the earth, drawn to the heat and destruction. Dragons, lava golems, and other monsters roamed the wasteland, hunting the people of Eorath and terrorizing their settlements.\r\n\r\nIt was in this world that a group of adventurers rose up to challenge the monsters that threatened the people of Eorath. They were warriors, wizards, and priests, each with their own unique skills and talents. They banded together, united by a common goal: to find the source of the cataclysm and put an end to it.\r\n\r\nThe adventurers set out across the lava seas, facing off against ferocious beasts and treacherous terrain. Along the way, they discovered clues that led them to the heart of the cataclysm, a great volcano that had erupted in the wake of the earthquake.\r\n\r\nAs they climbed the volcano, they were beset by hordes of monsters, but they pressed on, their determination unwavering. When they reached the summit, they found a dark wizard, who had summoned the earthquake and unleashed the volcano's fury. The wizard laughed as he attacked the adventurers with blasts of lava and fire, but they fought back with all their might.\r\n\r\nIn the end, the adventurers emerged victorious, defeating the wizard and using their combined power to seal the volcano's eruption. The lava seas began to recede, and the ground beneath their feet solidified once more. The people of Eorath rejoiced, celebrating the return of solid ground and the defeat of their monstrous foes.\r\n\r\nThe adventurers, now hailed as heroes, returned to their homes, knowing that they had saved the world of Eorath from certain destruction. They remained ever vigilant, knowing that the threat of another cataclysm loomed on the horizon, but they also knew that they would be ready to face it when it came."
                }
            };

            _mockContext= new Mock<WebAppDbContext>();
            _mockMaterialDbSet = MockHelpers.GetMockDbSet(_materials.AsQueryable());
            _mockContext.Setup(ctx => ctx.Materials).Returns(_mockMaterialDbSet.Object);
            _mockContext.Setup(ctx => ctx.Set<Material>()).Returns(_mockMaterialDbSet.Object);
        }

        [Test]
        public void BackstoryCompletionActuallyHasContentInIt_AndIsNotEmpty()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepo = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = "The world of Eorath was once a beautiful place, filled with rolling hills, sparkling rivers, and dense forests. But one day, the ground began to rumble, and a massive earthquake shook the land. As the dust settled, the people of Eorath looked out to see that the earth had been transformed. Everywhere they looked, the land had turned into a sea of lava.\\r\\n\\r\\nThe people of Eorath were devastated. Their homes, their crops, and their livestock were all destroyed in the cataclysmic event. Many perished in the flames, and those who survived were left to eke out a meager existence in a world that had been turned upside down.\r\n\r\nAs the years passed, the people of Eorath adapted to their new reality. They built homes and settlements on the few patches of solid ground that remained, using boats and other watercraft to navigate the lava seas. They learned to cultivate crops that could withstand the heat, and they discovered new ways to hunt and fish in the harsh environment.\r\n\r\nBut the people of Eorath were not alone in this new world. The cataclysm had unleashed ancient and terrible creatures from the depths of the earth, drawn to the heat and destruction. Dragons, lava golems, and other monsters roamed the wasteland, hunting the people of Eorath and terrorizing their settlements.\r\n\r\nIt was in this world that a group of adventurers rose up to challenge the monsters that threatened the people of Eorath. They were warriors, wizards, and priests, each with their own unique skills and talents. They banded together, united by a common goal: to find the source of the cataclysm and put an end to it.\r\n\r\nThe adventurers set out across the lava seas, facing off against ferocious beasts and treacherous terrain. Along the way, they discovered clues that led them to the heart of the cataclysm, a great volcano that had erupted in the wake of the earthquake.\r\n\r\nAs they climbed the volcano, they were beset by hordes of monsters, but they pressed on, their determination unwavering. When they reached the summit, they found a dark wizard, who had summoned the earthquake and unleashed the volcano's fury. The wizard laughed as he attacked the adventurers with blasts of lava and fire, but they fought back with all their might.\r\n\r\nIn the end, the adventurers emerged victorious, defeating the wizard and using their combined power to seal the volcano's eruption. The lava seas began to recede, and the ground beneath their feet solidified once more. The people of Eorath rejoiced, celebrating the return of solid ground and the defeat of their monstrous foes.\r\n\r\nThe adventurers, now hailed as heroes, returned to their homes, knowing that they had saved the world of Eorath from certain destruction. They remained ever vigilant, knowing that the threat of another cataclysm loomed on the horizon, but they also knew that they would be ready to face it when it came.";

            //Act
            var mockBackstory = materialRepo.GetBackstoryById("1928283764738198");
            string actual = mockBackstory.Completion;

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BackstoryCompletionIsExpectedToBeEmptyButActuallyHasACompletionInItAndShouldFail()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepo = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = " ";

            //Act
            var mockBackstory = materialRepo.GetBackstoryById("1928283764738198");
            string actual = mockBackstory.Completion;

            //Assert
            Assert.That(actual, Does.Not.EqualTo(expected));
        }

        [Test]
        public void BackstoryPromptContainsAPromptAndIsNotEmpty()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepo = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = "Create a DnD Backstory where everyone has turned into snowmen!";

            //Act
            var mockBackstory = materialRepo.GetBackstoryById("683910938473829");
            string actual = mockBackstory.Prompt;

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BackstoryPromptExpectedToBeEmptyButActuallyHasAPromptAndShouldFail()
        {
            //Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepo = new MaterialRepository(_mockContext.Object, mockUserManager);

            string expected = " ";

            //Act
            var mockBackstory = materialRepo.GetBackstoryById("683910938473829");
            string actual = mockBackstory.Prompt;

            //Assert
            Assert.That(actual, Does.Not.EqualTo(expected));
        }
    }
}
