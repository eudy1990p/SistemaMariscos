-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-08-2017 a las 01:27:59
-- Versión del servidor: 10.1.22-MariaDB
-- Versión de PHP: 7.0.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db_sistema_venta_reparacion_celulares`
--
CREATE DATABASE IF NOT EXISTS `db_sistema_venta_reparacion_celulares` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `db_sistema_venta_reparacion_celulares`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

DROP TABLE IF EXISTS `clientes`;
CREATE TABLE IF NOT EXISTS `clientes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(150) DEFAULT NULL,
  `apellido` varchar(150) DEFAULT NULL,
  `cedula` varchar(40) DEFAULT NULL,
  `rnc` varchar(60) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `clientes`
--

REPLACE INTO `clientes` (`id`, `nombre`, `apellido`, `cedula`, `rnc`, `fecha`, `usuario_id`, `visible`) VALUES
(1, 'rafa', 'prueba1', 'prueba1', 'prueba1', '2017-06-25 11:21:51', 1, 1),
(3, 'arias', 'prueba3', 'prueba3', 'prueba3', '2017-06-25 11:28:59', 1, 1),
(5, 'amaury', 'cuello', 'buscarcliente', 'buscarcliente', '2017-06-25 12:45:32', 1, 1),
(6, 'eudy1234', 'eudy1234', 'eudy1234', 'eudy1234', '2017-06-25 12:47:00', 1, 1),
(7, 'reparaciones', 'reparaciones', 'reparaciones', 'reparaciones', '2017-06-25 20:20:40', 1, 0),
(8, 'cirilo ', 'cuello', '001-0252025-1', '', '2017-07-30 14:25:45', 2, 1),
(9, 'wellinton', 'quezada', '001-1122220-2', '3535', '2017-07-30 14:49:19', 2, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cliente_email`
--

DROP TABLE IF EXISTS `cliente_email`;
CREATE TABLE IF NOT EXISTS `cliente_email` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `correo` varchar(150) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `cliente_email`
--

REPLACE INTO `cliente_email` (`id`, `correo`, `fecha`, `usuario_id`, `visible`) VALUES
(1, 'prueba3', '2017-06-25 11:28:59', 1, 1),
(2, 'prueba4', '2017-06-25 12:42:20', 1, 1),
(3, 'prueba4', '2017-06-25 12:42:20', 1, 1),
(4, 'prueba4', '2017-06-25 12:42:20', 1, 1),
(5, 'buscarcliente', '2017-06-25 12:45:32', 1, 1),
(6, 'eudy1234', '2017-06-25 12:47:00', 1, 1),
(7, 'reparaciones', '2017-06-25 20:20:40', 1, 1),
(8, 'cirilocuello00@gmail.com', '2017-07-30 14:25:45', 2, 1),
(9, 'we@gami.com', '2017-07-30 14:49:19', 2, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cliente_telefono`
--

DROP TABLE IF EXISTS `cliente_telefono`;
CREATE TABLE IF NOT EXISTS `cliente_telefono` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `telefono` varchar(150) DEFAULT NULL,
  `tipo` enum('celular','casa','trabajo','otro') DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `cliente_telefono`
--

REPLACE INTO `cliente_telefono` (`id`, `telefono`, `tipo`, `fecha`, `usuario_id`, `visible`) VALUES
(1, 'prueba3', 'celular', '2017-06-25 11:28:59', 1, 1),
(2, 'prueba4', 'celular', '2017-06-25 12:42:20', 1, 1),
(3, 'prueba4', 'celular', '2017-06-25 12:42:20', 1, 1),
(4, 'prueba4', 'trabajo', '2017-06-25 12:42:20', 1, 1),
(5, 'buscarcliente', 'celular', '2017-06-25 12:45:32', 1, 1),
(6, 'eudy1234', 'celular', '2017-06-25 12:47:00', 1, 1),
(7, 'reparaciones', 'celular', '2017-06-25 20:20:40', 1, 1),
(8, '829-420-6078', 'celular', '2017-07-30 14:25:45', 2, 1),
(9, '56789', 'celular', '2017-07-30 14:49:19', 2, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `compras`
--

DROP TABLE IF EXISTS `compras`;
CREATE TABLE IF NOT EXISTS `compras` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `equipo_id` int(11) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `precio_venta` decimal(20,2) DEFAULT NULL,
  `precio_compra` decimal(20,2) DEFAULT NULL,
  `monto_compra` decimal(20,2) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `nombre_suplidor` varchar(150) DEFAULT NULL,
  `telefono_suplidor` varchar(150) DEFAULT NULL,
  `email_suplidor` varchar(150) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `compras`
--

REPLACE INTO `compras` (`id`, `equipo_id`, `cantidad`, `precio_venta`, `precio_compra`, `monto_compra`, `fecha`, `usuario_id`, `nombre_suplidor`, `telefono_suplidor`, `email_suplidor`, `visible`) VALUES
(1, 0, 23, '144.00', '123.00', '2829.00', '2017-06-24 08:36:49', 1, 'tp', 'eud@gmail.com', '23454556', 1),
(2, 1, 1, '3.00', '2.00', '2.00', '2017-06-24 09:20:05', 1, '', '', '', 1),
(3, 1, 1, '3.00', '2.00', '2.00', '2017-06-24 09:24:52', 1, '', '', '', 1),
(4, 1, 12, '4.00', '3.00', '36.00', '2017-06-24 09:30:28', 1, '', '', '', 1),
(5, 1, 3, '3.00', '34.00', '102.00', '2017-06-24 09:31:38', 1, '', '', '', 1),
(6, 1, 6, '20.00', '19.00', '114.00', '2017-06-24 09:58:20', 1, '', '', '', 1),
(7, 1, 3, '60.00', '45.00', '135.00', '2017-06-24 12:29:25', 1, '', '', '', 1),
(8, 2, 12, '0.00', '6000.00', '72000.00', '2017-06-24 22:31:09', 1, '', '', '', 1),
(9, 4, 5, '0.00', '20000.00', '100000.00', '2017-07-07 12:13:32', 1, 'no hay', 'no hay', 'no hay', 1),
(10, 4, 3, '0.00', '1000.00', '3000.00', '2017-07-07 12:44:58', 1, 'no hay', 'no hay', 'no hay', 1),
(11, 1, 9, '0.00', '2000.00', '18000.00', '2017-07-07 12:51:52', 1, 'no hay', 'no hay', 'no hay', 1),
(12, 4, 9, '0.00', '9000.00', '81000.00', '2017-07-07 12:56:37', 1, 'no', 'no', 'no', 1),
(13, 4, 9, '0.00', '9000.00', '81000.00', '2017-07-07 12:56:51', 1, 'no', 'no', 'no', 1),
(14, 2, 3, '0.00', '1000.00', '3000.00', '2017-07-07 13:12:14', 1, '', '', '', 1),
(15, 4, 2, '0.00', '10.00', '20.00', '2017-07-07 13:13:57', 1, '', '', '', 1),
(16, 1, 7, '0.00', '90.00', '630.00', '2017-07-07 13:20:47', 1, '', '', '', 1),
(17, 2, 100, '600.00', '500.00', '50000.00', '2017-07-30 14:50:41', 2, 'fagrovet', 'vageto@gamil.com', '809-897-987', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `equipos`
--

DROP TABLE IF EXISTS `equipos`;
CREATE TABLE IF NOT EXISTS `equipos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `marca` varchar(100) DEFAULT NULL,
  `modelo` varchar(100) DEFAULT NULL,
  `cantidad_almacen` int(11) DEFAULT NULL,
  `precio_venta` decimal(20,2) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `equipos`
--

REPLACE INTO `equipos` (`id`, `marca`, `modelo`, `cantidad_almacen`, `precio_venta`, `fecha`, `usuario_id`, `visible`) VALUES
(1, 'p1', 'p1', 27, '60.00', '2017-06-23 18:45:02', 1, 1),
(2, 'samsung', 'note', 109, '600.00', '2017-06-24 22:30:11', 1, 1),
(3, 'nexus', 'v8', 0, '10000.00', '2017-06-25 12:57:38', 1, 1),
(4, 'Samsung Galaxy', 'S8', 28, '24990.00', '2017-07-07 12:12:47', 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura_reparacion`
--

DROP TABLE IF EXISTS `factura_reparacion`;
CREATE TABLE IF NOT EXISTS `factura_reparacion` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sub_total` decimal(20,2) DEFAULT NULL,
  `monto_apagar` decimal(20,2) DEFAULT NULL,
  `monto_itbis` decimal(20,2) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `estado` enum('pendiente','pagada','cancelada') DEFAULT 'pendiente',
  `usuario_id_cancelacion` int(11) DEFAULT NULL,
  `motivo_cancelacion` text,
  `cliente_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `factura_reparacion`
--

REPLACE INTO `factura_reparacion` (`id`, `sub_total`, `monto_apagar`, `monto_itbis`, `fecha`, `usuario_id`, `estado`, `usuario_id_cancelacion`, `motivo_cancelacion`, `cliente_id`, `visible`) VALUES
(1, '2829.00', '3338.22', '509.22', '2017-06-25 21:23:19', 1, 'pendiente', NULL, NULL, 7, 1),
(2, '24000.00', '28320.00', '4320.00', '2017-06-26 11:50:23', 1, 'pendiente', NULL, NULL, 6, 1),
(3, '246.00', '290.28', '44.28', '2017-06-26 14:59:16', 1, 'pendiente', NULL, NULL, 6, 1),
(4, '123.00', '145.14', '22.14', '2017-06-26 19:13:18', 1, 'pendiente', NULL, NULL, 6, 1),
(5, '1000.00', '1180.00', '180.00', '2017-06-30 22:32:41', 1, 'pendiente', NULL, NULL, 6, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura_reparacion_detalle`
--

DROP TABLE IF EXISTS `factura_reparacion_detalle`;
CREATE TABLE IF NOT EXISTS `factura_reparacion_detalle` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cantidad` int(11) DEFAULT NULL,
  `monto` decimal(20,2) DEFAULT NULL,
  `precio` decimal(20,2) DEFAULT NULL,
  `problema` text,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `estado` enum('pendiente','completado','cancelado') DEFAULT 'pendiente',
  `usuario_id_cancelacion` int(11) DEFAULT NULL,
  `motivo_cancelacion` text,
  `factura_reparacion_id` int(11) DEFAULT NULL,
  `equipo_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `factura_reparacion_detalle`
--

REPLACE INTO `factura_reparacion_detalle` (`id`, `cantidad`, `monto`, `precio`, `problema`, `fecha`, `usuario_id`, `estado`, `usuario_id_cancelacion`, `motivo_cancelacion`, `factura_reparacion_id`, `equipo_id`, `visible`) VALUES
(1, 23, '2829.00', '123.00', 'problema', '2017-06-25 21:23:19', 1, 'pendiente', NULL, NULL, 1, 3, 1),
(2, 3, '24000.00', '8000.00', 'error de los grandes.', '2017-06-26 11:50:23', 1, 'pendiente', NULL, NULL, 2, 2, 1),
(3, 2, '246.00', '123.00', 'ccx', '2017-06-26 14:59:16', 1, 'pendiente', NULL, NULL, 3, 3, 1),
(4, 1, '123.00', '123.00', 'ttuyuui ii ', '2017-06-26 19:13:18', 1, 'pendiente', NULL, NULL, 4, 2, 1),
(5, 1, '1000.00', '1000.00', 'se descarga rapido', '2017-06-30 22:32:41', 1, 'pendiente', NULL, NULL, 5, 3, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura_ventas`
--

DROP TABLE IF EXISTS `factura_ventas`;
CREATE TABLE IF NOT EXISTS `factura_ventas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sub_total` decimal(20,2) DEFAULT NULL,
  `monto_apagar` decimal(20,2) DEFAULT NULL,
  `monto_itbis` decimal(20,2) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `estado` enum('pendiente','pagada','cancelada') DEFAULT 'pendiente',
  `usuario_id_cancelacion` int(11) DEFAULT NULL,
  `motivo_cancelacion` text,
  `cliente_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `factura_ventas`
--

REPLACE INTO `factura_ventas` (`id`, `sub_total`, `monto_apagar`, `monto_itbis`, `fecha`, `usuario_id`, `estado`, `usuario_id_cancelacion`, `motivo_cancelacion`, `cliente_id`, `visible`) VALUES
(1, '6000.00', '7080.00', '1080.00', '2017-06-25 17:40:05', 1, 'pendiente', NULL, NULL, 6, 1),
(2, '240.00', '240.00', '0.00', '2017-06-25 18:11:26', 1, 'pendiente', NULL, NULL, 6, 1),
(3, '180.00', '180.00', '0.00', '2017-06-25 18:23:05', 1, 'pendiente', NULL, NULL, 6, 1),
(4, '6000.00', '7080.00', '1080.00', '2017-06-26 11:14:37', 1, 'pendiente', NULL, NULL, 6, 1),
(5, '6000.00', '7080.00', '1080.00', '2017-06-26 14:55:15', 1, 'pendiente', NULL, NULL, 6, 1),
(6, '60.00', '70.80', '10.80', '2017-06-26 15:00:25', 1, 'pendiente', NULL, NULL, 6, 1),
(7, '6000.00', '7080.00', '1080.00', '2017-06-26 17:05:32', 1, 'pendiente', NULL, NULL, 6, 1),
(8, '6000.00', '6000.00', '0.00', '2017-06-26 18:00:11', 1, 'pendiente', NULL, NULL, 6, 1),
(9, '60.00', '70.80', '10.80', '2017-06-26 19:11:26', 1, 'pendiente', NULL, NULL, 6, 1),
(10, '60.00', '70.80', '10.80', '2017-06-26 21:55:10', 1, 'pendiente', NULL, NULL, 6, 1),
(11, '60.00', '60.00', '0.00', '2017-06-26 22:38:53', 1, 'pendiente', NULL, NULL, 6, 1),
(12, '60.00', '70.80', '10.80', '2017-06-29 13:23:17', 1, 'pendiente', NULL, NULL, 6, 1),
(13, '6000.00', '7080.00', '1080.00', '2017-06-30 22:27:43', 1, 'pendiente', NULL, NULL, 6, 1),
(14, '6000.00', '7080.00', '1080.00', '2017-07-06 15:15:19', 1, 'pendiente', NULL, NULL, 6, 1),
(15, '60.00', '60.00', '0.00', '2017-07-30 14:53:25', 2, 'pendiente', NULL, NULL, 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura_venta_detalle`
--

DROP TABLE IF EXISTS `factura_venta_detalle`;
CREATE TABLE IF NOT EXISTS `factura_venta_detalle` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `equipo_id` int(11) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `monto` decimal(20,2) DEFAULT NULL,
  `precio` decimal(20,2) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `estado` enum('pendiente','cancelado') DEFAULT 'pendiente',
  `usuario_id_cancelacion` int(11) DEFAULT NULL,
  `motivo_cancelacion` text,
  `factura_venta_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `factura_venta_detalle`
--

REPLACE INTO `factura_venta_detalle` (`id`, `equipo_id`, `cantidad`, `monto`, `precio`, `fecha`, `usuario_id`, `estado`, `usuario_id_cancelacion`, `motivo_cancelacion`, `factura_venta_id`, `visible`) VALUES
(1, 2, 1, '6000.00', '6000.00', '2017-06-25 17:40:05', 1, 'pendiente', NULL, NULL, 1, 1),
(2, 1, 4, '240.00', '60.00', '2017-06-25 18:11:26', 1, 'pendiente', NULL, NULL, 2, 1),
(3, 1, 3, '180.00', '60.00', '2017-06-25 18:23:05', 1, 'pendiente', NULL, NULL, 3, 1),
(4, 2, 1, '6000.00', '6000.00', '2017-06-26 11:14:37', 1, 'pendiente', NULL, NULL, 4, 1),
(5, 2, 1, '6000.00', '6000.00', '2017-06-26 14:55:15', 1, 'pendiente', NULL, NULL, 5, 1),
(6, 1, 1, '60.00', '60.00', '2017-06-26 15:00:25', 1, 'pendiente', NULL, NULL, 6, 1),
(7, 2, 1, '6000.00', '6000.00', '2017-06-26 17:05:32', 1, 'pendiente', NULL, NULL, 7, 1),
(8, 2, 1, '6000.00', '6000.00', '2017-06-26 18:00:11', 1, 'pendiente', NULL, NULL, 8, 1),
(9, 1, 1, '60.00', '60.00', '2017-06-26 19:11:26', 1, 'pendiente', NULL, NULL, 9, 1),
(10, 1, 1, '60.00', '60.00', '2017-06-26 21:55:10', 1, 'pendiente', NULL, NULL, 10, 1),
(11, 1, 1, '60.00', '60.00', '2017-06-26 22:38:53', 1, 'pendiente', NULL, NULL, 11, 1),
(12, 1, 1, '60.00', '60.00', '2017-06-29 13:23:17', 1, 'pendiente', NULL, NULL, 12, 1),
(13, 2, 1, '6000.00', '6000.00', '2017-06-30 22:27:43', 1, 'pendiente', NULL, NULL, 13, 1),
(14, 2, 1, '6000.00', '6000.00', '2017-07-06 15:15:20', 1, 'pendiente', NULL, NULL, 14, 1),
(15, 1, 1, '60.00', '60.00', '2017-07-30 14:53:25', 2, 'pendiente', NULL, NULL, 15, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

DROP TABLE IF EXISTS `pagos`;
CREATE TABLE IF NOT EXISTS `pagos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tipo_factura` enum('venta','reparacion') DEFAULT 'venta',
  `factura_reparacion_id` int(11) DEFAULT NULL,
  `factura_venta_id` int(11) DEFAULT NULL,
  `monto_pagado` decimal(20,2) DEFAULT NULL,
  `devuelta` decimal(20,2) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `tipo_pago` enum('EFECTIVO','CHEQUE','TARJETA DE CREDITO','TRANSFERENCIA BANCARIA') DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `pagos`
--

REPLACE INTO `pagos` (`id`, `tipo_factura`, `factura_reparacion_id`, `factura_venta_id`, `monto_pagado`, `devuelta`, `fecha`, `tipo_pago`, `usuario_id`, `visible`) VALUES
(1, 'venta', 0, 1, '8000.00', '920.00', '2017-06-25 17:40:05', 'EFECTIVO', 1, 1),
(2, 'venta', 0, 2, '240.00', '0.00', '2017-06-25 18:11:26', 'EFECTIVO', 1, 1),
(3, 'venta', 0, 3, '300.00', '120.00', '2017-06-25 18:23:05', 'EFECTIVO', 1, 1),
(4, 'reparacion', 1, 0, '4000.00', '661.78', '2017-06-25 21:23:19', 'EFECTIVO', 1, 1),
(5, 'venta', 0, 4, '20000.00', '12920.00', '2017-06-26 11:14:37', 'EFECTIVO', 1, 1),
(6, 'reparacion', 2, 0, '40000.00', '11680.00', '2017-06-26 11:50:23', 'EFECTIVO', 1, 1),
(7, 'venta', 0, 5, '8000.00', '920.00', '2017-06-26 14:55:15', 'CHEQUE', 1, 1),
(8, 'reparacion', 3, 0, '400.00', '109.72', '2017-06-26 14:59:16', 'EFECTIVO', 1, 1),
(9, 'venta', 0, 6, '80.00', '9.20', '2017-06-26 15:00:25', 'EFECTIVO', 1, 1),
(10, 'venta', 0, 7, '8000.00', '920.00', '2017-06-26 17:05:32', 'EFECTIVO', 1, 1),
(11, 'venta', 0, 8, '6001.00', '1.00', '2017-06-26 18:00:11', 'EFECTIVO', 1, 1),
(12, 'venta', 0, 9, '90.00', '19.20', '2017-06-26 19:11:26', 'EFECTIVO', 1, 1),
(13, 'reparacion', 4, 0, '2000.00', '1854.86', '2017-06-26 19:13:18', 'EFECTIVO', 1, 1),
(14, 'venta', 0, 10, '100.00', '29.20', '2017-06-26 21:55:10', 'EFECTIVO', 1, 1),
(15, 'venta', 0, 11, '90.00', '30.00', '2017-06-26 22:38:54', 'EFECTIVO', 1, 1),
(16, 'venta', 0, 12, '90.00', '19.20', '2017-06-29 13:23:17', 'EFECTIVO', 1, 1),
(17, 'venta', 0, 13, '8000.00', '920.00', '2017-06-30 22:27:43', 'EFECTIVO', 1, 1),
(18, 'reparacion', 5, 0, '2000.00', '820.00', '2017-06-30 22:32:41', 'EFECTIVO', 1, 1),
(19, 'venta', 0, 14, '9000.00', '1920.00', '2017-07-06 15:15:20', 'EFECTIVO', 1, 1),
(20, 'venta', 0, 15, '60.00', '0.00', '2017-07-30 14:53:25', 'EFECTIVO', 2, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE IF NOT EXISTS `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` varchar(100) DEFAULT NULL,
  `clave` varchar(100) DEFAULT NULL,
  `tipo` enum('cajero','administrador','supervisor') DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `visible` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `usuarios`
--

REPLACE INTO `usuarios` (`id`, `usuario`, `clave`, `tipo`, `fecha`, `usuario_id`, `visible`) VALUES
(1, 'eudy1234', '1234', 'cajero', '2017-06-23 12:54:46', 1, 1),
(2, 'administrador', 'administrador', 'supervisor', '2017-06-23 16:18:20', 1, 1),
(3, 'cajero', 'cajero', 'cajero', '2017-06-23 16:19:46', 1, 1),
(4, 'supervisor', 'supervisor', 'supervisor', '2017-06-26 18:07:59', 1, 1);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
