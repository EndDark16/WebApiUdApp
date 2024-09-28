
USE [UdAppDB]
GO
/****** Object:  Table [dbo].[CHAT]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHAT](
	[idChat] [int] NOT NULL,
	[fk_idUsuarioEmisor] [int] NULL,
	[fk_idUsuarioReceptor] [int] NULL,
	[contenido] [text] NULL,
	[fechaEnvio] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idChat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMENTARIO]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMENTARIO](
	[idComentario] [int] NOT NULL,
	[contenido] [text] NULL,
	[fechaComentario] [date] NULL,
	[fk_idUsuarioComentador] [int] NULL,
	[fk_idPublicacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idComentario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GRUPO]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GRUPO](
	[idGrupo] [int] NOT NULL,
	[nombreGrupo] [varchar](100) NULL,
	[descripcion] [text] NULL,
	[fechaCreacion] [date] NULL,
	[fk_idUsuarioCreador] [int] NULL,
	[Estado] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HORARIO]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HORARIO](
	[idHorario] [int] NOT NULL,
	[diaSemana] [varchar](20) NULL,
	[horaInicio] [time](7) NULL,
	[horaFin] [time](7) NULL,
	[fk_idUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PUBLICACION]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PUBLICACION](
	[idPublicacion] [int] NOT NULL,
	[titulo] [varchar](100) NULL,
	[contenido] [text] NULL,
	[fechaPublicacion] [date] NULL,
	[fk_idUsuarioPublicador] [int] NULL,
	[likes] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPublicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REPORTE]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REPORTE](
	[idReporte] [int] NOT NULL,
	[tipoReporte] [varchar](50) NULL,
	[fechaReporte] [date] NULL,
	[fk_idUsuarioReportador] [int] NULL,
	[fk_idPublicacionReportada] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idReporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[idRol] [int] NOT NULL,
	[nombreRol] [nchar](10) NULL,
	[permisos] [nchar](10) NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[idUsuario] [int] NOT NULL,
	[nombreUsuario] [varchar](50) NULL,
	[correoElectronico] [varchar](100) NULL,
	[contrasena] [varchar](50) NULL,
	[fk_idRol] [varchar](20) NULL,
	[estadoSuspension] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO_HAS_GRUPO]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO_HAS_GRUPO](
	[fk_idUsuario] [int] NOT NULL,
	[fk_idGrupo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[fk_idUsuario] ASC,
	[fk_idGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO_HAS_PUBLICACION]    Script Date: 10/04/2024 4:39:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO_HAS_PUBLICACION](
	[fk_idUsuario] [int] NOT NULL,
	[fk_idPublicacion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[fk_idUsuario] ASC,
	[fk_idPublicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[AUDITORIA](
    [idAuditoria] [int] IDENTITY(1,1) NOT NULL,           -- ID autoincremental para la auditoría
      NOT NULL,              -- Nombre de la tabla afectada
      NOT NULL,                      -- Tipo de acción: 'INSERT', 'UPDATE', 'DELETE'
    [detalleCambios] [varchar]400NULL,                 -- Detalle de los cambios realizados
    [fechaAccion] [datetime] NOT NULL DEFAULT GETDATE(),  -- Fecha de la acción
    [fk_idUsuario] [int] NULL,                            -- Usuario que realizó la acción
    [observaciones] [varchar]400NULL,                  -- Observaciones adicionales
PRIMARY KEY CLUSTERED 
(
    [idAuditoria] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) 
ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

ALTER TABLE [dbo].[CHAT]  WITH CHECK ADD FOREIGN KEY([fk_idUsuarioEmisor])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[CHAT]  WITH CHECK ADD FOREIGN KEY([fk_idUsuarioReceptor])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[COMENTARIO]  WITH CHECK ADD FOREIGN KEY([fk_idUsuarioComentador])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[COMENTARIO]  WITH CHECK ADD FOREIGN KEY([fk_idPublicacion])
REFERENCES [dbo].[PUBLICACION] ([idPublicacion])
GO
ALTER TABLE [dbo].[GRUPO]  WITH CHECK ADD FOREIGN KEY([fk_idUsuarioCreador])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[HORARIO]  WITH CHECK ADD FOREIGN KEY([fk_idUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[PUBLICACION]  WITH CHECK ADD FOREIGN KEY([fk_idUsuarioPublicador])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[REPORTE]  WITH CHECK ADD FOREIGN KEY([fk_idPublicacionReportada])
REFERENCES [dbo].[PUBLICACION] ([idPublicacion])
GO
ALTER TABLE [dbo].[REPORTE]  WITH CHECK ADD FOREIGN KEY([fk_idUsuarioReportador])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[USUARIO_HAS_GRUPO]  WITH CHECK ADD FOREIGN KEY([fk_idUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[USUARIO_HAS_GRUPO]  WITH CHECK ADD FOREIGN KEY([fk_idGrupo])
REFERENCES [dbo].[GRUPO] ([idGrupo])
GO
ALTER TABLE [dbo].[USUARIO_HAS_PUBLICACION]  WITH CHECK ADD FOREIGN KEY([fk_idUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[USUARIO_HAS_PUBLICACION]  WITH CHECK ADD FOREIGN KEY([fk_idPublicacion])
REFERENCES [dbo].[PUBLICACION] ([idPublicacion])
GO
ALTER TABLE [dbo].[AUDITORIA] WITH CHECK ADD CONSTRAINT FK_Auditoria_Usuario 
FOREIGN KEY([fk_idUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario]);
GO
