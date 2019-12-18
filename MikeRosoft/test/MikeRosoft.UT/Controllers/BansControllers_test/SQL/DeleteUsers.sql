/* Delete roles */

Delete from [dbo].[AspNetUserRoles] where [RoleId] != N'6d71dd01-8c6e-473c-8313-366e3428e9a2'

/* Delete other users */
Delete from [dbo].[AspNetUsers] where UserName != 'ms@uclm.es'