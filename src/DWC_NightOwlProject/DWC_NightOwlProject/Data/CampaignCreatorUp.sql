--CREATE DATABASE [CampaignCreator];



CREATE TABLE [World] 
(
    [ID]            INT         PRIMARY KEY     IDENTITY(1, 1),
    [CreationDate]  DATE    NOT NULL,
    [UserID]        NVARCHAR(450)         NOT NULL,
);

CREATE TABLE [Material]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [UserID]        NVARCHAR(450)         NOT NULL,
    [Type]          NVARCHAR(40)    NOT NULL, 
    [CreationDate]  DATE            NOT NULL,
    [WorldID]       INT             NOT NULL,
    [Prompt]        NVARCHAR(1000)   NOT NULL,
    [Completion]    NVARCHAR(1000)   NOT NULL,
    [TemplateID]    INT             NOT NULL 
);

CREATE TABLE [Template]
(
    [ID]            INT             PRIMARY KEY     IDENTITY(1, 1),
    [CreationDate]  DATE            NOT NULL,
    [Body]          NVARCHAR(250)   NOT NULL,
    [Type]          NVARCHAR(250)   NOT NULL
);

--ALTER TABLE [World]     ADD CONSTRAINT [World_Fk_User]              FOREIGN KEY ([UserID])      REFERENCES [User] ([ID])        ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Material]  ADD CONSTRAINT [Material_Fk_World]          FOREIGN KEY ([WorldID])     REFERENCES [World] ([ID])       ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Material]  ADD CONSTRAINT [Material_Fk_Template]       FOREIGN KEY ([TemplateID])  REFERENCES [Template] ([ID])    ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Material]  ADD CONSTRAINT [Material_Fk_TemplateType]   FOREIGN KEY ([Type])        REFERENCES [Template] ([Type])  ON DELETE NO ACTION ON UPDATE NO ACTION;