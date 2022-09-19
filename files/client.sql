-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Anamakine: localhost
-- Üretim Zamanı: 21 Oca 2021, 19:35:59
-- Sunucu sürümü: 10.3.27-MariaDB-0+deb10u1
-- PHP Sürümü: 7.3.19-1~deb10u1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `ibiza`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `client`
--

CREATE TABLE `client` (
  `ID` int(12) NOT NULL,
  `nickname` varchar(24) DEFAULT NULL,
  `tempname` varchar(24) DEFAULT NULL,
  `CPUID` varchar(32) DEFAULT NULL,
  `MAC` varchar(32) DEFAULT NULL,
  `MSERIAL` varchar(32) DEFAULT NULL,
  `RAM` varchar(64) DEFAULT NULL,
  `ip` varchar(16) DEFAULT NULL,
  `verified` int(12) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo için indeksler `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`ID`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `client`
--
ALTER TABLE `client`
  MODIFY `ID` int(12) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
