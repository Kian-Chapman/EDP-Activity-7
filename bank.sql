-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 29, 2024 at 04:10 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bank`
--

-- --------------------------------------------------------

--
-- Table structure for table `accounts`
--

CREATE TABLE `accounts` (
  `AccountID` int(11) NOT NULL,
  `UserID` varchar(20) DEFAULT NULL,
  `Balance` decimal(12,2) NOT NULL DEFAULT 0.00,
  `LastActivityDate` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `accounts`
--

INSERT INTO `accounts` (`AccountID`, `UserID`, `Balance`, `LastActivityDate`) VALUES
(1001, '2024-6385-93748', 80.00, '2024-04-10'),
(1002, '2024-3712-35456', 300.00, '2024-04-10'),
(1003, '2024-6901-13536', 900.00, '2024-04-10'),
(1004, '2024-7419-34573', 1500.00, '2024-04-10'),
(1005, '2024-1578-45362', 100.00, '2024-04-10'),
(1006, '2024-9400-95190', 4000.00, '2024-04-17'),
(1007, '2024-2688-76284', 15000.00, '2024-04-17');

-- --------------------------------------------------------

--
-- Table structure for table `administrator`
--

CREATE TABLE `administrator` (
  `AdminID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Role` enum('admin','super_admin') NOT NULL DEFAULT 'admin'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `administrator`
--

INSERT INTO `administrator` (`AdminID`, `Username`, `Password`, `Name`, `Email`, `Role`) VALUES
(1, 'Admin', 'Admin', 'Yggdrass', 'admin@example.com', 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE `transactions` (
  `TransactionID` int(11) NOT NULL,
  `AccountID` int(11) DEFAULT NULL,
  `TransactionType` varchar(20) NOT NULL,
  `Amount` decimal(12,2) NOT NULL,
  `TransactionDate` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transactions`
--

INSERT INTO `transactions` (`TransactionID`, `AccountID`, `TransactionType`, `Amount`, `TransactionDate`) VALUES
(1001, 1001, 'Deposit', 60.00, '2024-04-10 16:00:00'),
(1002, 1001, 'Withdrawal', 25.00, '2024-04-11 16:00:00'),
(1003, 1001, 'Deposit', 70.00, '2024-04-12 16:00:00'),
(1004, 1001, 'Withdrawal', 40.00, '2024-04-13 16:00:00'),
(1005, 1001, 'Deposit', 30.00, '2024-04-14 16:00:00'),
(1008, 1002, 'Deposit', 80.00, '2024-04-10 16:00:00'),
(1009, 1002, 'Withdrawal', 20.00, '2024-04-11 16:00:00'),
(1010, 1002, 'Deposit', 50.00, '2024-04-12 16:00:00'),
(1011, 1002, 'Withdrawal', 30.00, '2024-04-13 16:00:00'),
(1012, 1002, 'Deposit', 100.00, '2024-04-14 16:00:00'),
(1015, 1003, 'Deposit', 150.00, '2024-04-10 16:00:00'),
(1016, 1003, 'Withdrawal', 40.00, '2024-04-11 16:00:00'),
(1017, 1003, 'Deposit', 90.00, '2024-04-12 16:00:00'),
(1018, 1003, 'Withdrawal', 60.00, '2024-04-13 16:00:00'),
(1019, 1003, 'Deposit', 120.00, '2024-04-14 16:00:00'),
(1022, 1004, 'Deposit', 200.00, '2024-04-10 16:00:00'),
(1023, 1004, 'Withdrawal', 50.00, '2024-04-11 16:00:00'),
(1024, 1004, 'Deposit', 100.00, '2024-04-12 16:00:00'),
(1025, 1004, 'Withdrawal', 70.00, '2024-04-13 16:00:00'),
(1026, 1004, 'Deposit', 180.00, '2024-04-14 16:00:00'),
(1029, 1005, 'Deposit', 80.00, '2024-04-10 16:00:00'),
(1030, 1005, 'Withdrawal', 30.00, '2024-04-11 16:00:00'),
(1031, 1005, 'Deposit', 50.00, '2024-04-12 16:00:00'),
(1032, 1005, 'Withdrawal', 20.00, '2024-04-13 16:00:00'),
(1033, 1005, 'Deposit', 80.00, '2024-04-14 16:00:00'),
(1034, 1006, 'Deposit', 7000.00, '2024-04-17 15:23:55'),
(1035, 1006, 'Withdrawal', 8000.00, '2024-04-17 15:24:27'),
(1036, 1001, 'Deposit', 777.00, '2024-04-17 15:26:10'),
(1037, 1001, 'Withdrawal', 999.00, '2024-04-17 15:26:18'),
(1038, 1007, 'Deposit', 14000.00, '2024-04-17 15:34:10'),
(1039, 1007, 'Withdrawal', 4000.00, '2024-04-17 15:34:35'),
(1040, 1001, 'Deposit', 2.00, '2024-04-17 15:35:57'),
(1041, 1001, 'Withdrawal', 400.00, '2024-04-17 15:36:07');

-- --------------------------------------------------------

--
-- Table structure for table `user_accounts`
--

CREATE TABLE `user_accounts` (
  `UserID` varchar(20) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `PIN` varchar(6) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Contact` varchar(50) DEFAULT NULL,
  `InitialDeposit` decimal(12,2) NOT NULL DEFAULT 0.00
) ;

--
-- Dumping data for table `user_accounts`
--

INSERT INTO `user_accounts` (`UserID`, `Username`, `PIN`, `Name`, `Contact`, `InitialDeposit`) VALUES
('2024-1578-45362', 'oliviaM', '990011', 'Olivia Martinez', 'olivia@example.com', 16000.00),
('2024-2688-76284', 'fomalhaut', '112233', 'Fomalhaut', 'Galaxy', 5000.00),
('2024-3712-35456', 'emilyB', '332211', 'Emily Brown', 'emily@example.com', 12000.00),
('2024-6385-93748', 'michaelJ', '112233', 'Michael Johnson', 'michael@example.com', 15000.00),
('2024-6901-13536', 'danielL', '445566', 'Daniel Lee', 'daniel@example.com', 18000.00),
('2024-7419-34573', 'sophiaG', '778899', 'Sophia Garcia', 'sophia@example.com', 20000.00),
('2024-9400-95190', 'yggdrasil', '123456', 'Yggdrass', 'Galaxy', 5000.00);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`AccountID`),
  ADD KEY `UserID` (`UserID`);

--
-- Indexes for table `administrator`
--
ALTER TABLE `administrator`
  ADD PRIMARY KEY (`AdminID`),
  ADD UNIQUE KEY `Username` (`Username`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- Indexes for table `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`TransactionID`),
  ADD KEY `AccountID` (`AccountID`);

--
-- Indexes for table `user_accounts`
--
ALTER TABLE `user_accounts`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `accounts`
--
ALTER TABLE `accounts`
  MODIFY `AccountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1008;

--
-- AUTO_INCREMENT for table `administrator`
--
ALTER TABLE `administrator`
  MODIFY `AdminID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `transactions`
--
ALTER TABLE `transactions`
  MODIFY `TransactionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1042;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `accounts`
--
ALTER TABLE `accounts`
  ADD CONSTRAINT `accounts_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user_accounts` (`UserID`);

--
-- Constraints for table `transactions`
--
ALTER TABLE `transactions`
  ADD CONSTRAINT `transactions_ibfk_1` FOREIGN KEY (`AccountID`) REFERENCES `accounts` (`AccountID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
