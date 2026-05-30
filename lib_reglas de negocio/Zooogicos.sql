CREATE DATABASE db_Zoologicos;
GO

USE db_Zoologicos;
GO

-- ================= ZOOLOGICOS =================
CREATE TABLE [Zoologicos] (
    [Id]        INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre]    NVARCHAR(100)   NOT NULL,
    [Ubicacion] NVARCHAR(200)   NOT NULL
);

-- ================= HABITATS =================
CREATE TABLE [Habitats] (
    [Id]              INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre]          NVARCHAR(100)  NOT NULL,
    [Tipo]            NVARCHAR(50)   NOT NULL,
    [CapacidadMaxima] INT            NOT NULL,
    [Estado]          NVARCHAR(50)   NOT NULL,

    [ZoologicoId] INT NOT NULL,
    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= JAULAS =================
CREATE TABLE [Jaulas] (
    [Id]          INT PRIMARY KEY IDENTITY(1, 1),
    [FechaCompra] DATETIME2 NOT NULL,

    [HabitatId] INT NOT NULL,
    FOREIGN KEY (HabitatId) REFERENCES [Habitats]([Id])
);

-- ================= ESPECIES =================
CREATE TABLE [Especies] (
    [Id]          INT PRIMARY KEY IDENTITY(1, 1),
    [Descripcion] NVARCHAR(200) NOT NULL,
    [Tipo]        NVARCHAR(50)  NOT NULL
);

-- ================= ANIMALES =================
CREATE TABLE [Animales] (
    [Id]             INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre]         NVARCHAR(100)  NOT NULL,
    [Naturaleza]     NVARCHAR(50)   NOT NULL,
    [FechaNacimiento] DATETIME2     NOT NULL,
    [Alimentacion]   NVARCHAR(100)  NULL,
    [Genero]         NVARCHAR(10)   NOT NULL CHECK ([Genero] IN ('Macho', 'Hembra')),
    [EspecieId] INT NOT NULL,
    [JaulaId]   INT NOT NULL,

    FOREIGN KEY (EspecieId) REFERENCES [Especies]([Id]),
    FOREIGN KEY (JaulaId)   REFERENCES [Jaulas]([Id])
);
GO

-- ================= REPRODUCCIONES =================
CREATE TABLE [Reproducciones] (
    [Id]                  INT PRIMARY KEY IDENTITY(1,1),
    [AnimalMadreId]       INT           NOT NULL,
    [AnimalPadreId]       INT           NOT NULL,
    [FechaAppariamiento]  DATETIME2     NOT NULL,
    [FechaNacimiento]     DATETIME2     NULL,
    [CantidadCrias]       INT           NOT NULL DEFAULT 0,
    [Metodo]              NVARCHAR(50)  NOT NULL
                              CHECK ([Metodo] IN ('Natural', 'Asistida', 'In vitro')),
    [Estado]              NVARCHAR(50)  NOT NULL
                              CHECK ([Estado] IN ('En proceso', 'Exitosa', 'Fallida')),
    [Observaciones]       NVARCHAR(500) NULL,

    CONSTRAINT FK_Reproducciones_Madre
        FOREIGN KEY ([AnimalMadreId]) REFERENCES [Animales]([Id]),
    CONSTRAINT FK_Reproducciones_Padre
        FOREIGN KEY ([AnimalPadreId]) REFERENCES [Animales]([Id])
);
GO

-- ================= TRIGGER REPRODUCCIONES =================
CREATE TRIGGER TR_Reproducciones_ValidarGenero
ON [Reproducciones]
AFTER INSERT, UPDATE
AS
BEGIN
    -- Validar que AnimalMadreId sea Hembra
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Animales a ON a.Id = i.AnimalMadreId
        WHERE a.Genero != 'Hembra'
    )
    BEGIN
        RAISERROR('El AnimalMadreId debe ser de género Hembra.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Validar que AnimalPadreId sea Macho
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Animales a ON a.Id = i.AnimalPadreId
        WHERE a.Genero != 'Macho'
    )
    BEGIN
        RAISERROR('El AnimalPadreId debe ser de género Macho.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- ================= ENFERMEDADES =================
CREATE TABLE [Enfermedades] (
    [Id]          INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre]      NVARCHAR(100) NOT NULL,
    [Descripcion] NVARCHAR(200) NOT NULL
);

-- ================= EMPLEADOS =================
CREATE TABLE [Empleados] (
    [Id]                INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre]            NVARCHAR(100)   NOT NULL,
    [Cedula]            NVARCHAR(50)    NOT NULL,
    [Telefono]          NVARCHAR(50)    NOT NULL,
    [Email]             NVARCHAR(100)   NOT NULL,
    [Salario]           DECIMAL(10,2)   NOT NULL,
    [FechaContratacion] DATETIME2       NOT NULL,

    [ZoologicoId] INT NOT NULL,
    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= VETERINARIOS =================
CREATE TABLE [Veterinarios] (
    [Id]               INT PRIMARY KEY IDENTITY (1, 1),
    [Especialidad]     NVARCHAR(100) NOT NULL,
    [AñosExperiencia]  INT           NOT NULL,
	[IdEmpleado]  INT NOT NULL,

    FOREIGN KEY (IdEmpleado) REFERENCES [Empleados]([Id])
);

-- ================= GERENTES =================
CREATE TABLE [Gerentes] (
    [Id] INT PRIMARY KEY IDENTITY (1, 1),
	[IdEmpleado]  INT NOT NULL,
    FOREIGN KEY (IdEmpleado) REFERENCES [Empleados]([Id])
);

-- ================= CUIDADORES =================
CREATE TABLE [CuidadorAnimales] (
    [Id] INT PRIMARY KEY IDENTITY(1, 1),
    [EspecieId] INT NULL,
    [Turno] NVARCHAR(50) NOT NULL,
    [IdEmpleado]  INT NOT NULL,
    [AñosExperiencia] INT          NOT NULL,

    FOREIGN KEY (IdEmpleado) REFERENCES [Empleados]([Id]),
    FOREIGN KEY (EspecieId) REFERENCES [Especies]([Id])
);

-- ================= PERSONAL ASEO =================
CREATE TABLE [PersonalAseo] (
    [Id]                INT PRIMARY KEY IDENTITY (1, 1),
	[IdEmpleado]         INT NOT NULL,
    [ZonaAsignada]      NVARCHAR(100) NOT NULL,
    [Turno]             NVARCHAR(50)  NOT NULL,
    [ProductosAsignados] NVARCHAR(200) NOT NULL,

    FOREIGN KEY (IdEmpleado) REFERENCES [Empleados]([Id])
);

-- ================= ENTRENADORES =================
CREATE TABLE [Entrenadores] (
    [Id]              INT PRIMARY KEY IDENTITY (1, 1),
	[IdEmpleado]  INT NOT NULL,
    [Especialidad]    NVARCHAR(100) NOT NULL,
    [TipoEntrenamiento] NVARCHAR(100) NOT NULL,

    FOREIGN KEY (IdEmpleado) REFERENCES [Empleados]([Id])
);

-- ================= DIAGNOSTICOS =================
CREATE TABLE [Diagnosticos] (
    [Id]               INT PRIMARY KEY IDENTITY(1, 1),
    [FechaDiagnostico] DATETIME2 NOT NULL,
    [FechaCura]        DATETIME2 NULL,

    [AnimalId]      INT NOT NULL,
    [EnfermedadId]  INT NOT NULL,
    [VeterinarioId] INT NOT NULL,

    FOREIGN KEY (AnimalId)      REFERENCES [Animales]([Id]),
    FOREIGN KEY (EnfermedadId)  REFERENCES [Enfermedades]([Id]),
    FOREIGN KEY (VeterinarioId) REFERENCES [Veterinarios]([Id])
);

-- ================= HISTORIALES MEDICOS =================
CREATE TABLE [HistorialesMedicos] (
    [Id]           INT PRIMARY KEY IDENTITY(1, 1),
    [AnimalId]     INT           NOT NULL,
    [Tratamiento]  NVARCHAR(200) NOT NULL,
    [Medicamento]  NVARCHAR(100) NOT NULL,
    [Dosis]        NVARCHAR(50)  NOT NULL,
    [FechaControl] DATETIME2     NOT NULL,
    [EstadoActual] NVARCHAR(100) NOT NULL,

    FOREIGN KEY (AnimalId) REFERENCES [Animales]([Id])
);

-- ================= VACUNACIONES =================
CREATE TABLE [Vacunaciones] (
    [Id]                INT PRIMARY KEY IDENTITY(1, 1),
    [AnimalId]          INT           NOT NULL,
    [NombreVacuna]      NVARCHAR(100) NOT NULL,
    [Dosis]             NVARCHAR(50)  NOT NULL,
    [FechaAplicacion]   DATETIME2     NOT NULL,
    [FechaProximaDosis] DATETIME2     NULL,
    [VeterinarioId]     INT           NOT NULL,

    FOREIGN KEY (AnimalId)      REFERENCES [Animales]([Id]),
    FOREIGN KEY (VeterinarioId) REFERENCES [Veterinarios]([Id])
);

-- ================= CUARENTENA =================
CREATE TABLE [Cuarentenas] (
    [Id]            INT             PRIMARY KEY IDENTITY(1,1),
    [AnimalId]      INT             NOT NULL,
    [VeterinarioId] INT             NOT NULL,
    [FechaInicio]   DATETIME2       NOT NULL,
    [FechaFin]      DATETIME2       NULL,           -- NULL mientras está Activa
    [Motivo]        NVARCHAR(50)    NOT NULL
                        CHECK ([Motivo] IN ('Animal Nuevo', 'Enfermedad', 'Adaptación')),
    [Estado]        NVARCHAR(20)    NOT NULL
                        CHECK ([Estado] IN ('Activa', 'Finalizada')),
    [Observaciones] NVARCHAR(500)   NULL,

    -- 🔗 FK → Animales
    CONSTRAINT FK_Cuarentenas_Animal
        FOREIGN KEY ([AnimalId]) REFERENCES [Animales]([Id]),

    -- 🔗 FK → Veterinarios
    CONSTRAINT FK_Cuarentenas_Veterinario
        FOREIGN KEY ([VeterinarioId]) REFERENCES [Veterinarios]([Id])
);
GO

-- ================= TRIGGER: Solo una cuarentena activa por animal =================
CREATE TRIGGER TR_Cuarentenas_ValidarUnicaActiva
ON [Cuarentenas]
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE (
            SELECT COUNT(*)
            FROM Cuarentenas c
            WHERE c.AnimalId = i.AnimalId
              AND c.Estado = 'Activa'
        ) > 1
    )
    BEGIN
        RAISERROR('El animal ya tiene una cuarentena activa. Finalícela antes de crear una nueva.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Validar que FechaFin no sea anterior a FechaInicio
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE i.FechaFin IS NOT NULL
          AND i.FechaFin < i.FechaInicio
    )
    BEGIN
        RAISERROR('La fecha de fin no puede ser anterior a la fecha de inicio.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- ================= TABLA INGRESOS =================
CREATE TABLE [Ingresos] (
    [Id]            INT             PRIMARY KEY IDENTITY(1,1),
    [AnimalId]      INT             NOT NULL,
    [ZoologicoId]   INT             NOT NULL,
    [FechaIngreso]  DATETIME2       NOT NULL,
    [TipoIngreso]   NVARCHAR(50)    NOT NULL
                        CHECK ([TipoIngreso] IN ('Donación', 'Rescate', 'Traslado')),
    [Procedencia]   NVARCHAR(200)   NOT NULL,
    [Estado]        NVARCHAR(50)    NOT NULL
                        CHECK ([Estado] IN ('Pendiente', 'Aceptado', 'Rechazado')),
    [Observaciones] NVARCHAR(500)   NULL,

    -- 🔗 FK → Animales
    CONSTRAINT FK_Ingresos_Animal
        FOREIGN KEY ([AnimalId]) REFERENCES [Animales]([Id]),

    -- 🔗 FK → Zoologicos
    CONSTRAINT FK_Ingresos_Zoologico
        FOREIGN KEY ([ZoologicoId]) REFERENCES [Zoologicos]([Id])
);
GO

-- ================= TRIGGER: Al aceptar ingreso crear cuarentena =================
CREATE TRIGGER TR_Ingresos_CrearCuarentena
ON [Ingresos]
AFTER INSERT, UPDATE
AS
BEGIN
    -- Solo actuar cuando el estado es Aceptado
    IF EXISTS (SELECT 1 FROM inserted WHERE Estado = 'Aceptado')
    BEGIN
        INSERT INTO Cuarentenas (AnimalId, VeterinarioId, FechaInicio, FechaFin, Motivo, Estado, Observaciones)
        SELECT
            i.AnimalId,
            1, -- VeterinarioId por defecto
            GETDATE(),
            NULL,
            CASE i.TipoIngreso
                WHEN 'Donación' THEN 'Animal Nuevo'
                ELSE 'Adaptación'
            END,
            'Activa',
            'Cuarentena generada automáticamente por ingreso tipo ' + i.TipoIngreso
        FROM inserted i
        WHERE i.Estado = 'Aceptado'
          AND NOT EXISTS (
              SELECT 1 FROM Cuarentenas c
              WHERE c.AnimalId = i.AnimalId
                AND c.Estado = 'Activa'
          );
    END
END;
GO


-- ================= ALIMENTACIONES =================
CREATE TABLE [Alimentaciones] (
    [Id]             INT PRIMARY KEY IDENTITY(1, 1),
    [AnimalId]       INT           NOT NULL,
    [TipoDieta]      NVARCHAR(100) NOT NULL,
    [CantidadDiaria] DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (AnimalId) REFERENCES [Animales]([Id])
);

-- ================= INVENTARIOS =================
CREATE TABLE [Inventarios] (
    [Id]                 INT PRIMARY KEY IDENTITY(1, 1),
    [ZoologicoId]        INT           NOT NULL,
    [NombreItem]         NVARCHAR(100) NOT NULL,
    [TipoItem]           NVARCHAR(50)  NOT NULL,
    [CantidadDisponible] DECIMAL(10,2) NOT NULL,
    [FechaVencimiento]   DATETIME2     NULL,

    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= VISITANTES =================
CREATE TABLE [Visitantes] (
    [Id]              INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre]          NVARCHAR(100) NOT NULL,
    [TipoDocumento]   NVARCHAR(50)  NOT NULL,
    [NumeroDocumento] NVARCHAR(50)  NOT NULL
);

-- ================= ENTRADAS =================
CREATE TABLE [Entradas] (
    [Id]          INT PRIMARY KEY IDENTITY(1, 1),
    [VisitanteId] INT           NOT NULL,
    [FechaVisita] DATETIME2     NOT NULL,
    [TipoEntrada] NVARCHAR(50)  NOT NULL,
    [ValorPagado] DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (VisitanteId) REFERENCES [Visitantes]([Id])
);

-- ================= ZONAS PUBLICAS =================
CREATE TABLE [ZonasPublicas] (
    [Id]          INT PRIMARY KEY IDENTITY(1, 1),
    [Nombre]      NVARCHAR(100) NOT NULL,
    [Tipo]        NVARCHAR(50)  NOT NULL,
    [ZoologicoId] INT           NOT NULL,

    FOREIGN KEY (ZoologicoId) REFERENCES [Zoologicos]([Id])
);

-- ================= AREAS =================
CREATE TABLE [Areas] (
    [Id]           INT PRIMARY KEY IDENTITY(1, 1),
    [HabitatId]    INT NULL,
    [JaulaId]      INT NULL,
    [ZonaPublicaId] INT NULL,

    FOREIGN KEY (HabitatId)     REFERENCES [Habitats]([Id]),
    FOREIGN KEY (JaulaId)       REFERENCES [Jaulas]([Id]),
    FOREIGN KEY (ZonaPublicaId) REFERENCES [ZonasPublicas]([Id])
);

-- ================= MANTENIMIENTOS =================
CREATE TABLE [Mantenimientos] (
    [Id]                    INT PRIMARY KEY IDENTITY(1, 1),
    [AreaId]                INT          NOT NULL,
    [FechaReporte]          DATETIME2    NOT NULL,
    [FechaProgramada]       DATETIME2    NOT NULL,
    [Estado]                NVARCHAR(50) NOT NULL,
    [EmpleadoResponsableId] INT          NOT NULL,

    FOREIGN KEY (AreaId)                REFERENCES [Areas]([Id]),
    FOREIGN KEY (EmpleadoResponsableId) REFERENCES [Empleados]([Id])
);

-- ================= AUDITORIAS =================
CREATE TABLE [Auditorias] (
    [IdAuditorias] INT IDENTITY(1,1) PRIMARY KEY,
    [Tabla] NVARCHAR(100) NOT NULL,
    [Accion] NVARCHAR(50) NOT NULL,
    [Datos] NVARCHAR(MAX) NOT NULL,
    [Fecha] DATETIME DEFAULT GETDATE(),
    [Usuario] NVARCHAR(50) -- Opcional: qui�n hizo el cambio
);

-- =================================================
-- =================== INSERTS =====================
-- =================================================

-- ================= ZOOLOGICO =================
INSERT INTO Zoologicos (Nombre, Ubicacion)
VALUES ('Zoo Medellín', 'Antioquia');

-- ================= ESPECIES =================
INSERT INTO Especies (Descripcion, Tipo)
VALUES 
('León africano',    'Mamífero'),
('Tigre de bengala', 'Mamífero'),
('Guacamaya',        'Ave');

-- ================= ENFERMEDADES =================
INSERT INTO Enfermedades (Nombre, Descripcion)
VALUES 
('Gripe',      'Infección respiratoria'),
('Parásitos',  'Infección intestinal');

-- ================= VISITANTES =================
INSERT INTO Visitantes (Nombre, TipoDocumento, NumeroDocumento)
VALUES 
('Juan Perez',  'CC', '111'),
('Maria Lopez', 'CC', '222');

-- ================= HABITATS =================
INSERT INTO Habitats (Nombre, Tipo, CapacidadMaxima, Estado, ZoologicoId)
VALUES 
('Sabana', 'Terrestre', 10, 'Activo', 1),
('Selva',  'Terrestre',  8, 'Activo', 1);

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

-- ================= ANIMALES =================
INSERT INTO Animales (Nombre, Naturaleza, FechaNacimiento, Alimentacion, Genero, EspecieId, JaulaId)
VALUES 
('Simba',      'Salvaje',     '2020-01-01', 'Carnívoro', 'Macho',  1, 1),
('Shere Khan', 'Salvaje',     '2019-05-10', 'Carnívoro', 'Macho',  2, 2),
('Lola',       'Domesticado', '2021-03-15', 'Frutas',    'Hembra', 3, 2);

-- ================= REPRODUCCIONES =================
INSERT INTO Reproducciones 
    (AnimalMadreId, AnimalPadreId, FechaAppariamiento, FechaNacimiento, CantidadCrias, Metodo, Estado, Observaciones)
VALUES 
(3, 1, '2023-01-15', '2023-04-20', 2, 'Natural',  'Exitosa',    'Nacieron 2 crías en buen estado'),
(3, 2, '2024-06-01', NULL,         0, 'Asistida', 'En proceso', 'Proceso de reproducción asistida en curso'),
(3, 1, '2022-08-10', NULL,         0, 'In vitro', 'Fallida',    'El proceso no fue exitoso');

-- ================= EMPLEADOS =================
INSERT INTO Empleados (Nombre, Cedula, Telefono, Email, Salario, FechaContratacion, ZoologicoId)
VALUES 
('Carlos Vet',       '100', '3001', 'vet@mail.com',      3000, GETDATE(), 1),
('Ana Gerente',      '101', '3002', 'gerente@mail.com',  5000, GETDATE(), 1),
('Luis Cuidador',    '102', '3003', 'cuidador@mail.com', 2000, GETDATE(), 1),
('Pedro Aseo',       '103', '3004', 'aseo@mail.com',     1500, GETDATE(), 1),
('Sofia Entrenadora','104', '3005', 'trainer@mail.com',  2500, GETDATE(), 1);

-- ================= ROLES =================
-- Veterinarios: NO insertar Id, usar IdEmpleado
INSERT INTO Veterinarios (IdEmpleado, Especialidad, AñosExperiencia)
VALUES (1, 'Felinos', 5);

INSERT INTO Gerentes (IdEmpleado)
VALUES (2);

INSERT INTO CuidadorAnimales (IdEmpleado, EspecieId, Turno, AñosExperiencia)
VALUES (3, 1, 'Día', 3);

INSERT INTO PersonalAseo (IdEmpleado, ZonaAsignada, Turno, ProductosAsignados)
VALUES (4, 'Zona Sabana', 'Noche', 'Desinfectante');

INSERT INTO Entrenadores (IdEmpleado, Especialidad, TipoEntrenamiento)
VALUES (5, 'Aves', 'Conductual');

-- ================= DIAGNOSTICOS =================
INSERT INTO Diagnosticos (FechaDiagnostico, FechaCura, AnimalId, EnfermedadId, VeterinarioId)
VALUES 
(GETDATE(), NULL, 1, 1, 1);

-- ================= HISTORIALES MEDICOS =================
INSERT INTO HistorialesMedicos (AnimalId, Tratamiento, Medicamento, Dosis, FechaControl, EstadoActual)
VALUES 
(1, 'Reposo', 'Ibuprofeno', '2 veces al día', GETDATE(), 'En tratamiento');

-- ================= CUARENTENA ======================
INSERT INTO Cuarentenas (AnimalId, VeterinarioId, FechaInicio, FechaFin, Motivo, Estado, Observaciones)
VALUES
(1, 1, GETDATE(), NULL,                  'Animal Nuevo', 'Activa',    'Simba ingresó al zoológico, en periodo de adaptación'),
(2, 1, GETDATE(), DATEADD(DAY, 15, GETDATE()), 'Enfermedad',   'Finalizada', 'Shere Khan presentó síntomas respiratorios, ya recuperado'),
(3, 1, GETDATE(), NULL,                  'Adaptación',   'Activa',    'Lola en proceso de adaptación a su nuevo hábitat');

-- ================= INGRESOS =================
INSERT INTO Ingresos (AnimalId, ZoologicoId, FechaIngreso, TipoIngreso, Procedencia, Estado, Observaciones)
VALUES
(1, 1, GETDATE(), 'Rescate',   'Colombia - CRQ Quindío',          'Pendiente', 'Animal rescatado de tráfico ilegal'),
(2, 1, GETDATE(), 'Donación',  'Zoológico de Cali',               'Aceptado',  'Donación institucional'),
(3, 1, GETDATE(), 'Traslado',  'Parque Natural Sierra Nevada',    'Aceptado',  'Traslado por exceso de capacidad');


-- ================= VACUNACIONES =================
INSERT INTO Vacunaciones (AnimalId, NombreVacuna, Dosis, FechaAplicacion, FechaProximaDosis, VeterinarioId)
VALUES 
(1, 'Vacuna A', '1ml', GETDATE(), NULL, 1);

-- ================= ALIMENTACIONES =================
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
