-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-05-2024 a las 23:28:47
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `proyecto_biblioteca`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `registrar_prestamos`
--

CREATE TABLE `registrar_prestamos` (
  `id_registro` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `titulo_libro` varchar(50) NOT NULL,
  `autor` varchar(50) NOT NULL,
  `clasificacion` varchar(40) NOT NULL,
  `folio` varchar(40) NOT NULL,
  `nombre_alumno` varchar(50) NOT NULL,
  `carrera` varchar(50) NOT NULL,
  `numero_control` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `registrar_prestamos`
--
ALTER TABLE `registrar_prestamos`
  ADD PRIMARY KEY (`id_registro`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `registrar_prestamos`
--
ALTER TABLE `registrar_prestamos`
  MODIFY `id_registro` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
