use DBCMS
go

create table Department
(
DepartmentId int identity primary key,
DepartmentName varchar(100) not null unique
)

create table Designation
(
DesignationId int identity primary key,
DesignationName varchar(100) not null,
)

create table Subjects
(
SubjectId int identity primary key,
SubjectName varchar(100) not null,
DepartmentId int not null,
foreign key (DepartmentId) references Department(DepartmentId) 
)

create table StudentProfile
(
StudentProfileId int identity primary key,
StudentName varchar(100) not null,
Gender bit not null,
StudentPhone int unique,
StudentEmail varchar(250) not null unique,
[Password] varchar(max) not null,
FatherName varchar(100) not null,
FatherPhone int,
MotherName varchar(100) not null,
MotherPhone int,
[Address] varchar(max) not null,
DepartmentId int,
foreign key(DepartmentId) references Department(DepartmentId)
)

create table TeacherProfile
(
TeacherProfileId int identity primary key,
TeacherName varchar(100) not null,
Gender bit not null,
Degree varchar(250) not null,
DesignationId int not null,
TeacherEmail varchar(250) not null unique,
[TeacherPassword] varchar(max) not null,
TeacherPhone int not null unique,
TeacherAddress varchar(max) not null,
JoiningDate date not null,
DepartmentId int not null,
SubjectId int not null,
foreign key(DepartmentId) references Department(DepartmentId),
foreign key(SubjectId) references Subjects(SubjectId)
)

create table Result
(
ResultId int identity primary key,
ExamType varchar(100) not null,
StudentProfileId int not null,
DepartmentId int not null,
SubjectId int not null,
Marks int not null,
foreign key(StudentProfileId) references StudentProfile(StudentProfileId),
foreign key(DepartmentId) references Department(DepartmentId),
foreign key(SubjectId) references Subjects(SubjectId)
)
