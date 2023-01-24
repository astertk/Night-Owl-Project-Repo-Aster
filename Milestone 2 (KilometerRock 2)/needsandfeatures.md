NEEDS AND FEATURES

Our users will primarily be Game Masters for a DND/TTRPG campaign who
want to create a homebrew (custom content) game without the extensive
planning that typically takes. When planning a homebrew campaign, 
typically the GM will be constructing an entire world. This can take
days or weeks to prepare, and it is difficult to have the time for. 
Our goal is to create a tool that can do a lot of that for the GM. The
GM can have a general concept of what they want their campaign to be 
about and get what they need to bring their world to life.

Our application will take input from the user about what they want to do
for a DND/TTRPG campaign. They will describe broad concepts of what they 
want the campaign to be like, as well as quests, maps, encounters, and 
NPCs. The application will then return AI generated content for the user 
to put in their campaign. This will include images for maps and characters 
and descriptions of quests, encounters, characters, and likely more. The 
user can create an account to store these assets and download them to 
their device. This will give users the ability to create a campaign 
without needing to spend as much time as the planning often requires.

To do this, we will first need to be able to use the OpenAI API. We will
need to make requests using the user's input and then display the response.

After connecting the API, we will then need to give the user the ability 
to create an account, save their assets, and download them.

Another primary objective would be editing. The extent of editing would 
vary depending on the type of resource. Quest descriptions might only
be a plain text file, so we could provide a way to edit the text, but
that would be the only option. For maps, there are many more possible
editing options. The most obvious is the ability to apply and remove a
grid.

Another feature would be the ability to delete resources. Users would
need to be able to remove resources from their account.