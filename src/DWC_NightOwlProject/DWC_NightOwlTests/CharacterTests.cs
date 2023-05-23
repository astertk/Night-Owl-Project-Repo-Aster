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
using NUnit.Framework;
using DWC_NightOwlProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DWC_NightOwlTests
{
    public class CharacterTests
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
                    Id = 8,
                    UserId = "683910938473829",
                    Type = "Character",
                    Name= "",
                    WorldId= 0,
                    Prompt = "Create a DND character with a random race, age, skin tone, height, and weight. Only include the character. Do not include text or columns. Show the full body and face....",
                    Completion = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-hXKuB5CM3EtOZEwlvXkd1Ns4/user-wqHgZ8LRjgCAnFjGIt4nE4Vg/img-uwtIw4xIdo5l3aBaW65yNAJR.png?st=2023-04-17T12%3A12%3A28Z&se=2023-04-17T14%3A12%3A28Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-04-17T12%3A41%3A51Z&ske=2023-04-18T12%3A41%3A51Z&sks=b&skv=2021-08-06&sig=AzUH3erNmM1qF55HaRxLXNAndlSekGRYYMPGYynT%2B3M%3D",
                    
                },

         
            };

            _mockContext = new Mock<WebAppDbContext>();
            _mockMaterialDbSet = MockHelpers.GetMockDbSet(_materials.AsQueryable());
            _mockContext.Setup(ctx => ctx.Materials).Returns(_mockMaterialDbSet.Object);
            _mockContext.Setup(ctx => ctx.Set<Material>()).Returns(_mockMaterialDbSet.Object);
        }

        //test needs refactoring to work with new db design
        /*[Test]
        public async Task Random_Should_Return_ViewAsync()
        {
            // Arrange
            var mockUserManager = _mockUserManager;
            IMaterialRepository materialRepo = new MaterialRepository(_mockContext.Object, mockUserManager);
            var controller = new CharacterController(materialRepo, null, mockUserManager, null);

            // Act
            var result = await controller.Random();

            // Assert
            Assert.IsNotNull(result);
           // var viewResult = Assert.IsInstanceOf<ViewResult>(result);
            //Assert.AreEqual("", viewResult.ViewName);
        }*/
    }
}
