--CREATE DATABASE [CampaignCreator];



CREATE TABLE [World] 
(
    [ID]            INT         PRIMARY KEY     IDENTITY(1, 1),
    [Name]          NVARCHAR(250)   NOT NULL,
    [CreationDate]  DATE    NOT NULL,
    [UserID]        NVARCHAR(450)         NOT NULL,
);

CREATE TABLE [Backstory]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [Name]          NVARCHAR(450)  NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(4000)   NOT NULL,
);

CREATE TABLE [Quests]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [Name]          NVARCHAR(450)  NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(4000)   NOT NULL
);

CREATE TABLE [Characters]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [Name]          NVARCHAR(450)  NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(4000)   NOT NULL,
    [PictureData]   VARBINARY(max)
);

CREATE TABLE [Maps]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [Name]          NVARCHAR(450)  NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(4000)   NOT NULL,
    [PictureData]   VARBINARY(max)
);

CREATE TABLE [Items]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [Name]          NVARCHAR(450)  NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(4000)   NOT NULL,
    [PictureData]   VARBINARY(max)
)

CREATE TABLE [Songs]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [InstrumentID]  INT,
    [RateID]        INT,
    [Name]          NVARCHAR(450)  NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(4000)   NOT NULL,
    [PictureData]   VARBINARY(max)
);

CREATE TABLE [Encounter]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)   NOT NULL,
    [Biome]         NVARCHAR(450)   NOT NULL,
    [Type]          NVARCHAR(450)   NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(max)   NOT NULL,

)

CREATE TABLE [Material]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [Type]          NVARCHAR(40)    NOT NULL, 
    [Name]          NVARCHAR(450)  NOT NULL,
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(max)   NOT NULL,
    [FileName]      NVARCHAR(100) ,
    [PictureData]   VARBINARY(max)
);



--ALTER TABLE [World]     ADD CONSTRAINT [World_Fk_User]              FOREIGN KEY ([UserID])      REFERENCES [User] ([ID])        ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Material]  ADD CONSTRAINT [Material_Fk_World]          FOREIGN KEY ([WorldID])     REFERENCES [World] ([ID])       ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Material]  ADD CONSTRAINT [Material_Fk_Template]       FOREIGN KEY ([TemplateID])  REFERENCES [Template] ([ID])    ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Material]  ADD CONSTRAINT [Material_Fk_TemplateType]   FOREIGN KEY ([Type])        REFERENCES [Template] ([Type])  ON DELETE NO ACTION ON UPDATE NO ACTION;