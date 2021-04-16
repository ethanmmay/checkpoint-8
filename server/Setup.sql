-- SET FOREIGN_KEY_CHECKS=0;
-- DROP TABLE `ethanbcwfinal`.`profiles`;
-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   picture VARCHAR(255),
--   email VARCHAR(255),
--   PRIMARY KEY (Id)
-- );

-- SET FOREIGN_KEY_CHECKS=0;
-- DROP TABLE `ethanbcwfinal`.`keeps`;
-- CREATE TABLE keeps
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(255) NOT NULL,
--   img VARCHAR(255) NOT NULL,
--   views INT NOT NULL,
--   shares INT NOT NULL,
--   keeps INT NOT NULL,

--   PRIMARY KEY (id),

--   FOREIGN KEY (CreatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );

-- SET FOREIGN_KEY_CHECKS=0;
-- DROP TABLE `ethanbcwfinal`.`vaults`;
-- CREATE TABLE vaults
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(255) NOT NULL,
--   isPrivate TINYINT NOT NULL,
--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );

-- SET FOREIGN_KEY_CHECKS=0;
-- DROP TABLE `ethanbcwfinal`.`vaultkeeps`;
-- CREATE TABLE vaultkeeps
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   vaultId INT NOT NULL,
--   keepId INT NOT NULL,
--   PRIMARY KEY (id)
-- );

-- CLEAN DATA OUT OF TABE WITH TRUNCATE
-- TRUNCATE TABLE restaurants;

-- CREATE TABLE reviews
-- (
-- id INT NOT NULL AUTO_INCREMENT,
-- title VARCHAR(255) NOT NULL,
-- body VARCHAR(255) NOT NULL,
-- published TINYINT NOT NULL,
-- restaurantId INT NOT NULL,
-- rating INT CHECK (rating >= 0 AND rating < 6),
-- ownerId VARCHAR(255) NOT NULL,

-- PRIMARY KEY (id),

--   FOREIGN KEY (ownerId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (restaurantId)
--    REFERENCES restaurants (id)
--    ON DELETE CASCADE
-- );



