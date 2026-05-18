CREATE DATABASE db_Zooologicos;
GO

USE db_Zooologicos;
GO

-- ================= ZOOLOGICOS =================
CREATE TABLE [Zoologicos] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL,
    [Ubicacion] NVARCHAR(200) NOT NULL
);

-- ================= HABITATS =================
CREATE TABLE [Habitats] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL,
    [Tipo] NVARCHAR(50) NOT NULL,
    [CapacidadMaxima] INT NOT NULL,
    [Estado] NVARCHAR(50) NOT NULL,

    [ZoologicoId] INT NOT NULL,
    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= JAULAS =================
CREATE TABLE [Jaulas] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [FechaCompra] DATETIME2 NOT NULL,

    [HabitatId] INT NOT NULL,
    FOREIGN KEY (HabitatId) REFERENCES [Habitats]([Id])
);

-- ================= ESPECIES =================
CREATE TABLE [Especies] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Descripcion] NVARCHAR(200) NOT NULL,
    [Tipo] NVARCHAR(50) NOT NULL
);

-- ================= Alimentaciones =================
CREATE TABLE [Alimentaciones] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL,
    [Naturaleza] NVARCHAR(50) NOT NULL,
    [FechaNacimiento] DATETIME2 NOT NULL,
    [Alimentacion] NVARCHAR(100) NULL,

    [EspecieId] INT NOT NULL,
    [JaulaId] INT NOT NULL,

    FOREIGN KEY (EspecieId) REFERENCES [Especies]([Id]),
    FOREIGN KEY (JaulaId) REFERENCES [Jaulas]([Id])
);

-- ================= ENFERMEDADES =================
CREATE TABLE [Enfermedades] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL,
    [Descripcion] NVARCHAR(200) NOT NULL
);

-- ================= EMPLEADOS =================
CREATE TABLE [Empleados] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL,
    [Cedula] NVARCHAR(50) NOT NULL,
    [Telefono] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Salario] DECIMAL(10,2) NOT NULL,
    [FechaContratacion] DATETIME2 NOT NULL,

    [ZoologicoId] INT NOT NULL,
    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= VETERINARIOS =================
CREATE TABLE [Veterinarios] (
    [Id] INT PRIMARY KEY,
    [Especialidad] NVARCHAR(100) NOT NULL,
    [AñosExperiencia] INT NOT NULL,

    FOREIGN KEY (Id) REFERENCES [Empleados]([Id])
);

-- ================= GERENTES =================
CREATE TABLE [Gerentes] (
    [Id] INT PRIMARY KEY,
    FOREIGN KEY (Id) REFERENCES [Empleados]([Id])
);

-- ================= CUIDADORES =================
CREATE TABLE [CuidadorAlimentaciones] (
    [Id] INT PRIMARY KEY,
    [EspecieId] INT NULL,
    [Turno] NVARCHAR(50) NOT NULL,
    [AñosExperiencia] INT NOT NULL,

    FOREIGN KEY (Id) REFERENCES [Empleados]([Id]),
    FOREIGN KEY (EspecieId) REFERENCES [Especies]([Id])
);

-- ================= PERSONAL ASEO =================
CREATE TABLE [PersonalAseo] (
    [Id] INT PRIMARY KEY,
    [ZonaAsignada] NVARCHAR(100) NOT NULL,
    [Turno] NVARCHAR(50) NOT NULL,
    [ProductosAsignados] NVARCHAR(200) NOT NULL,

    FOREIGN KEY (Id) REFERENCES [Empleados]([Id])
);

-- ================= ENTRENADORES =================
CREATE TABLE [Entrenadores] (
    [Id] INT PRIMARY KEY,
    [Especialidad] NVARCHAR(100) NOT NULL,
    [TipoEntrenamiento] NVARCHAR(100) NOT NULL,

    FOREIGN KEY (Id) REFERENCES [Empleados]([Id])
);

-- ================= DIAGNOSTICOS =================
CREATE TABLE [Diagnosticos] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [FechaDiagnostico] DATETIME2 NOT NULL,
    [FechaCura] DATETIME2 NULL,

    [AnimalId] INT NOT NULL,
    [EnfermedadId] INT NOT NULL,
    [VeterinarioId] INT NOT NULL,

    FOREIGN KEY (AnimalId) REFERENCES [Alimentaciones]([Id]),
    FOREIGN KEY (EnfermedadId) REFERENCES [Enfermedades]([Id]),
    FOREIGN KEY (VeterinarioId) REFERENCES [Veterinarios]([Id])
);

-- ================= HISTORIALES =================
CREATE TABLE [HistorialesMedicos] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [AnimalId] INT NOT NULL,

    [Tratamiento] NVARCHAR(200) NOT NULL,
    [Medicamento] NVARCHAR(100) NOT NULL,
    [Dosis] NVARCHAR(50) NOT NULL,

    [FechaControl] DATETIME2 NOT NULL,
    [EstadoActual] NVARCHAR(100) NOT NULL,

    FOREIGN KEY (AnimalId) REFERENCES [Alimentaciones]([Id])
);

-- ================= VACUNACIONES =================
CREATE TABLE [Vacunaciones](
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [AnimalId] INT NOT NULL,

    [NombreVacuna] NVARCHAR(100) NOT NULL,
    [Dosis] NVARCHAR(50) NOT NULL,

    [FechaAplicacion] DATETIME2 NOT NULL,
    [FechaProximaDosis] DATETIME2 NULL,

    [VeterinarioId] INT NOT NULL,

    FOREIGN KEY (AnimalId) REFERENCES [Alimentaciones]([Id]),
    FOREIGN KEY (VeterinarioId) REFERENCES [Veterinarios]([Id])
);

-- ================= Alimentaciones =================
CREATE TABLE [Alimentaciones] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [AnimalId] INT NOT NULL,

    [TipoDieta] NVARCHAR(100) NOT NULL,
    [CantidadDiaria] DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (AnimalId) REFERENCES [Alimentaciones]([Id])
);

-- ================= INVENTARIOS =================
CREATE TABLE [Inventarios] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [ZoologicoId] INT NOT NULL,

    [NombreItem] NVARCHAR(100) NOT NULL,
    [TipoItem] NVARCHAR(50) NOT NULL,
    [CantidadDisponible] DECIMAL(10,2) NOT NULL,
    [FechaVencimiento] DATETIME2 NULL,

    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= VISITANTES =================
CREATE TABLE [Visitantes] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL,
    [TipoDocumento] NVARCHAR(50) NOT NULL,
    [NumeroDocumento] NVARCHAR(50) NOT NULL
);

-- ================= ENTRADAS =================
CREATE TABLE [Entradas] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [VisitanteId] INT NOT NULL,

    [FechaVisita] DATETIME2 NOT NULL,
    [TipoEntrada] NVARCHAR(50) NOT NULL,
    [ValorPagado] DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (VisitanteId) REFERENCES [Visitantes]([Id])
);

-- ================= ZONAS PUBLICAS =================
CREATE TABLE [ZonasPublicas] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre] NVARCHAR(100) NOT NULL,
    [Tipo] NVARCHAR(50) NOT NULL,

    [ZoologicoId] INT NOT NULL,
    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= AREAS =================
CREATE TABLE [Areas] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [HabitatId] INT NULL,
    [JaulaId] INT NULL,
    [ZonaPublicaId] INT NULL,

    FOREIGN KEY (HabitatId) REFERENCES [Habitats]([Id]),
    FOREIGN KEY (JaulaId) REFERENCES [Jaulas]([Id]),
    FOREIGN KEY (ZonaPublicaId) REFERENCES [ZonasPublicas]([Id])
);

-- ================= MANTENIMIENTOS =================
CREATE TABLE [Mantenimientos] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [AreaId] INT NOT NULL,

    [FechaReporte] DATETIME2 NOT NULL,
    [FechaProgramada] DATETIME2 NOT NULL,
    [Estado] NVARCHAR(50) NOT NULL,

    [EmpleadoResponsableId] INT NOT NULL,

    FOREIGN KEY (AreaId) REFERENCES [Areas]([Id]),
    FOREIGN KEY (EmpleadoResponsableId) REFERENCES [Empleados]([Id])
);

-- ================= ZOOLOGICO =================
INSERT INTO Zoologicos (Nombre, Ubicacion)
VALUES ('Zoo Medellín', 'Antioquia');

-- ================= ESPECIES =================
INSERT INTO Especies (Descripcion, Tipo)
VALUES 
('León africano', 'Mamífero'),
('Tigre de bengala', 'Mamífero'),
('Guacamaya', 'Ave');

-- ================= ENFERMEDADES =================
INSERT INTO Enfermedades (Nombre, Descripcion)
VALUES 
('Gripe', 'Infección respiratoria'),
('Parásitos', 'Infección intestinal');

-- ================= VISITANTES =================
INSERT INTO Visitantes (Nombre, TipoDocumento, NumeroDocumento)
VALUES 
('Juan Perez', 'CC', '111'),
('Maria Lopez', 'CC', '222');

-- ================= HABITATS =================
INSERT INTO Habitats (Nombre, Tipo, CapacidadMaxima, Estado, ZoologicoId)
VALUES 
('Sabana', 'Terrestre', 10, 'Activo', 1),
('Selva', 'Terrestre', 8, 'Activo', 1);

-- ================= ZONAS PUBLICAS =================
INSERT INTO ZonasPublicas (Nombre, Tipo, ZoologicoId)
VALUES 
('Zona Picnic', 'Recreativa', 1);

-- ================= INVENTARIOS =================
INSERT INTO Inventarios (ZoologicoId, NombreItem, TipoItem, CantidadDisponible, FechaVencimiento)
VALUES 
(1, 'Carne', 'Alimento', 100, NULL);

-- ================= JAULAS =================
INSERT INTO Jaulas (FechaCompra, HabitatId)
VALUES 
(GETDATE(), 1),
(GETDATE(), 2);

-- ================= Alimentaciones =================
INSERT INTO Alimentaciones (Nombre, Naturaleza, FechaNacimiento, Alimentacion, EspecieId, JaulaId)
VALUES 
('Simba', 'Salvaje', '2020-01-01', 'Carnívoro', 1, 1),
('Shere Khan', 'Salvaje', '2019-05-10', 'Carnívoro', 2, 2),
('Lola', 'Domesticado', '2021-03-15', 'Frutas', 3, 2);

-- ================= EMPLEADOS =================
INSERT INTO Empleados (Nombre, Cedula, Telefono, Email, Salario, FechaContratacion, ZoologicoId)
VALUES 
('Carlos Vet', '100', '3001', 'vet@mail.com', 3000, GETDATE(), 1),
('Ana Gerente', '101', '3002', 'gerente@mail.com', 5000, GETDATE(), 1),
('Luis Cuidador', '102', '3003', 'cuidador@mail.com', 2000, GETDATE(), 1),
('Pedro Aseo', '103', '3004', 'aseo@mail.com', 1500, GETDATE(), 1),
('Sofia Entrenadora', '104', '3005', 'trainer@mail.com', 2500, GETDATE(), 1);

-- ================= ROLES =================

-- Veterinario (Id = 1)
INSERT INTO Veterinarios (Id, Especialidad, AñosExperiencia)
VALUES (1, 'Felinos', 5);

-- Gerente (Id = 2)
INSERT INTO Gerentes (Id)
VALUES (2);

-- Cuidador (Id = 3)
INSERT INTO CuidadorAlimentaciones (Id, EspecieId, Turno, AñosExperiencia)
VALUES (3, 1, 'Día', 3);

-- Personal Aseo (Id = 4)
INSERT INTO PersonalAseo (Id, ZonaAsignada, Turno, ProductosAsignados)
VALUES (4, 'Zona Sabana', 'Noche', 'Desinfectante');

-- Entrenador (Id = 5)
INSERT INTO Entrenadores (Id, Especialidad, TipoEntrenamiento)
VALUES (5, 'Aves', 'Conductual');

-- ================= DIAGNOSTICOS =================
INSERT INTO Diagnosticos (FechaDiagnostico, FechaCura, AnimalId, EnfermedadId, VeterinarioId)
VALUES 
(GETDATE(), NULL, 1, 1, 1);

-- ================= HISTORIALES =================
INSERT INTO HistorialesMedicos (AnimalId, Tratamiento, Medicamento, Dosis, FechaControl, EstadoActual)
VALUES 
(1, 'Reposo', 'Ibuprofeno', '2 veces al día', GETDATE(), 'En tratamiento');

-- ================= VACUNACIONES =================
INSERT INTO Vacunaciones (AnimalId, NombreVacuna, Dosis, FechaAplicacion, FechaProximaDosis, VeterinarioId)
VALUES 
(1, 'Vacuna A', '1ml', GETDATE(), NULL, 1);

-- ================= Alimentaciones =================
INSERT INTO Alimentaciones (AnimalId, TipoDieta, CantidadDiaria)
VALUES 
(1, 'Carne', 5);

-- ================= ENTRADAS =================
INSERT INTO Entradas (VisitanteId, FechaVisita, TipoEntrada, ValorPagado)
VALUES 
(1, GETDATE(), 'Adulto', 20);

-- ================= AREAS =================
INSERT INTO Areas (HabitatId, JaulaId, ZonaPublicaId)
VALUES 
(1, NULL, NULL),
(NULL, 1, NULL),
(NULL, NULL, 1);

-- ================= MANTENIMIENTOS =================
INSERT INTO Mantenimientos (AreaId, FechaReporte, FechaProgramada, Estado, EmpleadoResponsableId)
VALUES 
(1, GETDATE(), GETDATE(), 'Pendiente', 4);