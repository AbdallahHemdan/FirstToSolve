--drop table Blog
--create table Blog(
--BlogID int not null unique,
--BlogTitle varchar(50) not null,
--BlogWriter int not null,
--GroupID int,
--BlogContent nvarchar(max) not null,
--Primary key(BlogID),
--Foreign Key(GroupID) references _Group(GroupID),
--Foreign Key(BlogWriter) references _User(UserID),
--)
--CREATE PROCEDURE InsertBlog
--	@BlogID int,
--	@BlogTitle varchar(50),
--	@BlogWriter int,
--	@GroupID int,
--	@BlogContent nvarchar(max)
--AS
--BEGIN
--	insert into Blog values(@BlogID, @BlogTitle, @BlogWriter, @GroupID, @BlogContent);
--END

--CREATE PROCEDURE GetAllBlogs 
--AS
--BEGIN
--	select * from Blog
--END
--CREATE PROCEDURE GetMyBlogs 
--	@UserID int
--AS
--BEGIN
--	select * from Blog where BlogWriter = @UserID;
--END

insert into Blog values(1, '2 Hours Contest vs 3 Hours Contest', 1, 1, 'I have been with codeforces for a long long time. It has been always my thought that why codeforces rounds are only 2 hours (usually). The things that I am sharing right now are just my opinions.

I think a CF contest''s difficulty is chosen by looking at the very topmost population. Maybe like 5% of the population. They should complete or almost complete the problemset within the contest time. Well, that''s fair I guess. But from the top, 50% or even maybe from the top 25% would feel that they could solve another one if there was more time. That''s true for everyone else too I guess.

Let''s say a usual codeforces round is now 3 hours. What are the pros? Most of the contestants will get 1 extra hour to think with 1 or 2 more problems that they don''t get to solve within 2 hours. Speedforces effect should be reduced at least a bit. A slow but knowledgeable contestant might end up higher on some fast contestant who used to be on top of him most of the time. Doesn''t everyone want to be separated by problem solve count rather than the speed! And 2 hours of the contest does not let you much time to recover. 3 hours of contest would let you much time to recover and could enforce different strategies. Some problem requiring 90-100 minutes to solve for a certain person is almost impossible to solve within the 2 hours contest time.

What are the cons? The topmost guys would finish the contest very early. But it also happens in 2 hours contest too. Some guys finish X problems and they still had 30-40 minutes which is very low to solve the next one. Another problem is they have to sit through the whole contest because of hacks may be. Some people might be unable to find 3 straight hours of free slot. And is it a problem that 3 hours of contest would put heavy pressure on the server (maybe in terms of cost too)? I don''t think this one is an issue though. One problem could be someone might find it better with 4 hours rather than 3 hours, some might find it better with 5 hours of the individual contest. It''s a never-ending debate about what is suitable.

There could be many other pros and cons. But my point is maybe 2 hours is too low for a contest. I think most of the contestants put an extra hour at least on the almost solved one/thinking for a long time one on the pen-paper or in the comment section may be. Putting that hour in the contest itself might be more interesting.

');

CREATE PROCEDURE GetABlog 
	@BlogID int
AS
BEGIN
	select * from Blog where BlogID = @BlogID;
END

CREATE PROCEDURE Count_Blogs 
AS
BEGIN
	Select count(*) from Blog
END