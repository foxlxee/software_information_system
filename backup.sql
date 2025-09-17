-- MySQL dump 10.13  Distrib 8.4.5, for Win64 (x86_64)
--
-- Host: localhost    Database: installsdb
-- ------------------------------------------------------
-- Server version	8.4.5

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Офисные приложения'),(2,'Инструменты разработки'),(3,'Графические редакторы'),(4,'СУБД'),(5,'Веб-браузеры'),(6,'Игровые платформы'),(7,'Средства виртуализации'),(8,'Антивирусы'),(9,'Видеоредакторы'),(10,'Аудиоредакторы'),(11,'Математическое ПО'),(12,'Архиваторы'),(13,'Системы контроля версий'),(14,'3D моделирование'),(15,'Системы резервного копирования'),(16,'Игры'),(17,'Видеоконференции'),(19,'Видеоплееры');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `developers`
--

DROP TABLE IF EXISTS `developers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `developers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `developers`
--

LOCK TABLES `developers` WRITE;
/*!40000 ALTER TABLE `developers` DISABLE KEYS */;
INSERT INTO `developers` VALUES (1,'Microsoft'),(2,'JetBrains'),(4,'Oracle'),(5,'Google'),(6,'Яндекс'),(8,'MathWorks'),(9,'Wolfram Research'),(10,'Maplesoft'),(11,'R Foundation'),(12,'StataCorp'),(13,'PTC'),(14,'Adobe'),(15,'Blender Foundation'),(16,'Zoom Video Communications'),(17,'Unity Technologies'),(18,'Epic Games'),(19,'Valve'),(20,'Mozilla Foundation'),(22,'Python Software Foundation'),(23,'Itseez'),(24,'The Document Foundation'),(25,'Kaspersky Lab'),(26,'NortonLifeLock'),(27,'VideoLAN'),(28,'Blackmagic Design'),(29,'Audacity Team');
/*!40000 ALTER TABLE `developers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employees` (
  `id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` VALUES (1,'Иванов Артём Дмитриевич'),(2,'Попова Елизавета Сергеевна'),(3,'Лебедева Мария Олеговна'),(4,'Козлов Денис Андреевич'),(5,'Морозов Кирилл Алексеевич'),(6,'Пушкин Александр Сергеевич'),(7,'Кличко Владимир Владимирович'),(8,'Павлова Виктория Евгеньевна'),(9,'Маск Илон Иванович');
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `installation_rooms`
--

DROP TABLE IF EXISTS `installation_rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `installation_rooms` (
  `installation_id` int NOT NULL,
  `room_number` int NOT NULL,
  PRIMARY KEY (`installation_id`,`room_number`),
  CONSTRAINT `installation_rooms_ibfk_1` FOREIGN KEY (`installation_id`) REFERENCES `installations` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `installation_rooms`
--

LOCK TABLES `installation_rooms` WRITE;
/*!40000 ALTER TABLE `installation_rooms` DISABLE KEYS */;
INSERT INTO `installation_rooms` VALUES (11,101),(12,101),(13,101),(14,101),(14,233),(15,231),(15,233),(16,101),(16,102),(17,101),(17,231),(18,101),(18,231),(18,233),(19,101),(20,125),(21,125),(21,233),(22,123),(23,231),(23,233),(24,231),(24,233),(25,101),(26,125),(27,121),(27,125),(27,231),(27,233),(28,231),(28,233),(29,101),(29,123),(29,125),(30,101),(31,125),(32,125),(33,201),(34,101),(34,102),(35,1),(35,101),(35,102),(35,103);
/*!40000 ALTER TABLE `installation_rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `installations`
--

DROP TABLE IF EXISTS `installations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `installations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `software_id` int DEFAULT NULL,
  `version_id` int DEFAULT NULL,
  `installation_date` date DEFAULT NULL,
  `employee_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `software_id` (`software_id`),
  KEY `version_id` (`version_id`),
  KEY `employee_id` (`employee_id`),
  CONSTRAINT `installations_ibfk_1` FOREIGN KEY (`software_id`) REFERENCES `software` (`id`),
  CONSTRAINT `installations_ibfk_2` FOREIGN KEY (`version_id`) REFERENCES `versions` (`id`),
  CONSTRAINT `installations_ibfk_3` FOREIGN KEY (`employee_id`) REFERENCES `employees` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `installations`
--

LOCK TABLES `installations` WRITE;
/*!40000 ALTER TABLE `installations` DISABLE KEYS */;
INSERT INTO `installations` VALUES (11,17,10,'2025-05-01',6),(12,19,8,'2025-05-01',6),(13,22,11,'2025-05-01',6),(14,23,8,'2025-05-23',5),(15,21,7,'2025-05-10',3),(16,7,12,'2025-05-02',4),(17,2,4,'2025-05-23',7),(18,14,5,'2025-05-05',3),(19,3,13,'2025-05-23',1),(20,9,8,'2025-05-05',7),(21,1,2,'2025-04-10',3),(22,19,13,'2025-05-01',2),(23,18,14,'2025-03-01',6),(24,6,14,'2025-05-03',6),(25,23,13,'2025-02-01',1),(26,11,15,'2025-03-01',7),(27,15,4,'2025-05-23',3),(28,20,1,'2025-02-01',3),(29,17,10,'2025-01-30',5),(30,30,16,'2025-05-02',3),(31,25,5,'2025-02-01',9),(32,20,1,'2025-02-01',9),(33,14,17,'2025-05-23',4),(34,1,13,'2025-05-05',2),(35,15,4,'2025-05-26',7);
/*!40000 ALTER TABLE `installations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `software`
--

DROP TABLE IF EXISTS `software`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `software` (
  `id` int NOT NULL AUTO_INCREMENT,
  `developer_id` int DEFAULT NULL,
  `category_id` int DEFAULT NULL,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `developer_id` (`developer_id`),
  KEY `category_id` (`category_id`),
  CONSTRAINT `software_ibfk_1` FOREIGN KEY (`developer_id`) REFERENCES `developers` (`id`),
  CONSTRAINT `software_ibfk_2` FOREIGN KEY (`category_id`) REFERENCES `categories` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `software`
--

LOCK TABLES `software` WRITE;
/*!40000 ALTER TABLE `software` DISABLE KEYS */;
INSERT INTO `software` VALUES (1,8,11,'MATLAB'),(2,9,11,'Mathematica'),(3,10,11,'Maple'),(4,11,2,'R'),(5,12,11,'Stata'),(6,13,11,'Mathcad'),(7,14,3,'Adobe Photoshop'),(8,15,14,'Blender'),(9,16,17,'Zoom'),(10,17,2,'Unity'),(11,18,2,'Unreal Engine'),(12,19,6,'Steam'),(13,20,5,'Firefox'),(14,5,5,'Google Chrome'),(15,1,1,'Microsoft Office'),(17,22,2,'Python'),(18,23,2,'OpenCV'),(19,1,2,'Visual Studio Code'),(20,1,2,'Visual Studio'),(21,2,2,'IntelliJ IDEA'),(22,2,2,'PyCharm'),(23,4,7,'VirtualBox'),(24,24,1,'LibreOffice'),(25,25,8,'Kaspersky Total Security'),(26,26,8,'Norton 360'),(27,27,19,'VLC Media Player'),(28,28,9,'DaVinci Resolve'),(29,29,10,'Audacity'),(30,4,4,'MySQL');
/*!40000 ALTER TABLE `software` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `versions`
--

DROP TABLE IF EXISTS `versions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `versions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `versions`
--

LOCK TABLES `versions` WRITE;
/*!40000 ALTER TABLE `versions` DISABLE KEYS */;
INSERT INTO `versions` VALUES (1,'2022'),(2,'3.0'),(3,'3.8'),(4,'2024'),(5,'125'),(6,'2020'),(7,'Community Edition 2024.3.1'),(8,'2025'),(9,'1'),(10,'3.7'),(11,'Community Edition 2024.1.2'),(12,'CC 2021'),(13,'2023'),(14,'4.0'),(15,'5.0'),(16,'8.3'),(17,'124');
/*!40000 ALTER TABLE `versions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-09  5:48:47
