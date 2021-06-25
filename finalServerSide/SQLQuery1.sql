CREATE TABLE ClubMembers_2021 (
    [userId]    SMALLINT        NOT NULL,
    [seriesId]  INT             NOT NULL,
   
    PRIMARY KEY CLUSTERED (userId,seriesId  ASC),
    FOREIGN KEY ([userId]) REFERENCES [User_2021] ([id]),
    FOREIGN KEY (seriesId) REFERENCES [Series_2021] ([id])
);

select * from ClubMembers_2021

Select distinct S.name,S.id From Preferences_2021 as P inner join User_2021 as U on U.id=P.userId
inner join Series_2021 as S on P.seriesId= S.id WHERE U.id=17