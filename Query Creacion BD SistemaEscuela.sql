-- Crear la base de datos
CREATE DATABASE SistemaEscuela;

-- Conectar a la base de datos
USE SistemaEscuela;

-- Crea una tabla de Roles
CREATE TABLE Rol (
  Id INT PRIMARY KEY,
  Nombre NVARCHAR(100),
	Descripcion NVARCHAR(max),
	Bhabilitado bit,
  Vista NVARCHAR(100)
);

-- Crear una tabla de Matriculas
CREATE TABLE Matriculas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdMateria INT,
    IdAlumno INT,
    FOREIGN KEY (IdMateria) REFERENCES Materias(Id),
    FOREIGN KEY (IdAlumno) REFERENCES Alumnos(Id)
);

-- Crea una tabla de Usuarios
CREATE TABLE Usuarios (
  Id INT IDENTITY(1,1) PRIMARY KEY,--  
	IdRol INT,
	email NVARCHAR(100),
	pass NVARCHAR(300),
	Bhabilitado bit
);

-- Crear una tabla de alumnos
CREATE TABLE Alumnos (
    Id INT PRIMARY KEY,
	IdRol INT,
    Nombre NVARCHAR(100),
	ApellidoPaterno NVARCHAR(100),
	ApellidoMaterno NVARCHAR(100),
	Bhabilitado bit
);

-- Crear una tabla de profesores
CREATE TABLE Profesor (
  Id INT PRIMARY KEY,
	IdRol INT,
  Nombre NVARCHAR(100),
	ApellidoPaterno NVARCHAR(100),
	ApellidoMaterno NVARCHAR(100),
	Bhabilitado bit
);

-- Crear una tabla de materias
CREATE TABLE Materias (
  Id INT PRIMARY KEY,
  NombreMateria NVARCHAR(100),
	IdProfesor INT,
	Bhabilitado bit
);

-- Crear una tabla de calificaciones
CREATE TABLE Calificaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MateriaID INT,
    EstudianteID INT,
    Calificacion INT,
	  Bhabilitado bit
);

INSERT INTO Rol (Id,Nombre,Descripcion,Bhabilitado) 
values 
(1,'Admin','Puede crear usuarios de profesor/alumno',1,'Admin'),

(2,'Profesor','Tiene la capacidad de ver materias, ver alumnos, calificar alumnos y visualizar aprobados y reprobados.',1,'Profesor'),

(3,'Alumno','Tiene la capacidad de ver, asignarse y cancelarse materias a sí mismo.',1,'Alumno');

INSERT INTO Materias (Id, NombreMateria, Bhabilitado) 
values 
    (1, 'Sistemas Digitales', 1),
    (2, 'Electrónica', 1),
    (3, 'Interfaces Graficas', 1),
    (4, 'Bases de Datos', 1),
    (5, 'Programacion Web', 1),
    (6, 'Estructura de Datos', 1),
    (7, 'Diseño de Algoritmos', 1);   

INSERT INTO Profesor (Id, IdRol, Nombre, ApellidoPaterno, ApellidoMaterno, Bhabilitado)
VALUES 
(1,2,'Julio Cesar', 'Avila','Alcaraz',1),
(2,2,'Antonio','Rodriguez','Vazquez',1),
(3,2,'Elvia','Gomez','Barrientos',1);

INSERT INTO Alumnos(Id, IdRol, Nombre, ApellidoPaterno, ApellidoMaterno, Bhabilitado)
VALUES 
(1,3,'Hector', 'Garcia','Prado',1),
(2,3,'Maria Fernanda', 'Gomez','Perez',1),
(3,3,'Alejandro','Rodriguez','Torres',1),
(4,3,'Juan Carlos','Lopez','Mendoza',1),
(5,3,'Ana Paula','Rodriguez ','Mendoza',1),
(6,3,'Carlos Eduardo','Fernandez ','Silva',1),
(7,3,'Andrés Felipe','Torres','Ruiz',1),
(8,3,'Sofia','Garcia','Martinez',1);

INSERT INTO Usuarios (Id, IdRol,email,pass,Bhabilitado)
values(1,1,'admin@admin.com','adminpass',1)

select * from Usuarios
select * from Calificaciones
select * from Matriculas
select * from Materias
select * from Alumnos
select * from Profesor
select * from Rol






















