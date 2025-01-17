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

create procedure GetAllAds
as
begin
	select a.AdID, Title, Description, a.Price, StartTime, EndTime, b.Price as BidPrice from Ad a
	inner join Bid b on a.AdID = b.AdID
end

select * from Ad