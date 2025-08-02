-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: product_listing
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `country`
--

DROP TABLE IF EXISTS `country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `country` (
  `Code` varchar(50) NOT NULL,
  `Id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=95 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `country`
--

LOCK TABLES `country` WRITE;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` VALUES ('Algeria',1),('Argentina',2),('Australia',3),('Austria',4),('Belgium',5),('Bolivia',6),('Brazil',7),('Brunei Darussalam',8),('Bulgaria',9),('Cambodia',10),('Canada',11),('Chile',12),('China',13),('Croatia',14),('Denmark',15),('Ecuador',16),('Egypt',17),('Ethiopia',18),('Europe',19),('Fiji Islands',20),('Finland',21),('France',22),('Germany',23),('Greece',24),('Hong Kong',25),('Hungary',26),('Iceland',27),('India',28),('Indonesia',29),('Irealand',30),('Ireland',31),('Israel',32),('Italy',33),('Japan',34),('Jordan',35),('Kenya',36),('Latvia',37),('Lithuania',38),('Luxembourg',39),('Malaysia',40),('Malta',41),('Mauritius',42),('Mexico',43),('Moldova',44),('Mongolia',45),('Montenegro',46),('Morocco',47),('Mozambique',48),('Netherlands',49),('New Zealand',50),('Nigeria',51),('Norway',52),('Osterreich',53),('Pakistan',54),('Paraguay',55),('Peru',56),('Philippines',57),('Poland',58),('Portugal',59),('Romania',60),('Russia',61),('Saudi Arabia',62),('Serbia & Montenegro',63),('Singapore',64),('Slovakia',65),('Slovenia',66),('Solomon Islands',67),('South Africa',68),('South Korea',69),('Spain',70),('Sri Lanka',71),('Sweden',72),('Switzerland',73),('Taiwan',74),('Thailand',75),('Tunisia',76),('Turkey',77),('Ukraine',78),('United Arab Emirates',79),('United Kingdom',80),('USA',81),('Vietnam',82);
/*!40000 ALTER TABLE `country` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-08-02 14:45:55
