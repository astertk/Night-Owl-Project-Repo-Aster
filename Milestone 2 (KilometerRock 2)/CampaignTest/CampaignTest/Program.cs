
using OpenAI;
using OpenAI.Images;
using OpenAI.Models;

Console.WriteLine("Hello, World!");



var api = new OpenAIClient(new OpenAIAuthentication("sk-secretkey")); // insert your APIKey from openAI into your secrets.json file


//Uncomment any of the lines to call the api! Must also uncomment respective Console.Writeline to get result. 


//var background = await api.CompletionsEndpoint.CreateCompletionAsync("Create my background story for my Dungeons and Dragons Campaign. Theme: Comical. Mogarr the Loser wants to steal all the music from the realm!",       max_tokens: 200, temperature: 0.8, presencePenalty: 0.1, frequencyPenalty: 0.1, model: Model.Davinci);

//var quest = await api.CompletionsEndpoint.CreateCompletionAsync("Create a quest for my Dungeons and Dragons Campaign: Slay the vicious warewolves at the top of Dreadlock Manor!", max_tokens: 200, temperature: 0.8, presencePenalty: 0.1, frequencyPenalty: 0.1, model: Model.Davinci);

//var characterList = await api.ImagesEndPoint.GenerateImageAsync("Create a character for my Dungeons and Dragons Campaign: High Elf, male, 16, casting a fireball spell", 1, ImageSize.Small);

//var characterOne = characterList.First().ToString(); //generates URL for the Character image, paste into browser to display image

//var mapList = await api.ImagesEndPoint.GenerateImageAsync("Create a Complex Map for my Dungeons and Dragons Campaign: Labyrinth, traps, Minotaur", 1, ImageSize.Medium);

//var mapOne = mapList.First().ToString(); //generates URL for the Map image, paste into browser to display image





Console.WriteLine("Please wait while the AI does it's magic!");
//Console.WriteLine(background);
//Console.WriteLine(quest);
//Console.WriteLine(characterOne);
//Console.WriteLine(mapOne);