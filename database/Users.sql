/*
В базе данных должна быть предусмотрена файловая группа MEMORY_OPTIMIZED_DATA,
чтобы можно было создать оптимизированный для памяти объект.

Число сегментов должно быть задано как примерно в два раза 
превышающее максимально ожидаемое количество уникальных значений в 
ключе индекса с округлением до ближайшего четного числа.
*/

CREATE TABLE [dbo].[Table1]
(
	[Login] NVARCHAR(50) NOT NULL PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT = 131072), 
    [Password] NVARCHAR(50) NULL, 
    [Name] NCHAR(254) NULL, 
    [Patronymic] NCHAR(254) NULL, 
    [TimeInPc] INT NULL, 
    [FontSize] INT NULL, 
    [FontFamily] NVARCHAR(254) NULL, 
    [SoundEffect] BIT NULL, 
    [HP_MaxVolume] BIT NULL, 
    [HP_problemsWidth] BIT NULL, 
    [Inaction] BIT NULL, 
    [RecordAudio] BIT NULL, 
    [RecordVideo] BIT NULL, 
    [ByDay] BIT NULL, 
    [URL] BIT NULL, 
    [Access] BIT NULL
) WITH (MEMORY_OPTIMIZED = ON)