-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Apr 18, 2022 at 01:55 PM
-- Server version: 5.7.24
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `classic_student`
--

-- --------------------------------------------------------

--
-- Table structure for table `answer`
--

CREATE TABLE `answer` (
  `answerId` int(16) NOT NULL,
  `questionId` int(16) NOT NULL,
  `answerText` varchar(150) NOT NULL,
  `correct` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `answer`
--

INSERT INTO `answer` (`answerId`, `questionId`, `answerText`, `correct`) VALUES
(45, 18, 'USA', ''),
(46, 18, 'Russia', 'true'),
(47, 18, 'Italy', ''),
(48, 18, 'Spain', ''),
(49, 19, 'True', ''),
(50, 19, 'False', 'true'),
(53, 20, 'Red, Green, Blue', 'true'),
(54, 20, 'Purple, Green, Brown', ''),
(55, 20, 'Red, Yellow, Blue', ''),
(57, 21, 'Yes', ''),
(58, 21, 'No', 'true'),
(61, 22, 'Yes', 'true'),
(62, 22, 'No', ''),
(65, 23, '1942', ''),
(66, 23, '1941', ''),
(67, 23, '1940', ''),
(68, 23, '1939', 'true'),
(69, 19, '', 'true'),
(70, 19, '', ''),
(71, 20, '', ''),
(72, 21, '', ''),
(73, 21, '', 'true'),
(74, 22, '', 'true'),
(75, 22, '', '');

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `questionId` int(16) NOT NULL,
  `testId` int(16) NOT NULL,
  `answersNumber` int(16) NOT NULL,
  `points` int(16) NOT NULL,
  `questionText` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `questions`
--

INSERT INTO `questions` (`questionId`, `testId`, `answersNumber`, `points`, `questionText`) VALUES
(18, 17, 60, 10, 'The biggest country on the planet is?'),
(19, 17, 60, 10, '15+35=60'),
(20, 17, 58, 10, 'The three main colours are:'),
(21, 17, 57, 10, 'Can water stay liquid below zero degrees Celsius?'),
(22, 17, 55, 10, 'Charles Darwin introduced the idea of natural selection.'),
(23, 17, 53, 10, 'When World War 2 started?');

-- --------------------------------------------------------

--
-- Table structure for table `scores`
--

CREATE TABLE `scores` (
  `scoreId` int(11) NOT NULL,
  `studentName` varchar(100) NOT NULL,
  `testId` int(11) NOT NULL,
  `points` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `scores`
--

INSERT INTO `scores` (`scoreId`, `studentName`, `testId`, `points`) VALUES
(1, 'Hristo Stoev', 17, 10),
(2, 'Alexandra Stoilove', 17, 10),
(3, 'Ivan Ivanov', 17, 0),
(4, 'George Jhon', 17, 10);

-- --------------------------------------------------------

--
-- Table structure for table `tests`
--

CREATE TABLE `tests` (
  `testId` int(16) NOT NULL,
  `userId` int(16) NOT NULL,
  `numberQuestions` int(16) NOT NULL,
  `title` varchar(40) NOT NULL,
  `totalPoints` int(16) NOT NULL,
  `timer` int(16) NOT NULL,
  `code` int(7) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tests`
--

INSERT INTO `tests` (`testId`, `userId`, `numberQuestions`, `title`, `totalPoints`, `timer`, `code`) VALUES
(17, 44, 6, 'Tutorial test', 10, 20, 1000000);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `userID` int(16) NOT NULL,
  `email` varchar(40) NOT NULL,
  `password` varchar(70) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userID`, `email`, `password`) VALUES
(44, 'admin@admin.com', '$2y$10$U5Sc59MJvB7mYKoJ800HNetlxWUB2.9CiLBQiRyQclm2TQQBunHOm'),
(45, 'aaa@aaa.com', '$2y$10$7tDEdfQqwmvMD9NwARewEOE11iOo.akoiiBMLOHyGLQZ3NcPRUNT2'),
(46, 'aaaa@aaa.com', '$2y$10$cCypZc6QtlJbKSbmQHp8juMqzyfbhcZoHZekJPNJRFpihzAlhiibC'),
(47, 'icak@gmai.com', '$2y$10$R31HJg844csZUctM.T6ts.1T6te1rsL52NS6LNI7/CoepyceMQpzu'),
(48, '', '$2y$10$lFWiVvPG7DfIFMgxqHDVNeq.e0JhHzeivRxg9X5CL.RTa1q4vYDuy');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `answer`
--
ALTER TABLE `answer`
  ADD PRIMARY KEY (`answerId`),
  ADD KEY `questionId` (`questionId`);

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`questionId`),
  ADD KEY `testId` (`testId`);

--
-- Indexes for table `scores`
--
ALTER TABLE `scores`
  ADD PRIMARY KEY (`scoreId`),
  ADD KEY `testId` (`testId`);

--
-- Indexes for table `tests`
--
ALTER TABLE `tests`
  ADD PRIMARY KEY (`testId`),
  ADD UNIQUE KEY `code` (`code`),
  ADD KEY `userId` (`userId`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userID`),
  ADD UNIQUE KEY `Email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `answer`
--
ALTER TABLE `answer`
  MODIFY `answerId` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=76;

--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `questionId` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `scores`
--
ALTER TABLE `scores`
  MODIFY `scoreId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tests`
--
ALTER TABLE `tests`
  MODIFY `testId` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `userID` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=49;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `answer`
--
ALTER TABLE `answer`
  ADD CONSTRAINT `answer_ibfk_1` FOREIGN KEY (`questionId`) REFERENCES `questions` (`questionId`);

--
-- Constraints for table `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `questions_ibfk_1` FOREIGN KEY (`testId`) REFERENCES `tests` (`testId`);

--
-- Constraints for table `scores`
--
ALTER TABLE `scores`
  ADD CONSTRAINT `scores_ibfk_1` FOREIGN KEY (`testId`) REFERENCES `tests` (`testId`);

--
-- Constraints for table `tests`
--
ALTER TABLE `tests`
  ADD CONSTRAINT `tests_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `users` (`userID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
