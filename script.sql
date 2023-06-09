USE [BdiExamen]
GO
/****** Object:  Table [dbo].[tblExamen]    Script Date: 6/4/2023 11:54:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblExamen](
	[idExamen] [int] NOT NULL,
	[Nombre] [varchar](255) NULL,
	[Descripcion] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[idExamen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spActualizar]    Script Date: 6/4/2023 11:54:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spActualizar]
    @id INT,
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255)
AS
BEGIN
    DECLARE @ErrorCode INT, @ErrorMessage VARCHAR(255)
    
    BEGIN TRY
        UPDATE tblExamen
        SET Nombre = @Nombre, Descripcion = @Descripcion
        WHERE idExamen = @id

        SET @ErrorCode = 0
        SET @ErrorMessage = 'Registro actualizado satisfactoriamente'
    END TRY
    BEGIN CATCH
        SET @ErrorCode = ERROR_NUMBER()
        SET @ErrorMessage = ERROR_MESSAGE()
    END CATCH

    SELECT @ErrorCode AS ErrorCode, @ErrorMessage AS ErrorMessage
END
GO
/****** Object:  StoredProcedure [dbo].[spAgregar]    Script Date: 6/4/2023 11:54:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAgregar]
    @id INT,
    @Nombre VARCHAR(255),
    @Descripcion VARCHAR(255)
AS
BEGIN
    DECLARE @ErrorCode INT, @ErrorMessage VARCHAR(255)
    
    BEGIN TRY
        INSERT INTO tblExamen (idExamen, Nombre, Descripcion)
        VALUES (@id, @Nombre, @Descripcion)

        SET @ErrorCode = 0
        SET @ErrorMessage = 'Registro insertado satisfactoriamente'
    END TRY
    BEGIN CATCH
        SET @ErrorCode = ERROR_NUMBER()
        SET @ErrorMessage = ERROR_MESSAGE()
    END CATCH

    SELECT @ErrorCode AS ErrorCode, @ErrorMessage AS ErrorMessage
END
GO
/****** Object:  StoredProcedure [dbo].[spConsulta]    Script Date: 6/4/2023 11:54:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spConsulta]
    @Nombre VARCHAR(255) = NULL,
    @Descripcion VARCHAR(255) = NULL
AS
BEGIN
    SELECT idExamen, Nombre, Descripcion
    FROM tblExamen
    WHERE (@Nombre IS NULL OR Nombre = @Nombre)
        AND (@Descripcion IS NULL OR Descripcion = @Descripcion)
END

GO
/****** Object:  StoredProcedure [dbo].[spEliminar]    Script Date: 6/4/2023 11:54:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminar]
    @id INT,
    @ErrorCode INT OUTPUT,
    @ErrorMessage VARCHAR(255) OUTPUT
AS
BEGIN
    BEGIN TRY
        DELETE FROM tblExamen
        WHERE idExamen = @id

        SET @ErrorCode = 0
        SET @ErrorMessage = 'Registro eliminado satisfactoriamente'
    END TRY
    BEGIN CATCH
        SET @ErrorCode = ERROR_NUMBER()
        SET @ErrorMessage = ERROR_MESSAGE()
    END CATCH

    SELECT @ErrorCode AS ErrorCode, @ErrorMessage AS ErrorMessage
END
GO
