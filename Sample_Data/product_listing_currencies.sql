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
-- Table structure for table `currencies`
--

DROP TABLE IF EXISTS `currencies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `currencies` (
  `Code` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Description` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Symbol` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Exchange_Rate_Date` date DEFAULT NULL,
  `Exchange_Rate` decimal(64,5) DEFAULT NULL,
  `Id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `currencies`
--

LOCK TABLES `currencies` WRITE;
/*!40000 ALTER TABLE `currencies` DISABLE KEYS */;
INSERT INTO `currencies` VALUES ('AED','United Arab Emirates dirham','','2017-01-01',0.54115,1),('AUD','Australian dollar','$','2017-01-01',0.79405,2),('BGN','Bulgarian leva','','2017-01-01',0.73178,3),('BND','Brunei Darussalem dollar','$','2017-01-01',0.90981,4),('BRL','Brazilian real','','2017-01-01',0.56765,5),('CHF','Swiss franc','','2017-01-01',1.25806,6),('CZK','Czech koruna','','2017-01-01',0.06162,7),('DKK','Danish krone','KR','2017-01-01',0.27040,8),('DZD','Algerian dinar','','2017-01-01',0.02596,9),('EUR','Euro','EU','2017-01-01',1.49500,10),('FJD','Fiji dollar','$','2017-01-01',0.69776,11),('GBP','Pound Sterling','GB','2017-01-01',2.31506,12),('HKD','Hong Kong dollar','$','2017-01-01',0.20288,13),('HRK','Croatian Kuna','EU','2017-01-01',0.27356,14),('HUF','Hungarian forint','','2017-01-01',0.00850,15),('IDR','Indonesian rupiah','','2017-01-01',0.00022,16),('INR','Indian rupee','','2017-01-01',0.03334,17),('ISK','Icelandic krona','KR','2017-01-01',0.02303,18),('JPY','Japanese yen','JPY','2017-01-01',0.01343,19),('KES','Kenyan Shilling','','2017-01-01',0.02506,20),('LTL','Lithuanian litas','EU','2017-01-01',0.39532,21),('LVL','Latvian lats','EU','2017-01-01',2.57158,22),('MAD','Moroccan dirham','','2017-01-01',0.19061,23),('MXN','Mexican peso','$','2017-01-01',0.16691,24),('MYR','Malaysian ringgit','','2017-01-01',0.41809,25),('MZM','Mozambique metical','','2017-01-01',0.00008,26),('NGN','Nigerian naira','','2017-01-01',0.01574,27),('NOK','Norwegian krone','KR','2017-01-01',0.23754,28),('NZD','New Zealand dollar','$','2017-01-01',0.67205,29),('PHP','Philippines peso','','2017-01-01',0.03101,30),('PLN','Polish zloty','','2017-01-01',0.38429,31),('RON','Romanian leu','','2017-01-01',0.00006,32),('RSD','Serbian Dinar','','2017-01-01',0.02366,33),('RUB','Russian ruble','','2017-01-01',0.05440,34),('SAR','Saudi Arabian ryial','','2017-01-01',0.42320,35),('SBD','Solomon Islands dollar','$','2017-01-01',0.29379,36),('SEK','Swedish krona','KR','2017-01-01',0.22911,37),('SGD','Singapore dollar','$','2017-01-01',0.91560,38),('SZL','Swaziland lilangeni','','2017-01-01',0.21841,39),('THB','Thai baht','','2017-01-01',0.03563,40),('TND','Tunesian dinar','','2017-01-01',1.45875,41),('TOP','Tongan Pa anga','','2017-01-01',0.79000,42),('TRY','New Turkish lira','','2017-01-01',1.23484,43),('UGS','Ugandan Shilling','','2017-01-01',0.00114,44),('USD','US dollar','$','2017-01-01',1.50207,45),('VUV','Vanuatu vatu','','2017-01-01',0.01111,46),('WST','Western Samoan tala','','2017-01-01',5.54552,47),('XPF','French Pacific Franc','','2017-01-01',0.01691,48),('ZAR','South African rand','','2017-01-01',0.18498,49);
/*!40000 ALTER TABLE `currencies` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-08-02 14:45:54
