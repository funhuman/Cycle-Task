CREATE DATABASE [cycle_task]
GO
USE [cycle_task]
GO
/****** Object:  Table [dbo].[cycle_task]    Script Date: 10/23/2021 10:36:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cycle_task](
	[task_id] [int] IDENTITY(1,1) NOT NULL,
	[task_name] [varchar](100) NOT NULL,
	[last_finish_day] [date] NOT NULL,
	[yellow_line_days] [int] NOT NULL,
	[red_line_days] [int] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[update_time] [datetime] NOT NULL,
 CONSTRAINT [PK_cycle_task] PRIMARY KEY CLUSTERED
(
	[task_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cycle_task] ON
INSERT [dbo].[cycle_task] ([task_id], [task_name], [last_finish_day], [yellow_line_days], [red_line_days], [is_deleted], [create_time], [update_time]) VALUES (1, N'test1', CAST(0x20430B00 AS Date), 1, 1, 0, CAST(0x0000ADCA00A93CC9 AS DateTime), CAST(0x0000ADCA00AB34D2 AS DateTime))
INSERT [dbo].[cycle_task] ([task_id], [task_name], [last_finish_day], [yellow_line_days], [red_line_days], [is_deleted], [create_time], [update_time]) VALUES (2, N'test2', CAST(0x19430B00 AS Date), 3, 7, 0, CAST(0x0000ADCA00AA75DB AS DateTime), CAST(0x0000ADCA00AB9340 AS DateTime))
INSERT [dbo].[cycle_task] ([task_id], [task_name], [last_finish_day], [yellow_line_days], [red_line_days], [is_deleted], [create_time], [update_time]) VALUES (3, N'test3', CAST(0x1A430B00 AS Date), 7, 14, 0, CAST(0x0000ADCA00AAE8A8 AS DateTime), CAST(0x0000ADCA00AE24A7 AS DateTime))
INSERT [dbo].[cycle_task] ([task_id], [task_name], [last_finish_day], [yellow_line_days], [red_line_days], [is_deleted], [create_time], [update_time]) VALUES (4, N'test4', CAST(0x25430B00 AS Date), 14, 21, 0, CAST(0x0000ADCA00AB2A51 AS DateTime), CAST(0x0000ADCA00AB2A51 AS DateTime))
SET IDENTITY_INSERT [dbo].[cycle_task] OFF
/****** Object:  Default [DF_cycle_task_is_delete]    Script Date: 10/23/2021 10:36:37 ******/
ALTER TABLE [dbo].[cycle_task] ADD  CONSTRAINT [DF_cycle_task_is_delete]  DEFAULT ((0)) FOR [is_deleted]
GO
/****** Object:  Default [DF_cycle_task_create_time]    Script Date: 10/23/2021 10:36:37 ******/
ALTER TABLE [dbo].[cycle_task] ADD  CONSTRAINT [DF_cycle_task_create_time]  DEFAULT (getdate()) FOR [create_time]
GO
/****** Object:  Default [DF_cycle_task_update_time]    Script Date: 10/23/2021 10:36:37 ******/
ALTER TABLE [dbo].[cycle_task] ADD  CONSTRAINT [DF_cycle_task_update_time]  DEFAULT (getdate()) FOR [update_time]
GO
