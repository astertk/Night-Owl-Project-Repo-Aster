Project Inception Worksheet
=====================================

## Summary of Our Approach to Software Development
    Following the AGILE and SCRUM processes, we have chosen to create a web app that creates
    a campaign for people who enjoy Dungeons and Dragons and/or enjoy Table Top Role Playing Games (TTRPGs) in general.
    We will be using ChatGPT to generate a backstory and quests that will guide them through their adventures and 
    DALLE to generate images of the character that they describe to be and the map that they describe to the AI.

## Initial Vision Discussion with Stakeholders
    Summarize what was discussed.  What do they want? Include:

### Description of Clients/Users

### List of Stakeholders and their Positions (if applicable)
    - Users who enjoy TTRPGs as the project is mainly catered to them
    - Developers as they create and deploy the project
    - Professor as they also test/approve the application

## Initial Requirements Elaboration and Elicitation
    See Requirements_template for more

### Elicitation Questions
    1. Is there any competition or other apps that currently does this?
    2. Will I be able to edit the content that's created? 
    3. How many characters, maps, etc. am I able to create?

### Elicitation Interviews
    As we discussed the questions above, we learned that there is an app that currently does this
    but they have an in-house randomization algorithm for creating text models, whereas we are 
    utilizing OpenAI. 
    
    As far as we have discussed with our clients, they would like for the web app to 
    be able to edit the content that was created on it as well. 

    As far as we have discussed with our clients, users will be able to create 3 or so of each component 
    to create the campaign for free. For example, the user can create 3 characters, 3 maps, 
    3 backstories, and 3 quests all for free. It may cost some money to create more after all free 
    creations have been used up. 

### Other Elicitation Activities?
    As needed

## List of Needs and Features
Our users will primarily be Game Masters for a DND/TTRPG campaign who want to create a homebrew (custom content) game without the extensive planning that typically takes. When planning a homebrew campaign, typically the GM will be constructing an entire world. This can take days or weeks to prepare, and it is difficult to have the time for. Our goal is to create a tool that can do a lot of that for the GM. The GM can have a general concept of what they want their campaign to be about and get what they need to bring their world to life.

Our application will take input from the user about what they want to do for a DND/TTRPG campaign. They will describe broad concepts of what they want the campaign to be like, as well as quests, maps, encounters, and NPCs. The application will then return AI generated content for the user to put in their campaign. This will include images for maps and characters and descriptions of quests, encounters, characters, and likely more. The user can create an account to store these assets and download them to their device. This will give users the ability to create a campaign without needing to spend as much time as the planning often requires.

To do this, we will first need to be able to use the OpenAI API. We will need to make requests using the user's input and then display the response.

After connecting the API, we will then need to give the user the ability to create an account, save their assets, and download them.

Another primary objective would be editing. The extent of editing would vary depending on the type of resource. Quest descriptions might only be a plain text file, so we could provide a way to edit the text, but that would be the only option. For maps, there are many more possible editing options. The most obvious is the ability to apply and remove a grid.

Another feature would be the ability to delete resources. Users would need to be able to remove resources from their account.

## Initial Modeling

### Use Case Diagrams
    Diagrams

### Sequence Diagrams

### Other Modeling
    Diagrams, UI wireframes, page flows, ...

## Identify Non-Functional Requirements
    1. Each page must load within 1-3 seconds 
    2. The project should look simple enough that anyone of all age ranges is able to use the web page
    3. The project's database should be able to store the user's data from a previous session

## Identify Functional Requirements (In User Story Format)

E: Epic  
U: User Story  
T: Task  

7. [E] 
    1. [U]
        a. [T]
        b. [T]
    2. [U]
        a. [T]

## Initial Architecture Envisioning
    See mvc_architecture.png

## Agile Data Modeling
    Diagrams, SQL modeling (dbdiagram.io), UML diagrams

## Timeline and Release Plan

    Milestone 2: January 24, 2023
    Milestone 3: January 30, 2023
    Milestone 4: February 6, 2023
    Sprint 1: Feb 10, 2023
    Sprint 2: March 6, 2023
    Sprint 3: March 20, 2023

    Meetings with Professor: Tuesdays @ 11:00 a.m.
    SCRUM Team Meetings: Thursdays or Fridays @ 12:00 p.m.