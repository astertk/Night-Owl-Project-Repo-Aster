import { validateMaterialData } from '../../DWC_NightOwlProject/wwwroot/js/validation.js';

describe('Materials data validation tests', () => {
    test('undefined material fails validation', () => {
        expect(validateMaterialData(undefined)).toBe(false);
    });

    test('empty material passes validation', () => {
        expect(validateMaterialData([])).toBe(true);
    });

    test('object material fails validation', () => {
        expect(validateMaterialData({})).toBe(false);
    });

    test('materials without type, prompt and completion fail validation', () => {
        // arrange
        const materials = [
            { name: "myBackstory", name: "myQuest" },
            { name: "myCharacter", name: "mappy" }
        ];
        // act & assert
        expect(validateMaterialData(materials)).toBe(false);
    });

    test('materials missing prompt fail validation', () => {
        // arrange
        const materials = [
            { Type: "Backstory", Completion: "long, long ago, in a galaxy far away" },
            { Type: "Quest", Completion: "The quest! The quest for sleep!" },
            { Type: "Character", Completion: "https://oaidalleapiprodscus.blob.core.windows.net/private/org-hXKuB5CM3EtOZEwlvXkd1Ns4/user-wqHgZ8LRjgCAnFjGIt4nE4Vg/img-YGaWEcMj2EoiRKUHQoVhPQje.png?st=2023-03-19T08%3A31%3A37Z&se=2023-03-19T10%3A31%3A37Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-03-19T05%3A57%3A56Z&ske=2023-03-20T05%3A57%3A56Z&sks=b&skv=2021-08-06&sig=8mos1SG49nsjT60auYmesYfaaiVqg89k0ULKJRk0Bqk%3D" }
        ];
        // act & assert
        expect(validateMaterialData(materials)).toBe(false);
    });

    test('materials missing completion fail validation', () => {
        // arrange
        const materials = [
            { type: "Backstory", prompt: "Create me a dark fantasy",  },
            { type: "Character", prompt: "Shrek, but blue!", },
            { type: "Quest", prompt: "Underwater level from Sonic"}
        ];
        // act & assert
        expect(validateMaterialData(materials)).toBe(false);
    });

    test('materials with all properties and correct types pass validation', () => {
        // arrange
        const materials = [
            { type: "Backstory", prompt: "Create me a dark fantasy", Completion: "Starwars in the dark ages" },
        ];
        // act & assert
        expect(validateMaterialData(materials)).toBe(true);
    });

    

});
