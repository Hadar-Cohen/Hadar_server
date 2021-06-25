CREATE TABLE Comments_2021 (
    commentId  INT  IDENTITY (1, 1) NOT NULL,
	currDate  VARCHAR (20) NOT NULL,
    userId    SMALLINT NOT NULL,
    seriesId  INT      NOT NULL,
	content NVARCHAR (2000) NOT NULL,
    PRIMARY KEY CLUSTERED (commentId ASC),
    UNIQUE NONCLUSTERED ([userId] ASC, [seriesId] ASC),
    FOREIGN KEY (userId) REFERENCES User_2021 (id));
    
    select * from Comments_2021