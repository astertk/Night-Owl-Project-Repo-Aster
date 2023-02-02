# Requirements Workup

## Elicitation

1. Is the goal or outcome well defined?  Does it make sense?
- The overall goal/outcome of this project is well defined. We as a web development team want to
create a web application where users can make a backstory, character model, maps, and quests using AI.
2. What is not clear from the given description?
3. How about scope?  Is it clear what is included and what isn't?
Our scope is well defined. Our application shoudl be able to:
- Create an account
- Input basic world information and generate detailed description
- Input basic quest information and generate detailed description
- Input basic character information and generate detailed description
- Input location information and generate map
- Input character information and generate character image
- Delete unwanted resources
- Download resources to device
- Upload resources from device
- Edit resources
- Delete account
We can organize this list into epics and later on into user stories during development.

4. What do you not understand?
    * Technical domain knowledge
    - Still a bit unsure about the API, we will need to go more in depth with that, possibly create a demo mvc project.
    * Business domain knowledge
    - We should ask a number of people who enjoy D&D and are Dugeam Masters. Do they want more details in our results, or more inspiration?
5. Is there something missing?
6. Get answers to these questions.

## Analysis

Go through all the information gathered during the previous round of elicitation.  

1. For each attribute, term, entity, relationship, activity ... precisely determine its bounds, limitations, types and constraints in both form and function.  Write them down.
- Our web applciation will utilize 2 databases: one for user login and one for application data. For our application database, we have 6 main entities: World, Backstory, Quest, Character, Map, and Template.
2. Do they work together or are there some conflicting requirements, specifications or behaviors?
- These entitys can work together, but that doesn't necesarrily mean that each entity needs each other.
3. Have you discovered if something is missing?  
4. Return to Elicitation activities if unanswered questions remain.


## Design and Modeling
Our first goal is to create a **data model** that will support the initial requirements.

1. Identify all entities;  for each entity, label its attributes; include concrete types
- We have two databases: One for User Login and one for our application data. Our application database has these entities: World, Backstory, Quest, Character, Map, and Template.
2. Identify relationships between entities.  Write them out in English descriptions.
- World has relationships with 4 entities. World has a one-to-many relationship with Character and Map, while it has a one-to-one relationship with Backstory and Quest.
- Template also has relationships with 4 entities. Template has a many-to-many with backstory, Quests, Character, and Map have a one-to-many relationship with Template. 
3. Draw these entities and relationships in an _informal_ Entity-Relation Diagram.
4. If you have questions about something, return to elicitation and analysis before returning here.

## Analysis of the Design
The next step is to determine how well this design meets the requirements _and_ fits into the existing system.

1. Does it support all requirements/features/behaviors?
    * For each requirement, go through the steps to fulfill it.  Can it be done?  Correctly?  Easily?
2. Does it meet all non-functional requirements?
    * May need to look up specifications of systems, components, etc. to evaluate this.

