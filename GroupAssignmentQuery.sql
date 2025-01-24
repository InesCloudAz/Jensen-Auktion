CREATE PROCEDURE InsertUser
    @UserName varchar(100),
    @Password varchar(80)
AS
BEGIN
    INSERT INTO Users (UserName, Password)
    VALUES (@UserName, @Password);
END;

go

CREATE PROCEDURE UpdateUser
    @UserID INT,
    @UserName VARCHAR(100),
    @Password VARCHAR(80)
AS
BEGIN
    UPDATE Users
    SET UserName = @UserName,
        Password = @Password
    WHERE UserID = @UserID;
END;

go

CREATE PROCEDURE CreateBid
    @Price INT,
    @AdID INT,
    @UserID INT,
    @NewBidID INT OUTPUT
AS
BEGIN

    INSERT INTO Bid (Price, AdID, UserID)
    VALUES (@Price, @AdID, @UserID);

    SET @NewBidID = SCOPE_IDENTITY();
END;

go

CREATE PROCEDURE DeleteBid
    @BidID INT,
    @IsDeleted BIT OUTPUT
AS
BEGIN
    DELETE FROM Bid
    WHERE BidID = @BidID;

    SET @IsDeleted = CASE 
    WHEN @@ROWCOUNT > 0 THEN 1 ELSE 0 END;
END;

go

CREATE PROCEDURE LoginUser
    @UserName VARCHAR(100),
    @Password VARCHAR(80)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT UserID
    FROM Users
    WHERE UserName = @UserName AND Password = @Password;
END;
go
create procedure CreateAd (@Title varchar (80), @Description varchar (max), @Price int, @StartTime datetime, @EndTime datetime,@UserID int)
as
begin
    insert into Ad(Title, Description, Price, StartTime, EndTime,UserID)
    values (@Title,@Description,@Price,@StartTime,@EndTime,@UserID)

end
go

ALTER PROCEDURE UpdateAd
    @AdID INT,
    @Title VARCHAR(80),
    @Description VARCHAR(MAX),
    @Price INT,
    @StartTime DATETIME,
    @EndTime DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE Ad
    SET 
        Title = @Title,
        Description = @Description,
        Price = @Price,
        StartTime = @StartTime,
        EndTime = @EndTime
    WHERE AdID = @AdID;
END;

go
CREATE PROCEDURE DeleteAd
    @AdID INT
AS
BEGIN
    DELETE FROM Ad
    WHERE AdID = @AdID;
END;

ALTER PROCEDURE GetAllAds
AS
BEGIN
    SELECT 
        a.AdID, 
        a.Title, 
        a.Description, 
        a.Price, 
        a.StartTime, 
        a.EndTime, 
        b.BidId, 
        b.Price,
		b.UserID
    FROM 
        Ad a
    LEFT JOIN 
        Bid b ON a.AdID = b.AdID
    WHERE 
        b.Price = (
            SELECT MAX(Price)
            FROM Bid
            WHERE AdID = a.AdID
        );
END


Create Procedure GetAdById
    @AdID INT
As
Begin
    Select 
        AdID,
        Title,
        Description,
        Price,
        StartTime,
        EndTime,
        UserID
    From 
        Ad
    Where 
        AdID = @AdID;
End;

Create Procedure GetClosedAdById
    @AdID INT
As
Begin
    SELECT 
        a.AdID, 
        a.Title, 
        a.Description, 
        a.Price, 
        a.StartTime, 
        a.EndTime, 
        b.BidId, 
        b.Price,
		b.UserID
    FROM 
        Ad a
    LEFT JOIN 
        Bid b ON a.AdID = b.AdID
    WHERE 
        b.Price = (
            SELECT MAX(Price)
            FROM Bid
            WHERE AdID = a.AdID
        );
End;

create procedure UpdateAdWithBid( @AdID INT,
    @Title VARCHAR(80),
    @Description VARCHAR(MAX),
    @StartTime DATETIME,
    @EndTime DATETIME,
    @UserID INT)
	as
	begin
	 UPDATE Ad
    SET 
        Title = @Title,
        Description = @Description,
        StartTime = @StartTime,
        EndTime = @EndTime
    WHERE AdID = @AdID;

	end

create procedure Get