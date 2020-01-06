----insert into Contest(ContestID, ContestName, ContestDate, ContestLength, ContestWriter)
----			Values(100, 'CMP Contest 2022', convert(DATETIME, '2019-5-22'), 3, 1);

----insert into Contest(ContestID, ContestName, ContestDate, ContestLength, ContestWriter)
----			Values(101, 'CMP Contest 2021', convert(DATETIME, '2018-5-22'), 5, 1);

----insert into Contest(ContestID, ContestName, ContestDate, ContestLength, ContestWriter)
----			Values(102, 'CMP Contest 2020', convert(DATETIME, '2017-5-22'), 8, 1);

----insert into Contest(ContestID, ContestName, ContestDate, ContestLength, ContestWriter)
----			Values(103, 'CMP Contest 2019', convert(DATETIME, '2016-5-22'), 2, 1);

----insert into Contest(ContestID, ContestName, ContestDate, ContestLength, ContestWriter)
----			Values(104, 'CMP Contest 2018', convert(DATETIME, '2015-5-22'), 5, 1);

--ALTER TABLE Problem
--ADD ProblemID varchar(50) not null;
--ALTER TABLE Problem
--add CONSTRAINT pk_Problem_ID primary key(ProblemID);

--ALTER TABLE Submission
--ADD ProblemID varchar(50) not null;
--ALTER TABLE Submission
--add CONSTRAINT fK_Problem_ID foreign key(ProblemID) references Problem(ProblemID);


insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 800, '100A', 'https://codeforces.s3.amazonaws.com/100A.pdf', 'Carpeting the Room', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 1000, '100B', 'https://codeforces.s3.amazonaws.com/100B.pdf', 'Friendly Numbers', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 1200, '100C', 'https://codeforces.s3.amazonaws.com/100C.pdf', 'A+B', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 1400, '100D', 'https://codeforces.s3.amazonaws.com/100D.pdf', 'World of Mouth', 'Strings', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 1600, '100E', 'https://codeforces.s3.amazonaws.com/100E.pdf', 'Lamps in a Line', 'Math', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 1800, '100F', 'https://codeforces.s3.amazonaws.com/100F.pdf', 'Polynom', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 2000, '100G', 'https://codeforces.s3.amazonaws.com/100G.pdf', 'Name the album', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 2200, '100H', 'https://codeforces.s3.amazonaws.com/100H.pdf', 'Battleship', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 2400, '100I', 'https://codeforces.s3.amazonaws.com/100I.pdf', 'Rotation', 'Geometry', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(100, 2600, '100J', 'https://codeforces.s3.amazonaws.com/100J.pdf', 'Interval Coloring', 'Greedy', 1);

insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(101, 800, '101A', 'https://codeforces.s3.amazonaws.com/101A.pdf', 'Homework', 'Greedy', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(101, 1000, '101B', 'https://codeforces.s3.amazonaws.com/101B.pdf', 'Buses', 'Binary Search', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(101, 1200, '101C', 'https://codeforces.s3.amazonaws.com/101C.pdf', 'Vectors', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(101, 1400, '101D', 'https://codeforces.s3.amazonaws.com/101D.pdf', 'Castle', 'Dynamic Programming', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(101, 1600, '101E', 'https://codeforces.s3.amazonaws.com/101E.pdf', 'Candies and Stones', 'Divide and Conquer', 1);

insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(102, 800, '102A', 'https://codeforces.s3.amazonaws.com/102A.pdf', 'Clothes', 'Brute Force', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(102, 1000, '102B', 'https://codeforces.s3.amazonaws.com/102B.pdf', 'Sum of Digits', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(102, 1200, '102C', 'https://codeforces.s3.amazonaws.com/102C.pdf', 'Homework', 'Greedy', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(102, 1400, '102D', 'https://codeforces.s3.amazonaws.com/102D.pdf', 'Buses', 'Data Structures', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(102, 1600, '102E', 'https://codeforces.s3.amazonaws.com/102E.pdf', 'Vectors', 'Implementation', 1);

insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(103, 800, '103A', 'https://codeforces.s3.amazonaws.com/103A.pdf', 'Testing Pants for Sadness', 'Greedy', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(103, 1000, '103B', 'https://codeforces.s3.amazonaws.com/103B.pdf', 'Cthulhu', 'DFS and Similar', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(103, 1200, '103C', 'https://codeforces.s3.amazonaws.com/103C.pdf', 'Russian Roulette', 'Constructive Algorithm', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(103, 1400, '103D', 'https://codeforces.s3.amazonaws.com/103D.pdf', 'Time to Raid Cowavans', 'Brute Force', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(103, 1600, '103E', 'https://codeforces.s3.amazonaws.com/103E.pdf', 'Buying Sets', 'Graphs', 1);

insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(104, 800, '104A', 'https://codeforces.s3.amazonaws.com/104A.pdf', 'Blackjack', 'Implementation', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(104, 1000, '104B', 'https://codeforces.s3.amazonaws.com/104B.pdf', 'Testing Pants for Sadness', 'Math', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(104, 1200, '104C', 'https://codeforces.s3.amazonaws.com/104C.pdf', 'Cthulhu', 'DSU', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(104, 1400, '104D', 'https://codeforces.s3.amazonaws.com/104D.pdf', 'Russian Roulette', 'Math', 1);
insert into Problem(ProblemContest, ProblemDifficulty, ProblemID, ProblemLink, ProblemName, ProblemTopic, ProblemWriter)
			Values(104, 1600, '104E', 'https://codeforces.s3.amazonaws.com/104E.pdf', 'Time to Raid Cowavans', 'Implementation', 1);