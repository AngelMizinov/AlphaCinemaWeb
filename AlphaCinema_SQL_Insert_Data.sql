use AlphaCinemaDB

--First delete all data in tables
--and set Id to start from 1
DELETE FROM Cities
DBCC CHECKIDENT ('Cities', RESEED, 0)

DELETE FROM Genres
DBCC CHECKIDENT ('Genres', RESEED, 0)

DELETE FROM Movies
DBCC CHECKIDENT ('Movies', RESEED, 0)

DELETE FROM OpenHours
DBCC CHECKIDENT ('OpenHours', RESEED, 0)

DELETE FROM Projections
DBCC CHECKIDENT ('Projections', RESEED, 0)

DELETE FROM MoviesGenres
DBCC CHECKIDENT ('MoviesGenres', RESEED, 0)

DELETE FROM WatchedMovies
DBCC CHECKIDENT ('WatchedMovies', RESEED, 0)

--/////////////////////////////////////////////////////////////////////////

--CITIES
INSERT INTO 
Cities([IsDeleted],[Name]) 
VALUES 
('false','Sofia'),
('false','Plovdiv'),
('false','Varna'),
('false','Burgas')
--('false','Veliko Turnovo'),
--('false','Vidin'),
--('false','Blagoevgrad')
SELECT * FROM Cities


--GENRES
INSERT INTO Genres([IsDeleted],[Name]) 
VALUES 
('false','Comedy'),
('false','Drama'),
('false','Action'),
('false','Adventure'),
('false','Sci-fi'),
('false','Fantastic'),
('false','Crime'),
('false','Romance'),
('false','Horror')
SELECT * FROM Genres


--MOVIES
INSERT INTO Movies([IsDeleted],[Name],[Description],[ReleaseYear],[Duration])
VALUES
('false','The Godfather','The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.',1972,175),
('false','The Dark Knight','When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.',2008,162),
('false','Forrest Gump','The presidencies of Kennedy and Johnson, Vietnam, Watergate, and other history unfold through the perspective of an Alabama man with an IQ of 75.',1994,142),
('false','Star Wars: Episode V - The Empire Strikes Back','After the rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued by Darth Vader.',1980,124),
('false','Venom','When Eddie Brock acquires the powers of a symbiote, he will have to release his alter-ego "Venom" to save his life.',2018,112),
('false','Jurassic World: Fallen Kingdom','When the islands dormant volcano begins roaring to life, Owen and Claire mount a campaign to rescue the remaining dinosaurs from this extinction-level event.',2018,128),
('false','It','In the summer of 1989, a group of bullied kids band together to destroy a shape-shifting monster, which disguises itself as a clown and preys on the children of Derry, their small Maine town.',2017,135)
SELECT * FROM Movies


--OPENHOURS
INSERT INTO OpenHours([IsDeleted],[Hours],[Minutes])
VALUES
('false',12,30),
('false',13,00),
('false',13,45),
('false',15,30),
('false',16,00),
('false',17,45),
('false',19,30),
('false',20,00),
('false',20,30),
('false',21,00),
('false',22,30),
('false',23,30)
SELECT * FROM OpenHours


--PROJECTIONS
INSERT INTO Projections([IsDeleted],[MovieId],[CityId],[OpenHourId],[Day],[Seats])
VALUES
('false',1,1,1,0,2),
('false',2,3,1,0,2),
('false',3,2,2,1,2),
('false',3,1,3,1,2),
('false',4,2,3,2,2),
('false',5,3,4,2,2),
('false',5,1,6,3,2),
('false',6,2,8,3,2),
('false',7,1,7,4,2),
('false',6,3,9,4,2),
('false',5,2,9,5,2),
('false',4,1,10,5,2),
('false',4,2,10,6,2),
('false',3,3,12,6,2),
('false',2,2,11,1,2),
('false',3,1,11,2,2)
SELECT * FROM Projections

--MOVIEGENRES
INSERT INTO MoviesGenres([IsDeleted],[MovieId],[GenreId])
VALUES
('false',1,2),
('false',1,7),

('false',2,3),
('false',2,2),
('false',2,7),

('false',3,2),
('false',3,8),

('false',4,3),
('false',4,4),
('false',4,5),

('false',5,3),
('false',5,5),

('false',6,3),
('false',6,4),
('false',6,5),

('false',7,2),
('false',7,9)
SELECT * FROM MoviesGenres
